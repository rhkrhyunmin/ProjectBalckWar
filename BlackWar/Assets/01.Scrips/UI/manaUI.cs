using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manaUI : MonoBehaviour
{
    public TextMeshProUGUI _manaText;
    public Slider _manaSlider;

    public void Update()
    {
        UpdateMana();
    }

    public void UpdateMana()
    {
        float currentMana = GameManager.Instance.currentCost;
        _manaText.text = currentMana + " / "; //여기에 추후 넣을 성 업글하면 최대마나 증가 같은거 넣기

        _manaSlider.value = currentMana;
    }
}
