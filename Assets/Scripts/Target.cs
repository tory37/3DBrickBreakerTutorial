using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int pointsOnHit;
    [SerializeField]
    private int pointsOnDestroy;

    public System.Action<Target> OnHit;
    public System.Action<Target> OnDestroy;

    public int PointsOnHit { get => pointsOnHit; }
    public int PointsOnDestroy { get => pointsOnDestroy; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BallController>())
        {
            health -= 1;
            OnHit?.Invoke(this);
        }

        if (health >= 0)
        {
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
