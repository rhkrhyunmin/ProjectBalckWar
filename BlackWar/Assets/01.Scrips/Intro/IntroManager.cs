using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI blinkingText; 
    private float blinkDuration = 1f; 

    void Start()
    {
        BlinkText();
    }

    void BlinkText()
    {
        blinkingText.DOFade(0, blinkDuration)
            .SetLoops(-1, LoopType.Yoyo) 
            .SetEase(Ease.InOutQuad);    
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene("Play");
    }
}
