using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    
    
    int barricadeHP = 12;

    BarricadeRangeDetection rangeDetection;
    [SerializeField] GameObject hpBar;

    float timer;
    float damageRate = 1f;

    private void Start()
    {
        rangeDetection = gameObject.GetComponentInChildren<BarricadeRangeDetection>();
        hpBar.GetComponent<Slider>().maxValue = barricadeHP;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        DestroyEnemy();

        DestroyBarricade();
        UpdateHPBar();
    }

    private void DestroyBarricade()
    {
        if(barricadeHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyEnemy()
    {
        /*if(rangeDetection.inRange && timer > damageRate)
        {
            timer = 0;
            Destroy(rangeDetection.closestEnemy);
            barricadeHP--;
        }*/

        if(rangeDetection.inRange && timer > damageRate)
        {
            timer = 0;
            Enemy closestEnemy = rangeDetection.closestEnemy.GetComponent<Enemy>();

            closestEnemy.Hit();
            barricadeHP--;
        }
    }

    private void UpdateHPBar()
    {
        
        hpBar.GetComponent<Slider>().value = barricadeHP;
    }
}
