using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
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
 *  26.10.2021  added patrol behaviour preconfiguration
 *  27.10.2021  added patrol behaviour
 *  
 *****************************************************************************/
public class FSM_PATROL : FSM
{
    private float moveSpeed;
    private Vector3 agentDestination;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Setting agents first destination
        agentDestination = SearchNearestPatrolPoint();
        base.navMeshAgent.SetDestination(agentDestination);
        Debug.Log($"Enter | GO: {gameObject} GO in base: {base.gameObject}");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Updating agents destination after reaching it
        if(base.gameObject.transform.position == agentDestination)
        {
            agentDestination = SearchNearestPatrolPoint();
            base.navMeshAgent.SetDestination(agentDestination);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /// <summary>
    /// Returns nearest wayPoint for agent
    /// </summary>
    /// <returns>Vector3 wayPoint</returns>
    private Vector3 SearchNearestPatrolPoint()
    {
        int[] tempArr = new int[base.wayPoints.Length];
        int leastValue = tempArr[0];
        int leastValueIndex = 0;
        for (int i = 0; i < base.wayPoints.Length; i++)
        {
            //Search algorithm
            int wayPointX = (int)base.wayPoints[i].x;
            int wayPointZ = (int)base.wayPoints[i].z;
            int playerPositionX = (int)base.gameObject.transform.position.x;
            int playerPositionZ = (int)base.gameObject.transform.position.z;
            int tempValue = (wayPointX - playerPositionX)+(wayPointZ-playerPositionZ);
            tempArr[i] = tempValue;
        }
        
        for (int i = 0; i < tempArr.Length; i++)
        {
            //comparing tempValues to get the least -> that one is the nearest wayPoint
            int tempLeastValue = tempArr[i];
            leastValueIndex = i;
            //skip 0 bc that means the destination is already equal to the enemies current pos
            if(tempLeastValue == 0)
            {
                continue;
            }
            if (tempLeastValue < leastValue)
            {
                //override leastValue
                leastValue = tempLeastValue;
                leastValueIndex = i;
            }
        }
        Debug.Log("Least value: " + leastValue + " | index: " + leastValueIndex + " | corresponding wayPoint: " + base.wayPoints[leastValueIndex]);
        //get coordinates of wayPoints by getting same position as in tempArr
        return base.wayPoints[leastValueIndex];
    }
}
