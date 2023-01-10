using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField]
    private float maxPaddleAngle = 20;
    [SerializeField]
    private float tiltSpeed;
    [SerializeField]
    private Transform paddleJoint;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Reset the joint on click
        if (Input.GetMouseButtonDown(0))
        {
            paddleJoint.rotation = transform.rotation;
        }
        else
        {
            float horizontal = -Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            paddleJoint.Rotate(transform.right * vertical * tiltSpeed * Time.deltaTime, Space.World);
            paddleJoint.Rotate(transform.up * horizontal * tiltSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION HAPPENED");
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball)
        {
            Vector3 origin = ball.transform.position;
            Vector3 target = Vector3.zero;
            // Option 1
            //Vector3 direction = (Vector3.zero - transform.position).normalized;
            // Option 2
            Vector3 direction = transform.forward;
            ball.ChangeDirection(direction);
        }
    }
}
