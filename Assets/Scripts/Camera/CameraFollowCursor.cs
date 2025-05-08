using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCursor : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    Vector2 startPos;
    Vector3 mousePosition;
    Vector3 worldMousePosition;
    Vector3 mousePosOffset = new Vector3(0, 0, 10);
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothSpeed;
    [SerializeField] GameObject followLight;
    [SerializeField] GameObject backgroundImage;


    private void Start()
    {
        startPos = backgroundImage.transform.position;
    }

    private void Update()
    {
        mousePosition = mainCam.ScreenToViewportPoint(Input.mousePosition);
        worldMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        float x = Mathf.Lerp(backgroundImage.transform.position.x, startPos.x + (mousePosition.x * smoothSpeed), 2f * Time.deltaTime);
        float y = Mathf.Lerp(backgroundImage.transform.position.y, startPos.y + (mousePosition.y * smoothSpeed), 2f * Time.deltaTime);


        backgroundImage.transform.position = new Vector2 (x, y);
        


        if(followLight != null)
        {
            followLight.transform.position = worldMousePosition + new Vector3(0, 0, 10);
        }

    }
}
