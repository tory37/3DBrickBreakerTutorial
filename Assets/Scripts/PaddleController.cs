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
}
