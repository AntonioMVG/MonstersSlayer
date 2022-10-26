using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // Speed movement
    public float parallaxEffect;

    private Transform cameraPos;
    private Vector3 cameraLastPosition;

    private void Start()
    {
        cameraPos = Camera.main.transform;
        cameraLastPosition = cameraPos.position;
    }

    // Recommended for camera movement, is the last update at the run
    private void LateUpdate()
    {
        Vector3 backgroundMove = cameraPos.position - cameraLastPosition;
        transform.position += new Vector3(backgroundMove.x * parallaxEffect, backgroundMove.y, 0);
        cameraLastPosition = cameraPos.position;
    }
}
