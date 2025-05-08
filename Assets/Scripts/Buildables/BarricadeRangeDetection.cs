using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeRangeDetection : MonoBehaviour
{
    public bool inRange;
    int range;

    public GameObject closestEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inRange = true;
            closestEnemy = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inRange = true;
            closestEnemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inRange = false;
            closestEnemy = null;
        }
    }
}
