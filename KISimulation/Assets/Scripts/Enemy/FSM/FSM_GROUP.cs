using UnityEngine;
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

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisEnemy = animator.GetComponent<Enemy>();
        otherEnemy = thisEnemy.enemyInReach;
        if (!thisEnemy.IsGrouped && !otherEnemy.IsGrouped)
        {
            //if not instantiated -> create new enemyGroup & add thisEnemy
            if (currentEnemyGroup == null || currentEnemyGroup.Size == 0)
            {
                string newGroupName = "EnemyGroup_" + groupCount.ToString();
                currentEnemyGroup = new EnemyGroup(newGroupName);
                currentEnemyGroup.AddMember(thisEnemy);
                currentEnemyGroup.AddMember(otherEnemy);
                thisEnemy.IsGrouped = true;
                otherEnemy.IsGrouped = true;
                Debug.Log($"Created new enemyGroup: {newGroupName} and added enemy {thisEnemy.name}");
                //animator.SetBool("playerInReach", true);
                //otherEnemy.GetComponent<Animator>().SetBool("playerInReach", true);
            }

            //add this Enemy if not already in enemyGroup
            if (!currentEnemyGroup.GroupMembers.Contains(thisEnemy))
            {
                currentEnemyGroup.AddMember(thisEnemy);
                thisEnemy.IsGrouped = true;
                Debug.Log($"added {thisEnemy} to {currentEnemyGroup}");
            }
            //add other Enemy if not already in enemyGroup
            if (!currentEnemyGroup.GroupMembers.Contains(otherEnemy))
            {
                currentEnemyGroup.AddMember(otherEnemy);
                otherEnemy.IsGrouped = true;
            }
        }

        //change states
        for (int i = 0; i < currentEnemyGroup.Size; i++)
        {
            currentEnemyGroup.GroupMembers[i].anim.SetBool("playerInReach", true);
        }

        currentEnemyGroup.DisplayEnemyGroup();

    }

}

