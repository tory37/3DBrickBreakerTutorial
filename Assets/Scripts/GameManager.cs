using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField]
    private GameObject gameOverPanel;

    private int points = 0;
    private Target[] targets;
    private int targetsLeft;

    public System.Action<int> OnPointsChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targets = FindObjectsOfType<Target>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].OnHit += OnTargetHit;
            targets[i].OnDestroy += OnTargetDestroyed;
            targetsLeft++;
        }
    }

    private void OnTargetHit(Target target)
    {
        points += target.PointsOnHit;
        OnPointsChange?.Invoke(points);
    }

    private void OnTargetDestroyed(Target target)
    {
        points += target.PointsOnDestroy;
        target.OnHit -= OnTargetHit;
        target.OnDestroy -= OnTargetDestroyed;
        OnPointsChange?.Invoke(points);
        targetsLeft--;

        if (targetsLeft <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            gameOverPanel.SetActive(true);
        }
    }

    public void Replay()
    {
        // Way 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
