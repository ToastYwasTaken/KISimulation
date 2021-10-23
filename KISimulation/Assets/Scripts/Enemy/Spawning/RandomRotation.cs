using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: RandomRotation.cs
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
 * 
 *  07.10.2021  created
 *  23.10.2021  renamed spawnRotation to rotation
 *              renamed GenerateRandomSpawnRotation() to GenerateRandomRotation()
 *              added method description
 *  
 *****************************************************************************/
public class RandomRotation : MonoBehaviour
{
    private Quaternion rotation;

    /// <summary>
    /// Generates a random rotation around y axis
    /// </summary>
    /// <returns>a Quaternion</returns>
    public Quaternion GenerateRandomRotation()
    {
        float rotationX = 0f, rotationZ = 0f;
        float rotationY = Random.Range(-1f, 1f), rotationMagnitudeW = Random.Range(-1f, 1f);

        rotation = new Quaternion(rotationX, rotationY, rotationZ, rotationMagnitudeW);
        return rotation;
    }

}
