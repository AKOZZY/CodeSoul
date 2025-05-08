using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector3 target;
    Vector2 startPos;
    
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothSpeed;
 
    [SerializeField] GameObject backgroundImage;


    private void Start()
    {
        startPos = backgroundImage.transform.position;
        
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

        float x = Mathf.Lerp(backgroundImage.transform.position.x, (target.x * smoothSpeed), 2f * Time.deltaTime);
        //float y = Mathf.Lerp(backgroundImage.transform.position.y, startPos.y + (target.y * smoothSpeed), 2f * Time.deltaTime);


        backgroundImage.transform.position = new Vector2(x, -3);

    }
}
