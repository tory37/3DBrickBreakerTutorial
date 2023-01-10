using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pointsText;

    private void Start()
    {
        GameManager.Instance.OnPointsChange += OnPointsChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPointsChange -= OnPointsChanged;
    }

    private void OnPointsChanged(int points)
    {
        pointsText.text = "Points: " + points;
    }
}
