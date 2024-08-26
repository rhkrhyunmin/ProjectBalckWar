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

    // ���� ���� �Լ�
    public void SetState(State newState)
    {
        if (newState == currentState) return;

        // ���� ���¸� ��Ȱ��ȭ
        animator.SetBool("is" + currentState, false);

        // ���� ���¸� ������Ʈ
        currentState = newState;

        // ���ο� ���¸� Ȱ��ȭ
        animator.SetBool("is" + newState, true);
    }
}
