using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Cost")]
    public float MaxCost;
    public float currentCost;

    public void Start()
    {
        currentCost = 0;
    }

    public void Update()
    {
        Time.timeScale = currentCost;
    }
}
