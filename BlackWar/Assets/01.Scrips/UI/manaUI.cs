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
        _manaText.text = currentMana + " / "; //���⿡ ���� ���� �� �����ϸ� �ִ븶�� ���� ������ �ֱ�

        _manaSlider.value = currentMana;
    }
}
