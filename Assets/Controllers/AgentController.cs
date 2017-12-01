using UnityEngine;
using PriorityQueueDemo;
using System;
using System.Collections.Generic;
using System.Collections;

/**
 * Abstracted for easy access later for abstract behaviors. 
 */
public abstract class AgentController : MonoBehaviour {

    public CircleAgent agent;

    public GameObject closestHostileAgentGameObject;

    public Vector2 targetPosition;//TODO protected
    protected Stack<int> pathToTargetIndexX;//updates according to targetPosition every handful of moments
    protected Stack<int> pathToTargetIndexY;
    private int nextNodeToTargetIndexX;
    private int nextNodeToTargetIndexY;
    private Vector2 nextNodeToTargetPosition;

    float attackDistance;

    protected virtual void Awake () { }

    // Use this for initialization
    protected virtual void Start () {
        agent = GetComponent<CircleAgent>();

        closestHostileAgentGameObject = null;

        targetPosition = transform.position;
        pathToTargetIndexX = new Stack<int>();
        pathToTargetIndexY = new Stack<int>();
        nextNodeToTargetIndexX = -1;
        nextNodeToTargetIndexY = -1;
        nextNodeToTargetPosition = transform.position;

        attackDistance = 12f;
    }

    // Update is called once per frame
    protected virtual void Update () { }

    protected virtual void FixedUpdate () { }

    /**
     * After updating, we rotate at, move towards, and attack the nextNodeToTargetPosition
     */
    protected void CautiousHuntPath () {
        UpdateNextNodeToTarget();
        CautiousHunt(nextNodeToTargetPosition);
    }

    /**
     * After updating, we rotate at, move towards, and attack the nextNodeToTargetPosition
     */
    protected void HuntPath () {
        UpdateNextNodeToTarget();
        Hunt(nextNodeToTargetPosition);
    }

    /**
     * After updating, we rotate and move towards the nextNodeToTargetPosition
     */
    protected void FollowPath () {
        UpdateNextNodeToTarget();
        NaiveFollow(nextNodeToTargetPosition);
    }

    /**
     * We want to update nextNodeToTargetPosition if we have reached the position or if we have never initialized the position
     */
    protected void UpdateNextNodeToTarget () {
        float distanceToNextNodeToTarget = MyStaticLibrary.GetDistance(transform.position, nextNodeToTargetPosition);
        while ((distanceToNextNodeToTarget < Math.Sqrt(0.5) || nextNodeToTargetIndexY < 0) && pathToTargetIndexY.Count > 0) {
            nextNodeToTargetIndexX = pathToTargetIndexX.Pop();
            nextNodeToTargetIndexY = pathToTargetIndexY.Pop();
            nextNodeToTargetPosition.x = EnvironmentManager.environmentManager.getPositionX(nextNodeToTargetIndexX);
            nextNodeToTargetPosition.y = EnvironmentManager.environmentManager.getPositionY(nextNodeToTargetIndexY);
            distanceToNextNodeToTarget = MyStaticLibrary.GetDistance(transform.position, nextNodeToTargetPosition);
        }
    }

    protected virtual void CautiousHunt (Vector2 targetPosition) {
        if (closestHostileAgentGameObject != null) {
            float distance = MyStaticLibrary.GetDistance(agent.transform.position, closestHostileAgentGameObject.transform.position);
            if (distance < attackDistance) {
                //agent.Rotate(this.targetPosition);
                agent.Rotate(closestHostileAgentGameObject.transform.position);
                if (agent.health < closestHostileAgentGameObject.GetComponent<CircleAgent>().health) {
                    agent.inventoryEquipableArray[6].Activate(agent);
                    agent.Flee(targetPosition);//cautiousness
                } else {
                    agent.Move(targetPosition);
                }
                agent.inventoryEquipableArray[0].Activate(agent);
            } else {
                NaiveFollow(targetPosition);
            }
        } else {
            NaiveFollow(targetPosition);
        }
    }

