using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrage : MonoBehaviour
{
    Enemy enemy;
    Animator animator;

    public int baseSpeed;
    public int enragedSpeed;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponentInParent<Animator>();

        baseSpeed = enemy.enemyData.speed;
        enragedSpeed = baseSpeed * 2;
    }

    private void Update()
    {
        BecomeEnraged();
    }

    private void BecomeEnraged()
    {
        if(enemy.hp <= (enemy.enemyData.maxhp / 2))
        {
            enemy.speed = enragedSpeed;
            animator.SetBool("angry", true);
        }

        
    }
}
