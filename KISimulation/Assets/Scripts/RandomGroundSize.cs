using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: RandomGroundSize.cs
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
 *  10.10.2021  created
 *  
 *****************************************************************************/
public class RandomGroundSize : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    private int maxScale;

    private const int minScale = 1;
    private int randomX, randomZ;


    void Awake()
    {
        randomX = Random.Range(minScale, maxScale);
        randomZ = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomX, 1, randomZ);
    }
}
