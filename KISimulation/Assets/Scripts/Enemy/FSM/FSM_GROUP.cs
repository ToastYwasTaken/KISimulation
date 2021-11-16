using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_GROUP.cs
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
 *  25.10.2021  re-created
 *  30.10.2021  added basic assignments
 *  06.11.2021  tried another possible fix for grouping enemies
 *  
 *****************************************************************************/
public class FSM_GROUP : FSM
{
    private Enemy thisEnemy, otherEnemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssignAllReferences();
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        thisEnemy = animator.GetComponent<Enemy>();
        otherEnemy = thisEnemy.EnemyInReach;
        //Is any of the current colliding enemies not grouped?
        if (!thisEnemy.IsGrouped && !otherEnemy.IsGrouped)
        {
            //NEW enemyGroup & add thisEnemy and otherEnemy
            if (EnemyGroups.subGroupList == null || EnemyGroups.groupCount == 0)
            {
                EnemyGroups.AddEnemyToCurrentList(thisEnemy);
                EnemyGroups.AddEnemyToCurrentList(otherEnemy);
                EnemyGroups.AddCurrentGroupToAllGroups(EnemyGroups.subGroupList);
                thisEnemy.IsGrouped = true;
                otherEnemy.IsGrouped = true;
                //Debug.Log($"Created new enemyGroup: {newGroupName} and added enemy {thisEnemy.name}");
                //animator.SetBool("playerInReach", true);
                //otherEnemy.GetComponent<Animator>().SetBool("playerInReach", true);
            }
        }
        if (EnemyGroups.groupCount != 0)
        {
            int thisEnemyNumInList = EnemyGroups.GetGroupNumberOfEnemy(thisEnemy);
            int otherEnemyNumInList = EnemyGroups.GetGroupNumberOfEnemy(otherEnemy);
            for (int i = 0; i < EnemyGroups.groupCount; i++)
            {
                List<Enemy> curList = EnemyGroups.GetCurrentList(i);
                //add this Enemy if not already in enemyGroup
                if (!curList.Contains(thisEnemy))
                {
                    EnemyGroups.AddEnemyToCurrentList(thisEnemy);
                    thisEnemy.IsGrouped = true;
                    Debug.Log($"added {thisEnemy} to List {thisEnemyNumInList}");
                }
                //add other Enemy if not already in enemyGroup
                else if (!curList.Contains(otherEnemy))
                {
                    EnemyGroups.AddEnemyToCurrentList(otherEnemy);
                    otherEnemy.IsGrouped = true;
                    Debug.Log($"added {otherEnemy} to {otherEnemyNumInList}");
                }
                else return;
            }

            EnemyGroups.DisplayGroups();
        }
        agentDestination = SearchRandomWayPoint();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: individually update destinations
        if (EnemyGroups.mainGroupList != null)
        {
            for (int i = 0; i < EnemyGroups.enemyCount; i++)
            {
                NavMeshAgent currentNavMeshAgent = EnemyGroups.subGroupList[i].GetComponent<NavMeshAgent>();
                if (currentNavMeshAgent.remainingDistance <= currentNavMeshAgent.stoppingDistance)  //Destination reached
                {
                    agentDestination = SearchRandomWayPoint();
                }
                currentNavMeshAgent.SetDestination(agentDestination);
            }
        }
    }

}

