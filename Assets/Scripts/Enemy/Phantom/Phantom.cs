using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    SpriteRenderer phantomSr;
    public Sprite[] phantomSpritesRandom;

    private void Start()
    {
        RandomPhantomSprite();
    }

    private void RandomPhantomSprite()
    {
        phantomSr = gameObject.GetComponent<SpriteRenderer>();

        Sprite randomPhantomSprite = phantomSpritesRandom[Random.Range(0, phantomSpritesRandom.Length)];
        phantomSr.sprite = randomPhantomSprite;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player plr = collision.gameObject.GetComponent<Player>();
            plr.hitByPhantom = true;
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player plr = collision.gameObject.GetComponent<Player>();
            plr.hitByPhantom = true;
            
        }
    }


}
