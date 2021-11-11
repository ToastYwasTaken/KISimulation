using UnityEngine;
using UnityEngine.AI;
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
    private int groupCount = 1;
    private EnemyGroup currentEnemyGroup;
    public EnemyGroup CurrentEnemyGroup { get => currentEnemyGroup; set => currentEnemyGroup = value; }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssignAllReferences();
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        thisEnemy = animator.GetComponent<Enemy>();
        otherEnemy = thisEnemy.EnemyInReach;
        if (!thisEnemy.IsGrouped && !otherEnemy.IsGrouped)
        {
            //if not instantiated -> create new enemyGroup & add thisEnemy and otherEnemy
            if (CurrentEnemyGroup == null || CurrentEnemyGroup.Size == 0)
            {
                string newGroupName = "EnemyGroup_" + groupCount.ToString();
                CurrentEnemyGroup = new EnemyGroup(newGroupName);
                CurrentEnemyGroup.AddMember(thisEnemy);
                CurrentEnemyGroup.AddMember(otherEnemy);
                thisEnemy.IsGrouped = true;
                otherEnemy.IsGrouped = true;
                //Debug.Log($"Created new enemyGroup: {newGroupName} and added enemy {thisEnemy.name}");
                //animator.SetBool("playerInReach", true);
                //otherEnemy.GetComponent<Animator>().SetBool("playerInReach", true);
            }
        }
        //add this Enemy if not already in enemyGroup
        if (!CurrentEnemyGroup.GroupMembers.Contains(thisEnemy))
        {
            CurrentEnemyGroup.AddMember(thisEnemy);
            thisEnemy.IsGrouped = true;
            Debug.Log($"added {thisEnemy} to {CurrentEnemyGroup}");
        }
        //add other Enemy if not already in enemyGroup
        if (!CurrentEnemyGroup.GroupMembers.Contains(otherEnemy))
        {
            CurrentEnemyGroup.AddMember(otherEnemy);
            otherEnemy.IsGrouped = true;
        }
        if (currentEnemyGroup != null)
        {
            CurrentEnemyGroup.DisplayEnemyGroup();
        }
        agentDestination = SearchRandomWayPoint();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CurrentEnemyGroup != null)
        {
            for (int i = 0; i < CurrentEnemyGroup.Size; i++)
            {
                NavMeshAgent currentNavMeshAgent = CurrentEnemyGroup.GroupMembers[i].GetComponent<NavMeshAgent>();
                if (currentNavMeshAgent.remainingDistance <= currentNavMeshAgent.stoppingDistance)  //Destination reached
                {
                    agentDestination = SearchRandomWayPoint();
                }
                currentNavMeshAgent.SetDestination(agentDestination);
            }
        }
    }

}

