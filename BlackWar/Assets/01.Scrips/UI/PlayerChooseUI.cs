using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CostType
{
    KnightCost,
    ArcherCost,
    NinjaCost,
    SpearmanCost,
    None,
}

public class PlayerChooseUI : MonoBehaviour
{
    public List<Button> buttons;
    public GameObject Spawner; 
    private Dictionary<CostType, float> costDictionary;

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        costDictionary = new Dictionary<CostType, float>
        {
            { CostType.KnightCost, 5f },
            { CostType.ArcherCost, 8f },
            { CostType.NinjaCost, 10f },
            { CostType.SpearmanCost, 13f},
        };
    }

    private void OnButtonClick(Button button)
    {
        int index = buttons.IndexOf(button);

        if (index >= 0 && index < buttons.Count)
        {
            var (costType, poolType) = GetCostAndPoolTypeByIndex(index);

            float cost = GetCost(costType);

            if (GameManager.Instance.currentCost >= cost)
            {
                // PoolManager�� ���� ����
                PoolManager.Instance.Pop(poolType, Spawner.transform.position);

                // ����� ����
                GameManager.Instance.currentCost -= cost;
            }
            else
            {
                Debug.Log("Not enough cost to spawn.");
            }
        }
    }

    // CostType�� PoolType�� ���ÿ� ��ȯ�ϴ� �Լ�
    private (CostType, PoolType) GetCostAndPoolTypeByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return (CostType.KnightCost, PoolType.knight);
            case 1:
                return (CostType.ArcherCost, PoolType.Archer);
            case 2:
                return (CostType.NinjaCost, PoolType.Ninja);
            case 3:
                return (CostType.SpearmanCost, PoolType.Spearman);
            default:
                return (CostType.None, PoolType.None);
        }
    }

    public float GetCost(CostType type)
    {
        if (costDictionary.TryGetValue(type, out float cost))
        {
            return cost;
        }
        else
        {
            return 0f;
        }
    }
}
