using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    GameObject btnPrefab;
    [SerializeField] RectTransform btnParent;

    private void Start()
    {
        btnPrefab = Resources.Load<GameObject>("Button");
        CreateBtn();
    }

    private void CreateBtn()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(btnPrefab, btnParent);
        }
    }
}
