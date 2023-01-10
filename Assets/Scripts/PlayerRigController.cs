using UnityEngine;
using System.Numerics;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRigController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float skewSpeed;

    [SerializeField]
    private Camera activeCamera;
    [SerializeField]
    private Transform activePaddle;

    [Header("Prefabs")]
    [SerializeField]
    private BallController ballPrefab;
    [SerializeField]
    private GameObject ballPreLaunchPrefab;

    private static PlayerRigController instance;

    // Cached Components
    private Rigidbody rb;

    // Current objects
    private GameObject currentPreLaunchBall;


    public static PlayerRigController Instance
    {
        get => instance;
    }

    public void OnBallDestroyed()
    {
        InstantiateBall();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = UnityEngine.Vector3.zero;
        InstantiateBall();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnLaunchBall();
        }

        float x = -Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        float z = -Input.GetAxis("Skew");

        transform.Rotate(
            (x * rotationSpeed * Time.deltaTime * transform.up) +
            (y * rotationSpeed * Time.deltaTime * transform.right) +
            (z * skewSpeed * Time.deltaTime * transform.forward),
            Space.World
        );
    }

    private void FixedUpdate()
    {
        float x = -Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        float z = -Input.GetAxis("Skew");

        UnityEngine.Vector3 deltaRotation = 10 * rotationSpeed * ((transform.right * y) + (transform.up * x)) * Time.deltaTime;
        UnityEngine.Vector3 deltaSkew = 10 * skewSpeed * (transform.forward * z) * Time.deltaTime;

        //rb.MoveRotation(rb.rotation * UnityEngine.Quaternion.Euler(Time.deltaTime * (deltaRotation + deltaSkew)));
        //rb.rotation = UnityEngine.Quaternion.Euler(rb.rotation.eulerAngles + (x * rotationSpeed * Time.fixedDeltaTime * transform.up));
    }

    private void InstantiateBall()
    {
        currentPreLaunchBall = GameObject.Instantiate(ballPreLaunchPrefab, activePaddle.position + (activePaddle.forward * 2), activePaddle.rotation, this.transform);
    }

    private void OnLaunchBall()
    {
        Destroy(currentPreLaunchBall);
        BallController launchedBall = GameObject.Instantiate(ballPrefab, activePaddle.position + (activePaddle.forward * 2), activePaddle.rotation);
        launchedBall.OnLaunch();
        launchedBall.OnBallDestroyed += InstantiateBall;
    }
}
