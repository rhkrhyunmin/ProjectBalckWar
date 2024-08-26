using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� Ÿ���� �����ϴ� ������
public enum CostType
{
    KnightCost,
    ArcherCost,
    NinjaCost,
    SpearmanCost,
    None,
}

public class PlayerCost : MonoBehaviour
{
    // �� Ÿ�Կ� ���� ����� ������ ��ųʸ�
    private Dictionary<CostType, float> costDictionary;

    // Start is called before the first frame update
    void Start()
    {
        // ��ųʸ� �ʱ�ȭ �� �ʱ� ��� ����
        costDictionary = new Dictionary<CostType, float>
        {
            { CostType.KnightCost, 5f },
            { CostType.ArcherCost, 8f },
            { CostType.NinjaCost, 10f },
            { CostType.SpearmanCost, 13f},
        };
    }

    // Ư�� Ÿ���� �ڽ�Ʈ�� �������� �Լ�
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

    public void SetCost(CostType type, float cost)
    {
        if (costDictionary.ContainsKey(type))
        {
            costDictionary[type] = cost;
            Debug.Log($"{type} Cost updated to {cost}");
        }
        else
        {
            Debug.LogError($"Cost for {type} not found!");
        }
    }
}