    /**
     * Simply move/attack towards the target forgetting about walls etc. 
     */
    protected virtual void Hunt (Vector2 targetPosition) {
        //NaiveFollow(targetPosition);
        //NaiveAttack(this.targetPosition);
        if (closestHostileAgentGameObject != null) {
            float distance = MyStaticLibrary.GetDistance(agent.transform.position, closestHostileAgentGameObject.transform.position);
            if (distance < attackDistance) {
                //agent.Rotate(this.targetPosition);
                agent.Rotate(closestHostileAgentGameObject.transform.position);
                if (agent.health < closestHostileAgentGameObject.GetComponent<CircleAgent>().health) {
                    agent.inventoryEquipableArray[6].Activate(agent);
                    agent.Move(targetPosition);
                } else {
                    agent.Move(targetPosition);
                }
                agent.inventoryEquipableArray[0].Activate(agent);
            } else {
                NaiveFollow(targetPosition);
            }
        } else {
            NaiveFollow(targetPosition);
        }
    }

    /**
     * Simply move towards the target forgetting about walls etc. 
     */
    protected virtual void NaiveFollow (Vector2 targetPosition) {
        agent.Rotate(targetPosition);
        agent.Move(targetPosition);
    }

    /**
     * Simply attack the target if in a range. 
     */
    protected virtual void NaiveAttack (Vector2 targetPosition) {
        float distance = MyStaticLibrary.GetDistance(agent.transform.position, targetPosition);
        if (distance < attackDistance) {
            agent.inventoryEquipableArray[0].Activate(agent);
        }
    }

    /**
     * Updates targetPosition to be the closestHostileAgent position
     */
    protected IEnumerator KeepFindClosestHostileAgent () {
        while (true) {
            closestHostileAgentGameObject = FindClosestHostileAgent();
            yield return new WaitForSeconds(Math.Max(1f, MyStaticLibrary.GetDistance(transform.position, targetPosition)/10f));
        }
    }

    /**
     * Returns closest circle agent who is on a different team or null if none. 
     */
    protected GameObject FindClosestHostileAgent () {
        CircleAgent[] circleAgentArray = FindObjectsOfType<CircleAgent>();
        List<CircleAgent> hostileCircleAgentList = new List<CircleAgent>();
        for (int c = 0; c < circleAgentArray.Length; c++) {
            if (circleAgentArray[c].team != agent.team && !circleAgentArray[c].defunct) {
                hostileCircleAgentList.Add(circleAgentArray[c]);
            }
        }
        if (hostileCircleAgentList.Count <= 0) {
            return null;
        }

        GameObject closestHostileCircleAgentGameObject = hostileCircleAgentList[0].gameObject;
        float closestCircleAgentTransformDistance = MyStaticLibrary.GetDistance(gameObject, closestHostileCircleAgentGameObject);
        for (int c = 1; c < hostileCircleAgentList.Count; c++) {
            float distance = MyStaticLibrary.GetDistance(gameObject, hostileCircleAgentList[c].gameObject);
            if (distance < closestCircleAgentTransformDistance) {
                closestHostileCircleAgentGameObject = hostileCircleAgentList[c].gameObject;
                closestCircleAgentTransformDistance = distance;
            }
        }

        return closestHostileCircleAgentGameObject;
    }

    /**
     * Updates the path to targetPosition every second
     */
    protected virtual IEnumerator KeepFindAStarSearchPath () {
        while (true) {
            yield return FindAStarSearchPath(targetPosition);
            yield return new WaitForSeconds(Math.Max(0.5f, MyStaticLibrary.GetDistance(transform.position, targetPosition)/10f));
        }
    }

