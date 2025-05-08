using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Animator animator;
    TurretRangeDetection rangeDetection;

    float timer;
    float firerate = 1.40f;

    float degradeTimer;
    float degradeTime = 60.00f;

    private void Start()
    {
        rangeDetection = gameObject.GetComponentInChildren<TurretRangeDetection>();
    }

    private void Update()
    {
        Flip();
        Shoot();
        Degrade();
    }

    private void Degrade()
    {
        degradeTimer += Time.deltaTime;
        if (degradeTimer >= degradeTime)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        timer += Time.deltaTime;

        if(rangeDetection.inRange && firerate < timer)
        {
            timer = 0;
            animator.Play("TurretShooting");
            StartCoroutine(DealDamage(rangeDetection.closestEnemy));
        }
    }

    private void Flip()
    {
        if(rangeDetection.inRange)
        {
            if(rangeDetection.closestEnemy.transform.position.x < transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, -1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private IEnumerator DealDamage(GameObject enemy)
    {
        yield return new WaitForSeconds(1.09f);
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>().Hit();
            yield return new WaitForSeconds(.31f);
        }
    }
}
