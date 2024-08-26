using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseUI : MonoBehaviour
{
    public List<Button> buttons; // 버튼 리스트
    public GameObject Spawner; // 유닛이 스폰될 위치
    public PlayerCost playerCost; // PlayerCost 스크립트 참조

    private void Start()
    {
        // 버튼에 클릭 이벤트를 추가합니다.
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
            // 인덱스에 해당하는 CostType 가져오기
            CostType costType = GetCostTypeByIndex(index);

            // 해당 CostType의 비용을 가져옴
            float cost = playerCost.GetCost(costType);

            // GameManager의 현재 비용과 비교
            if (GameManager.Instance.currentCost >= cost)
            {
                // 인덱스를 통해 적절한 PoolType을 선택합니다.
                PoolType type = GetPoolTypeByIndex(index);
                PoolManager.Instance.Pop(type, Spawner.transform.position);

                // 비용을 차감 (예시로 비용을 차감)
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
                return CostType.None; // 예외 처리용
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
