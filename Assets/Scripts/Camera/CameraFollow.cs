using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraHolder;

    public float y;

    public float currentZoom;
    public float minZoom;
    public float maxZoom;

    public bool isTriggered = false;
    public float shakeTimer;

    private void Start()
    {
        
    }

    private void Update()
    {
        Follow(GameObject.FindGameObjectWithTag("Player"));
        ZoomInAndOut();

        
    }

    private void ZoomInAndOut()
    {
        if(currentZoom <= minZoom)
        {
            currentZoom = minZoom;
        }
        else if(currentZoom >= maxZoom)
        {
            currentZoom = maxZoom;
        }

        currentZoom += Input.mouseScrollDelta.y;

        Camera cam = gameObject.GetComponent<Camera>();
        cam.orthographicSize = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void Follow(GameObject player)
    {
        cameraHolder.position = player.transform.position + new Vector3(0 , y, -10);
    }

    public void CameraShake(float intensity)
    {
        float randomX;
        float randomY;

        randomX = Mathf.PerlinNoise(0, Time.time * intensity) * 2 - 1;
        randomY = Mathf.PerlinNoise(1, Time.time * intensity) * 2 - 1;

        transform.localPosition = new Vector3(randomX, randomY, -10) * 0.5f;
    }


}
