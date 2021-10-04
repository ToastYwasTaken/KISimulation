using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    Vector3 offset;

    private float smoothFactor = 0.2f;
    private void LateUpdate()
    {
        //Smoothen the camera position calculation
        transform.position = Vector3.Lerp(transform.position, player.position + offset, smoothFactor);
    }
}
