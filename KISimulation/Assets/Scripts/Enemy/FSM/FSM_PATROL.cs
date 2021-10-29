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
 *  28.10.2021  changed nearest wayPoint to random wayPoint
 *  
 *****************************************************************************/
public class FSM_PATROL : FSM
{
    private float moveSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Assigning wayPoints & references
        AssignWayPoints();
        AssignPlayerReferences();
        //Initializing agent and setting agents first destination
        agentDestination = SearchRandomWayPoint();
        SetNavMeshAgent(animator.gameObject.GetComponent<NavMeshAgent>());
        navMeshAgent.SetDestination(agentDestination);
        //Debug.Log($"Enter | GO: {gameObject} GO in base: {base.gameObject}");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //destination reached -> next wayPoint
        Debug.Log(animator.gameObject.transform.position + " | " + agentDestination);
        if (DestinationReached())//animator.gameObject.transform.position == agentDestination)
        {
            Debug.Log("destination reached");
            agentDestination = SearchRandomWayPoint();
            navMeshAgent.SetDestination(agentDestination);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }




}
