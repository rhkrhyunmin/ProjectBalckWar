using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private State currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 상태 설정 함수
    public void SetState(State newState)
    {
        if (newState == currentState) return;

        // 기존 상태를 비활성화
        animator.SetBool("is" + currentState, false);

        // 현재 상태를 업데이트
        currentState = newState;

        // 새로운 상태를 활성화
        animator.SetBool("is" + newState, true);
    }
}
