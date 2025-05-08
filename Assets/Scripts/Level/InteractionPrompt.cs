using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionPrompt : MonoBehaviour
{
    public bool inRange;
    public UnityEvent eventTrigger;

    private void Update()
    {
        InteractionKey();
    }

    private void InteractionKey()
    {
        if (inRange && Input.GetKey(KeyCode.F))
        {
            eventTrigger.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
