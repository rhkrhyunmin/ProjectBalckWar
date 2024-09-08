using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideFollow : MonoBehaviour
{
    public GameObject targetObject;  
    public Slider slider;           
    private Vector3 offset = new Vector3(0, -8, 0);  

    private Camera mainCamera;       

    void Start()
    {
        mainCamera = Camera.main;  
    }

    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.transform.position + offset);
        slider.GetComponent<RectTransform>().position = screenPosition;
    }
}
