using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : PoolableMono
{
    protected Animator animation;

    private void Start()
    {
        animation = GetComponent<Animator>();
    }
}
