using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Cost")]
    public float MaxCost = 100;
    public float currentCost;

    // Increase cost by 1 every second
    public float costIncreaseRate = 1f; // 1 cost per second

    public void Start()
    {
        currentCost = 0;
    }

    public void Update()
    {
        // Increase currentCost over time, ensuring it doesn't exceed MaxCost
        if (currentCost < MaxCost)
        {
            currentCost += costIncreaseRate * Time.deltaTime;
            currentCost = Mathf.Clamp(currentCost, 0, MaxCost); // Ensure it stays within bounds
        }

        Debug.Log(currentCost);
    }
}
