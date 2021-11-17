using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/******************************************************************************
 * Project: KISimulation
 * File: DeathScene.cs
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
 *  17.11.2021  created
 *  
 *****************************************************************************/
public class DeathScene : MonoBehaviour
{
    /// <summary>
    /// Simple button behaviour to change to main scene
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
