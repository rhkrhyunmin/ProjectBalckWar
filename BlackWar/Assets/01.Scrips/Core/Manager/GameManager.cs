using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float currentCost;

    public void Update()
    {
        Time.timeScale = currentCost;
    }
}
