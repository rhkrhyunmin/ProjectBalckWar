using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Header("Time")]
    public float time;
    [SerializeField] GameObject endUI;
    [SerializeField] Text timeText;

    private void Start()
    {
        endUI.SetActive(false);
    }

    public void EndGame()
    {
        endUI.SetActive(true);
        timeText.text = $"½Ã°£ : {(int)time}";
    }
}
