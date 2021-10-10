using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: CameraManager.cs
 * Version: 1.01
 * Autor:  Franz Mörike (FM);
 * 
 * 
 * These coded instructions, statements, and computer programs contain
 * proprietary information of the author and are protected by Federal
 * copyright law. They may not be disclosed to third parties or copied
 * or duplicated in any form, in whole or in part, without the prior
 * written consent of the author.
 * 
 * ChangeLog
 * ----------------------------
 *  05.10.2021  created
 *  
 *****************************************************************************/
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
