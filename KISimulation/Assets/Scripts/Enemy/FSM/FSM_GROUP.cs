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
    private List<Enemy> currentGroup = new List<Enemy>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssignAllReferences();
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        thisEnemy = animator.GetComponent<Enemy>();
        otherEnemy = thisEnemy.EnemyInReach;
        //Is any of the current colliding enemies not grouped?
        if (!thisEnemy.IsGrouped && !otherEnemy.IsGrouped)
        {
            //if not instantiated -> create new enemyGroup & add thisEnemy and otherEnemy
            if (currentGroup == null || currentGroup.Count == 0)
            {
                string newGroupName = "EnemyGroup_" + (EnemyGroups.size+1);
                currentGroup = new List<Enemy>();
                currentGroup.Add(thisEnemy);
                currentGroup.Add(otherEnemy);
                thisEnemy.IsGrouped = true;
                otherEnemy.IsGrouped = true;
                EnemyGroups.SaveCurrentGroup(currentGroup);
                //Debug.Log($"Created new enemyGroup: {newGroupName} and added enemy {thisEnemy.name}");
                //animator.SetBool("playerInReach", true);
                //otherEnemy.GetComponent<Animator>().SetBool("playerInReach", true);
            }
        }
        if (currentGroup != null)
        {        
            //add this Enemy if not already in enemyGroup
            if (!currentGroup.Contains(thisEnemy))
            {
                currentGroup.Add(thisEnemy);
                thisEnemy.IsGrouped = true;
                Debug.Log($"added {thisEnemy} to {currentGroup}");
            }
            //add other Enemy if not already in enemyGroup
            else if (!currentGroup.Contains(otherEnemy))
            {
                currentGroup.Add(otherEnemy);
                otherEnemy.IsGrouped = true;
                Debug.Log($"added {otherEnemy} to {currentGroup}");
            }
            for (int i = 0; i < currentGroup.Count; i++)
            {
                Debug.Log($"Group member {i} : {currentGroup[i].name}");
            }
        }
        agentDestination = SearchRandomWayPoint();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currentGroup != null)
        {
            for (int i = 0; i < currentGroup.Count; i++)
            {
                NavMeshAgent currentNavMeshAgent = currentGroup[i].GetComponent<NavMeshAgent>();
                if (currentNavMeshAgent.remainingDistance <= currentNavMeshAgent.stoppingDistance)  //Destination reached
                {
                    agentDestination = SearchRandomWayPoint();
                }
                currentNavMeshAgent.SetDestination(agentDestination);
            }
        }
    }

}

