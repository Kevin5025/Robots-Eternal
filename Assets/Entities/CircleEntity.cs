using UnityEngine;
using System;

/**
 * This is a special entity that is a circle. 
 * Circles have a certain amount of force and torque in this Universe to move and rotate. 
 */
public abstract class CircleEntity : Entity {

    protected Rigidbody2D rb2D;
    public float radius;
    public float area;
    public float force;
    public float torque;

    protected int lastRotateDirection = 0;

    // Use this for initialization
    protected override void Start () {
		base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        radius = Mathf.Sqrt(2* rb2D.inertia/rb2D.mass);
        area = (float) Math.PI * radius * radius;
        GetComponent<Rigidbody2D>().mass = area;
        force = GetComponent<Rigidbody2D>().mass * 25f;
        torque = GetComponent<Rigidbody2D>().inertia * 50f;
    }

    protected override void FixedUpdate () {
        base.FixedUpdate();
    }

    /**
     * Smart rotation based. Uses current angular momentum for predicted rotation and compares to desired rotation. 
     */
    public virtual void Rotate (Vector2 targetPosition, float power = 1f) {
        if (!defunct) {
            float currentRotation = transform.eulerAngles.z;
            float predictedChangeInAngularVelocity = 0.5f * lastRotateDirection * (power * torque / rb2D.inertia) * Time.fixedDeltaTime;//magic 0.5f is due to angular drag
            float predictedChangeInRotation = (rb2D.angularVelocity + predictedChangeInAngularVelocity) * Time.fixedDeltaTime;
            float predictedRotation = currentRotation + predictedChangeInRotation;

            float targetRotation = Mathf.Atan2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y) * -Mathf.Rad2Deg;//0 to 180, then -180 to 0 counterclockwise
            float offsetRotation = ((targetRotation - predictedRotation) % 360 + 360) % 360;

            if (offsetRotation > 180) {
                lastRotateDirection = 1;
                rb2D.AddTorque(-power * torque);//turn right
            } else if (offsetRotation < 180) {
                lastRotateDirection = -1;
                rb2D.AddTorque(power * torque);//turn left
            } else {
                lastRotateDirection = 0;
                rb2D.AddTorque((180 - offsetRotation) * power * torque);
            }
        }
    }

    /**
     * Decides which of the 8 WASD directions (including diagonals) to move, in order to naively flee target position. 
     */
    public virtual void Flee (Vector2 targetPosition) {
        Vector2 currentPosition = transform.position;
        Vector2 offsetPosition = targetPosition - currentPosition;

        bool W = Math.Atan2(offsetPosition.y, offsetPosition.x) > 1 * Math.PI / 8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < 7 * Math.PI / 8;//offsetPosition.y > 0;
        bool S = Math.Atan2(offsetPosition.y, offsetPosition.x) > -7 * Math.PI / 8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < -1 * Math.PI / 8;//offsetPosition.y < 0;
        bool D = Math.Atan2(offsetPosition.y, offsetPosition.x) > -3 * Math.PI / 8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < 3 * Math.PI / 8;//offsetPosition.x > 0;
        bool A = Math.Atan2(offsetPosition.y, offsetPosition.x) > 5 * Math.PI / 8 || Math.Atan2(offsetPosition.y, offsetPosition.x) < -5 * Math.PI / 8;//offsetPosition.x < 0;
        Move(S, W, A, D);
    }

    /**
     * Decides which of the 8 WASD directions (including diagonals) to move, in order to reach target position. 
     */
    public virtual void Move (Vector2 targetPosition) {
        Vector2 currentPosition = transform.position;
        Vector2 offsetPosition = targetPosition - currentPosition;

        bool W = Math.Atan2(offsetPosition.y, offsetPosition.x) > 1*Math.PI/8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < 7*Math.PI/8;//offsetPosition.y > 0;
        bool S = Math.Atan2(offsetPosition.y, offsetPosition.x) > -7*Math.PI/8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < -1*Math.PI/8;//offsetPosition.y < 0;
        bool D = Math.Atan2(offsetPosition.y, offsetPosition.x) > -3*Math.PI/8 && Math.Atan2(offsetPosition.y, offsetPosition.x) < 3*Math.PI/8;//offsetPosition.x > 0;
        bool A = Math.Atan2(offsetPosition.y, offsetPosition.x) > 5*Math.PI/8 || Math.Atan2(offsetPosition.y, offsetPosition.x) < -5*Math.PI/8;//offsetPosition.x < 0;
        Move(W, S, D, A);
    }

    /**
     * Moves up for W, down for S, right for D, and left for A. 
     * Diagonal movement for orthogonal combinations of WASD. 
     */
    public virtual void Move (bool W, bool S, bool D, bool A) {
        if (!defunct) {
            float verticalDirection = 0;
            verticalDirection += W ? 1 : 0;
            verticalDirection += S ? -1 : 0;

            float horizontalDirection = 0;
            horizontalDirection += D ? 1 : 0;
            horizontalDirection += A ? -1 : 0;

            float verticalForce = 0;
            float horizontalForce = 0;
            if ((new Vector2(horizontalDirection, verticalDirection).magnitude > 0)) {
                double direction = Math.Atan2(verticalDirection, horizontalDirection);
                verticalForce = force * (float)Math.Sin(direction);
                horizontalForce = force * (float)Math.Cos(direction);
            }
            //Debug.Log(new Vector2(horizontalForce, verticalForce));
            //if (relative) {
            //    rb2D.AddRelativeForce(power * new Vector2(horizontalForce, verticalForce));
            //} else {
            rb2D.AddForce(new Vector2(horizontalForce, verticalForce));
            //}
        }
    }
}
