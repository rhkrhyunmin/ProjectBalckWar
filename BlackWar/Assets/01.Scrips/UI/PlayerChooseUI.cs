using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseUI : MonoBehaviour
{
    public List<Button> buttons; // ��ư ����Ʈ
    public GameObject Spawner; // ������ ������ ��ġ
    public PlayerCost playerCost; // PlayerCost ��ũ��Ʈ ����

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnButtonClick(Button button)
    {
        int index = buttons.IndexOf(button);

        if (index >= 0 && index < buttons.Count)
        {
            // �ε����� �ش��ϴ� CostType ��������
            CostType costType = GetCostTypeByIndex(index);

            // �ش� CostType�� ����� ������
            float cost = playerCost.GetCost(costType);

            // GameManager�� ���� ���� ��
            if (GameManager.Instance.currentCost >= cost)
            {
                // �ε����� ���� ������ PoolType�� �����մϴ�.
                PoolType type = GetPoolTypeByIndex(index);
                PoolManager.Instance.Pop(type, Spawner.transform.position);

                // ����� ���� (���÷� ����� ����)
                GameManager.Instance.currentCost -= cost;
            }
            else
            {
                Debug.Log("Not enough cost to spawn.");
            }
        }
    }

    private CostType GetCostTypeByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return CostType.KnightCost;
            case 1:
                return CostType.ArcherCost;
            case 2:
                return CostType.NinjaCost;
            case 3:
                return CostType.SpearmanCost;
            default:
                Debug.LogError("Invalid index for cost type.");
                return CostType.None; // ���� ó����
        }
    }

    private PoolType GetPoolTypeByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return PoolType.knight;
            case 1:
                return PoolType.Archer;
            case 2:
                return PoolType.Ninja;
            case 3:
                return PoolType.Spearman;
            default:
                Debug.LogError("Invalid index for pool type.");
                return PoolType.None;
        }
    }
}
