using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject LoseUI;
    [SerializeField] Text timeText;
    [SerializeField] Text timeText2;

    private void Start()
    {
        if (GameManager.Instance.Win)
        {
            WinUI.SetActive(true);
            LoseUI.SetActive(false);
        }
        else
        {
            LoseUI.SetActive(true);
            WinUI.SetActive(false);
        }
        int time = (int)GameManager.Instance.time;
        timeText.text = time < 60
            ? $"시간 : {time:D2}"
            : $"시간 : {time / 60}:{time % 60:D2}";

        timeText2.text = time < 60
            ? $"시간 : {time:D2}"
            : $"시간 : {time / 60}:{time % 60:D2}";
    }
    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    int time = (int)GameManager.Instance.time;
        //    timeText.text = time < 60
        //        ? $"시간 : {time:D2}"
        //        : $"시간 : {time / 60}:{time % 60:D2}";
        //}
    }
}
