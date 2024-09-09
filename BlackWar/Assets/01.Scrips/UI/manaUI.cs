using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manaUI : MonoBehaviour
{
    private Text _manaText;

    private void Start()
    {
        _manaText = transform.Find("MaNa").GetComponent<Text>();
    }

    public void Update()
    {
        UpdateMana();
    }

    public void UpdateMana()
    {
        float currentMana = GameManager.Instance.currentCost;
        _manaText.text = "MANA : " + (int)currentMana + " / " + GameManager.Instance.MaxCost; //���⿡ ���� ���� �� �����ϸ� �ִ븶�� ���� ������ �ֱ�

        //_manaSlider.value = currentMana;
    }
}
