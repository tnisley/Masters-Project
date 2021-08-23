using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour, IMover
{
    // can be set in editor
    public enum MoveDirection { VERTICAL, HORIZONTAL};
    public bool startReversed = false;
    public MoveDirection movement;
    public float speed = 0f;

    protected Vector2 direction;

    protected Vector2 origin;  // center of platform
    [SerializeField]
    protected float distance;
    protected Vector2 move;

    protected Rigidbody2D rbody;


    // Start is called before the first frame update
    void Start()
    {
        // set initial direction
        if (movement == MoveDirection.HORIZONTAL)
        {
            if (startReversed)
                direction = Vector2.left;
            else
                direction = Vector2.right;
        }

        else
        {
            if (startReversed)
                direction = Vector2.down;
            else
                direction = Vector2.up;
        }

            rbody = GetComponent<Rigidbody2D>();
            origin.x = transform.position.x;
            origin.y = transform.position.y;
            move = transform.position;
        }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 moveDistance = speed * Time.deltaTime * direction;

        rbody.position = rbody.position + moveDistance;


        if (rbody.position.x >= origin.x + distance / 2 || rbody.position.x < origin.x - distance / 2)
            direction.x *= -1;
        if (rbody.position.y >= origin.y + distance / 2 || rbody.position.y < origin.y - distance / 2)
            direction.y *= -1;
    }

    public Vector2 GetVelocity()
    {
        return speed * direction;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    // show platform path in the scene editor
    private void OnDrawGizmos()
    {
        Vector3 size = GetComponent<BoxCollider2D>().bounds.size;
        Gizmos.color = Color.green;
        Vector3 start = new Vector3(0f, 0f, 0f);

        if (movement == MoveDirection.HORIZONTAL)
        {
            start.x = transform.position.x - size.x / 2 - distance / 2;
            start.y = transform.position.y;

            Vector3 lineVector = transform.TransformDirection(Vector3.right) * (distance + size.x);
            Gizmos.DrawRay(start, lineVector);
        }

        else
        {
            start.y = transform.position.y - size.y / 2 - distance / 2;
            start.x = transform.position.x;
            Vector3 lineVector = transform.TransformDirection(Vector3.up) * (distance + size.y);
            Gizmos.DrawRay(start, lineVector);
        }

    }
}
