using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/******************************************************************************
 * Project: KISimulation
 * File: MySceneManager.cs
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
public class MySceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    // Update is called once per frame
    void Update()
    {
        if(player.PlayerHealth <= 0)
        {
            ChangeToDeathScene();
        }
    }

    public void ChangeToDeathScene()
    {
        SceneManager.LoadScene(1);
    }


}
