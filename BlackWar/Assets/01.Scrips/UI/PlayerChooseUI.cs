using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerChooseUI : MonoBehaviour
{
    public List<Button> buttons; // 버튼을 담을 리스트

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
            PoolManager.Instance.Pop(PoolType.knight, Vector3.zero);
        }
        else
        {
            
        }
    }
}
