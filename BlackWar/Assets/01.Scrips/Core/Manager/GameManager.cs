using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Cost")]
    public float MaxCost = 100;
    public float currentCost;

    [Header("Time")]
    public float time;
    public bool TimeTick;

    public bool Win; //우리가 이길까

    // Increase cost by 1 every second
    public float costIncreaseRate = 1f; // 1 cost per second

    public void Start()
    {
        currentCost = 0;
        TimeTick = true;
    }

    public void Update()
    {
        // Increase currentCost over time, ensuring it doesn't exceed MaxCost
        if (currentCost < MaxCost)
        {
            currentCost += costIncreaseRate * Time.deltaTime;
            currentCost = Mathf.Clamp(currentCost, 0, MaxCost); // Ensure it stays within bounds
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentCost = MaxCost;
            Debug.Log(currentCost);
        }

        //Debug.Log(currentCost);

        if (TimeTick)
            time += Time.deltaTime;
    }

    public void EndGame(bool win)
    {
        TimeTick = false;
        SceneManager.LoadScene("UI");
        Win = win;
    }
}
