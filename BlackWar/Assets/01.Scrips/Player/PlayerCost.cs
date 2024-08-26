using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 비용 타입을 정의하는 열거형
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
    // 각 타입에 따른 비용을 저장할 딕셔너리
    private Dictionary<CostType, float> costDictionary;

    // Start is called before the first frame update
    void Start()
    {
        // 딕셔너리 초기화 및 초기 비용 설정
        costDictionary = new Dictionary<CostType, float>
        {
            { CostType.KnightCost, 5f },
            { CostType.ArcherCost, 8f },
            { CostType.NinjaCost, 10f },
            { CostType.SpearmanCost, 13f},
        };
    }

    // 특정 타입의 코스트를 가져오는 함수
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
