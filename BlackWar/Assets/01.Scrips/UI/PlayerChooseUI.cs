using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerChooseUI : MonoBehaviour
{
    public List<Button> buttons;
    public GameObject Spawner;

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
            // 인덱스를 통해 적절한 PoolType을 선택합니다.
            PoolType type = GetPoolTypeByIndex(index);
            PoolManager.Instance.Pop(type, Spawner.transform.position);
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
                return PoolType.None; 
        }
    }
}
