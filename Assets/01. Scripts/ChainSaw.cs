using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : Monster
{
    private void Start()
    {
        monsterAction += AttackAction;
    }



    private void AttackAction()
    {
        StartCoroutine(Co_Dash());
    }

    protected IEnumerator Co_Dash()
    {
        moveSpeed = 10;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 2;
    }
}
