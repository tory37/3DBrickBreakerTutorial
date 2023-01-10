using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField]
    private float ballSpeed;

    public System.Action OnBallDestroyed;

    private Rigidbody rb;
    private bool isLaunched = false;

    public void OnLaunch()
    {
        isLaunched = true;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * ballSpeed;
    }

    public void ChangeDirection(Vector3 direction)
    {
        rb.velocity = direction * rb.velocity.magnitude;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.magnitude > 10)
        {
            Destroy(gameObject);
            OnBallDestroyed?.Invoke();
        }
        else if (isLaunched)
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }

        Vector3 direction = (Vector3.zero - transform.position).normalized;
        Debug.DrawRay(transform.position, direction * 100, Color.red);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //direction = rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal).normalized;
    }
}
