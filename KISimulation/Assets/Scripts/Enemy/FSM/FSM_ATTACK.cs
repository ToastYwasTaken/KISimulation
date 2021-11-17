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
    private Enemy thisEnemy;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisEnemy = animator.GetComponent<Enemy>();
        AssignAllReferences();
        UpdatePlayerPosition();
        //Get enemy group
        if(EnemyGroups.groupCount > 0)
        for (int i = 0; i < EnemyGroups.groupCount; i++)
        {
            List<Enemy> currentList = EnemyGroups.GetCurrentList(i);
                //If enemy is grouped
                if (currentList.Contains(thisEnemy))
                {
                    for (int j = 0; j < currentList.Count; j++)
                    {
                        //change status of all other members too
                        if(currentList[j] != thisEnemy)
                        {
                            currentList[j].anim.SetBool("playerSpotted", true);
                        }
                    }
                }
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
