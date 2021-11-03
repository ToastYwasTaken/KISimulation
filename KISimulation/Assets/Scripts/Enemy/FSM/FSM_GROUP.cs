using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_GROUP.cs
 * Version: 1.01
 * Autor:  Franz M�rike (FM);
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
 *  
 *****************************************************************************/
public class FSM_GROUP : FSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisEnemy = animator.GetComponent<Enemy>();
        AssignAllReferences();
        AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        Enemy otherEnemy = thisEnemy.EnemyInReach;
        animator.SetBool("nextToOtherEnemy", false);
        animator.SetBool("playerInReach", true);
    }
}
