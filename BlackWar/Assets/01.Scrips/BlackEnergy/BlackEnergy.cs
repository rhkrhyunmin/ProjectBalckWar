using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackEnergy : MonoBehaviour
{
    public Slider GageSlider;

    public void CollectGage()
    {
        if (GameManager.Instance.currentCost < GameManager.Instance.MaxCost)
        {
            GameManager.Instance.currentCost += Time.deltaTime;

            if (GameManager.Instance.currentCost > GameManager.Instance.MaxCost)
            {
                GameManager.Instance.currentCost = GameManager.Instance.MaxCost;
            }
        }
    }

    public void UseGage(float hire)
    {
        GameManager.Instance.currentCost -= hire;

        if(hire > GameManager.Instance.currentCost)
        {
            //구매 불가능 하게 하기
            Debug.Log("구매 불가능");
        }
    }
}
