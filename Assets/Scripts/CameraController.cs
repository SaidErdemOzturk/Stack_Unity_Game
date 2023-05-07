using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    private Vector3 cameraPos;
    private float smoothSpeed=0.1F;

    void Update()
    {
        if (transform.position.y > ball.transform.position.y+2)
        {
            cameraPos=new Vector3 (transform.position.x, ball.transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y+2, -10);
        }
    }
}
