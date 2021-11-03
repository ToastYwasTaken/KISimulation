using System.Collections;
using System.Collections.Generic;
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
 *  
 *****************************************************************************/
public class FSM_GROUP : FSM
{
    private int numOfGroup = 1;
    private Enemy otherEnemy;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisEnemy = animator.GetComponent<Enemy>();
        otherEnemy = thisEnemy.enemyInReach;
        //GroupEnemies();
        //AssignAllReferences();
        //AssignNavMeshAgent(animator.GetComponent<NavMeshAgent>());
        animator.SetBool("nextToOtherEnemy", false);
    }

    //private void GroupEnemies()
    //{
    //    Debug.Log("Grouping enemies");
    //    GameObject newEmptyMother = new GameObject();
    //    newEmptyMother.AddComponent<Enemy>();
    //    motherOfEnemies = newEmptyMother.GetComponent<Enemy>();
    //    Instantiate(newEmptyMother);
    //    newEmptyMother.transform.position = new Vector3(0, 0, 0);
    //    newEmptyMother.transform.name = "MotherOfGroup" + numOfGroup.ToString();
    //    newEmptyMother.transform.tag = "MotherOfGroup";
    //    thisEnemy.transform.parent = newEmptyMother.transform;
    //    otherEnemy.transform.parent = newEmptyMother.transform;
    //    numOfGroup++;
    //}
}

