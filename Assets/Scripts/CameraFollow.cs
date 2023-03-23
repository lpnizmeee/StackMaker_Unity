using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 playerCameraVecto3;
    void Start()
    {
        playerCameraVecto3 = PlayerCtrl.instance.transform.position - transform.position;
    }

    void LateUpdate()
    {
        transform.position = PlayerCtrl.instance.transform.position - playerCameraVecto3;
    }

}