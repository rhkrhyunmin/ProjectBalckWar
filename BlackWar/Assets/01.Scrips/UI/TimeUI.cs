using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] GameObject endUI;
    [SerializeField] Text timeText;

    private void Start()
    {
        int time = (int)GameManager.Instance.time;
        timeText.text = time < 60
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
