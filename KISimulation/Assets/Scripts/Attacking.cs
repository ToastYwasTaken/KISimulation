using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: Attacking.cs
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
 *  02..11.2021  created
 *  
 *****************************************************************************/

public class Attacking : MonoBehaviour
{

    /// <summary>
    /// Hitting the enemy
    /// </summary>
    /// <param name="collision">enemy</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //TODO: input attack animation
                collision.gameObject.GetComponent<Enemy>().ehealth -= 20f;
            }
        }
    }
}