    /**
     * Performs the A* Search algorithm from caller's position to target position. 
     */
    protected virtual IEnumerator FindAStarSearchPath (Vector2 targetPosition) {
        bool[,] tightEnvironmentGrid = EnvironmentManager.environmentManager.tightEnvironmentGrid;
        bool[,] looseEnvironmentGrid = EnvironmentManager.environmentManager.looseEnvironmentGrid;
        bool[,] isVisitedEnvironmentGrid = new bool[looseEnvironmentGrid.GetLength(0), looseEnvironmentGrid.GetLength(1)];
        int[,] parentIndexXGrid = new int[looseEnvironmentGrid.GetLength(0), looseEnvironmentGrid.GetLength(1)];
        int[,] parentIndexYGrid = new int[looseEnvironmentGrid.GetLength(0), looseEnvironmentGrid.GetLength(1)];
        PriorityQueue<float, float[]> frontier = new PriorityQueue<float, float[]>();

        /**
         * source node and target node
         */
        int targetIndexX = EnvironmentManager.environmentManager.getIndexX(targetPosition.x);
        int targetIndexY = EnvironmentManager.environmentManager.getIndexY(targetPosition.y);
        int sourceIndexX = EnvironmentManager.environmentManager.getIndexX(transform.position.x);
        int sourceIndexY = EnvironmentManager.environmentManager.getIndexY(transform.position.y);
        float startDistanceTraveled = 0;

        if (tightEnvironmentGrid[sourceIndexX, sourceIndexY] || tightEnvironmentGrid[targetIndexX, targetIndexY]) {
            Debug.Log("astar source or target in wall");
            yield break;
        }

        /**
         * Explore frontier from source node to target node
         */
        //yield return new WaitForSeconds(5f);
        frontier.Enqueue(startDistanceTraveled + euclideanDistance(targetIndexX, targetIndexY, sourceIndexX, sourceIndexY), new float[3] { sourceIndexX, sourceIndexY, startDistanceTraveled });
        isVisitedEnvironmentGrid[sourceIndexX, sourceIndexY] = true;
        parentIndexXGrid[sourceIndexX, sourceIndexY] = -1;
        parentIndexYGrid[sourceIndexX, sourceIndexY] = -1;
        while (!frontier.IsEmpty) {
            KeyValuePair<float, float[]> node = frontier.Dequeue();
            float priority = node.Key;
            int frontierIndexX = (int)node.Value[0];
            int frontierIndexY = (int)node.Value[1];
            float distanceTraveled = node.Value[2];

            //Debug.Log("frontier node: " + frontierIndexX + ", " + frontierIndexY + ", " + priority + ", " + distanceTraveled);
            //yield return instantiateNode(frontierIndexX, frontierIndexY, 0.01f, new Color(1, 1, 1));

            if ((frontierIndexX == targetIndexX && frontierIndexY == targetIndexY) || distanceTraveled > looseEnvironmentGrid.GetLength(0) + looseEnvironmentGrid.GetLength(1)) {
                break;
            }

            /**
             * Expand the node by adding its neighbors to the frontier
             */
            int[][] neighbors = EnvironmentManager.environmentManager.getNeighbors(frontierIndexX, frontierIndexY);
            for (int n = 0; n < neighbors.Length; n++) {
                if (neighbors[n] == null) {
                    continue;
                }
                float edgeDistanceToTravel = n <= 3 ? 1 : (float)Math.Sqrt(2);
                int neighborIndexX = neighbors[n][0];
                int neighborIndexY = neighbors[n][1];
                float neighborDistanceTraveled = distanceTraveled + edgeDistanceToTravel;
                bool isWall = (looseEnvironmentGrid[neighborIndexX, neighborIndexY] && !(neighborIndexX == targetIndexX && neighborIndexY == targetIndexY)) || tightEnvironmentGrid[neighborIndexX, neighborIndexY];
                if (!isVisitedEnvironmentGrid[neighborIndexX, neighborIndexY] && !isWall) {
                    frontier.Enqueue(neighborDistanceTraveled + euclideanDistance(targetIndexX, targetIndexY, neighborIndexX, neighborIndexY), new float[3] { neighborIndexX, neighborIndexY, neighborDistanceTraveled });
                    isVisitedEnvironmentGrid[neighborIndexX, neighborIndexY] = true;
                    parentIndexXGrid[neighborIndexX, neighborIndexY] = frontierIndexX;
                    parentIndexYGrid[neighborIndexX, neighborIndexY] = frontierIndexY;
                    //Debug.Log("neighbor node: " + neighborXIndex + ", " + neighborYIndex + "; parent node: " + frontierXIndex + ", " + frontierYIndex);
                }
            }
        }

        yield return tracePathToTarget(parentIndexXGrid, parentIndexYGrid, targetIndexX, targetIndexY, sourceIndexX, sourceIndexY);
    }

