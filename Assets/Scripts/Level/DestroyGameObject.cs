using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float timeToDestroy;

    private void Start()
    {
        DestroyObject(timeToDestroy);
    }

    public void DestroyObject(float time)
    {
        Destroy(gameObject, time);
    }
}
