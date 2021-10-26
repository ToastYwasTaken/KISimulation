using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_PATROL.cs
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
 *  07.10.2021  created
 *  26.10.2021  added patrol behaviour
 *  
 *****************************************************************************/
public class FSM_PATROL : FSM
{
    private float moveSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //can use base.wayPoints here and base.navMeshAgent
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void SearchNearestPatrolPoint()
    {
        for (int i = 0; i < base.wayPoints.Length; i++)
        {
            //Search algorithm: adding x and z coordinates then subtracting from enemy posiiton
            int wayPointX = (int)base.wayPoints[i].x;
            int wayPointZ = (int)base.wayPoints[i].z;
            int playerPositionX = (int)base.gameObject.transform.position.x;
            int playerPositionZ = (int)base.gameObject.transform.position.z;
        }
    }
}
