using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_EVADE.cs
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
 *  17.11.2021  implemented
 *  
 *****************************************************************************/
public class FSM_EVADE : FSM
{
    private Enemy thisEnemy;
    private float sqrDistance;
    private float minDistanceToEvadeSuccessfully = 100f; //to tweak
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisEnemy = animator.GetComponent<Enemy>();
        AssignAllReferences();
        UpdatePlayerPosition();
        //Get enemy group
        if (EnemyGroups.groupCount > 0)
            for (int i = 0; i < EnemyGroups.groupCount; i++)
            {
                List<Enemy> currentList = EnemyGroups.GetCurrentList(i);
                //If enemy is grouped
                if (currentList.Contains(thisEnemy))
                {
                    for (int j = 0; j < currentList.Count; j++)
                    {
                        //change status of all other members too
                        if (currentList[j] != thisEnemy)
                        {
                            currentList[j].anim.SetBool("gettingHit", true);
                        }
                    }
                }
            }
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        //evade
        agentDestination = SearchPositionToEvade();
        navMeshAgent.SetDestination(agentDestination);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdatePlayerPosition();
        //evade until distance between player and enemy is enough
        float lesser = Mathf.Min(animator.transform.position.sqrMagnitude, playerPosition.sqrMagnitude);
        float bigger = Mathf.Max(animator.transform.position.sqrMagnitude, playerPosition.sqrMagnitude);
        sqrDistance = bigger - lesser;
        //not evaded yet
        if(sqrDistance < minDistanceToEvadeSuccessfully)
        {
            if (DestinationReached())
            {
                //evade
                agentDestination = SearchPositionToEvade();
                navMeshAgent.SetDestination(agentDestination);
            }
            navMeshAgent.SetDestination(agentDestination);
        }
    }

}