    /**
     * Trace path through parent pointers
     */
    private IEnumerator tracePathToTarget (int[,] parentIndexXGrid, int[,] parentIndexYGrid, int targetIndexX, int targetIndexY, int sourceIndexX, int sourceIndexY) {
        Stack<int> pathToTargetIndexX = new Stack<int>();
        Stack<int> pathToTargetIndexY = new Stack<int>();
        int indexX = targetIndexX;
        int indexY = targetIndexY;
        pathToTargetIndexX.Push(indexX);
        pathToTargetIndexY.Push(indexY);
        //yield return instantiateNode(indexX, indexY, 0.1f, new Color(0, 0, 0));
        while (indexX != sourceIndexX || indexY != sourceIndexY) {
            int parentXIndex = parentIndexXGrid[indexX, indexY];
            int parentYIndex = parentIndexYGrid[indexX, indexY];
            indexX = parentXIndex;
            indexY = parentYIndex;
            pathToTargetIndexX.Push(indexX);
            pathToTargetIndexY.Push(indexY);
            //yield return instantiateNode(indexX, indexY, 0.1f, new Color(0, 0, 0));
        }
        nextNodeToTargetPosition = transform.position;
        this.pathToTargetIndexX = pathToTargetIndexX;//should be atomic
        this.pathToTargetIndexY = pathToTargetIndexY;//should be atomic
        yield return null;
    }

    /**
     * Places a circle node at index location mostly for demo purposes
     */
    private IEnumerator instantiateNode (int indexX, int indexY, float waitTime, Color color) {
        yield return new WaitForSeconds(waitTime);
        Debug.Log(indexX + ", " + indexY);
        Vector2 position = 0.5f * new Vector2(EnvironmentManager.environmentManager.minAllIndicesX + indexX, EnvironmentManager.environmentManager.minAllIndicesY + indexY);
        GameObject nodeGameObject = Instantiate(PrefabReferences.prefabReferences.circleSmall2, position, transform.rotation);
        nodeGameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private float euclideanDistance (int targetIndexX, int targetIndexY, int indexX, int indexY) {
        return (float) Math.Sqrt((targetIndexX - indexX) * (targetIndexX - indexX) + (targetIndexY - indexY) * (targetIndexY - indexY));
    }

    /**
     * min 8 directional distance weighting 1 on axis-parallel directions and sqrt(2) on diagonal directions
     */
    private float manhattanDiagonalDistance (int targetIndexX, int targetIndexY, int indexX, int indexY) {
        int deltaXIndex = Math.Abs(targetIndexX - indexX);
        int deltaYIndex = Math.Abs(targetIndexY - indexY);
        int deltaDiagonalIndex = Math.Min(deltaXIndex, deltaYIndex);
        int deltaAxisParallelIndex = Math.Abs(deltaXIndex - deltaYIndex);
        return deltaAxisParallelIndex + (float)Math.Sqrt(2) * deltaDiagonalIndex;
    }

    private float manhattanDistance (int targetIndexX, int targetIndexY, int indexX, int indexY) {
        return Math.Abs(targetIndexX - indexX) + Math.Abs(targetIndexY - indexY);
    }
}
