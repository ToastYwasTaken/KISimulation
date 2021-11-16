using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_ATTACK.cs
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
 *  30.10.2021  minor changes
 *  
 *****************************************************************************/
public class FSM_ATTACK : FSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        AssignAllReferences();
        UpdatePlayerPosition();
        //Get enemy group
        //Dictionary<List<Enemy>, int> groups = EnemyGroups.ReturnAllGroups();
        if (true)
        {

        }
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        agentDestination = playerPosition;
        navMeshAgent.SetDestination(agentDestination);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdatePlayerPosition();
        agentDestination = playerPosition;
        if (DestinationReached())
        {
            navMeshAgent.SetDestination(agentDestination);
        }

    }

}
