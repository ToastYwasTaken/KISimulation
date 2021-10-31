using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM.cs
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
 *  26.10.2021  changed structure, FSM holds main information for all deriving behaviours
 *  28.10.2021  "" and added ore methods to inherit
 *  30.10.2021  added comments + multiple assign methods
 *  
 *****************************************************************************/

/// <summary>
/// FSM states are:
/// FSM_ATTACK, FSM_EVADE, FSM_IDLE, FSM_PATROL, FSM_GROUP
/// </summary>

public class FSM : StateMachineBehaviour
{
    private GameObject playerGO;
    private PlayerManager playerRef;

    private GameObject groundRef;
    private Ground ground;

    #region important variables accessible from inheriting subclass behaviours
    protected List<NavMeshAgent> currentlyGroupedEnemyAgents;
    protected Vector3 playerPosition;
    protected Vector3 agentDestination;

    protected NavMeshAgent navMeshAgent;

    protected WayPoints wayPoints;
    protected int wayPointsAmount;

    protected bool isGrouped;
    #endregion


    /// <summary>
    /// Assigning all agents, that act as group
    /// </summary>
    protected void AssignGroupedAgents(NavMeshAgent _currentAgent)
    {
        if (!currentlyGroupedEnemyAgents.Contains(_currentAgent)) 
        {
            currentlyGroupedEnemyAgents.Add(_currentAgent);
        }
    }

    /// <summary>
    /// Assigning references for te player to get access to the players position
    /// </summary>
    protected void AssignAllReferences()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        wayPoints = GameObject.FindGameObjectWithTag("WayPointsMother").GetComponent<WayPoints>();
        playerRef = playerGO.GetComponent<PlayerManager>();
    }

    /// <summary>
    /// Updates the players position
    /// </summary>
    protected void UpdatePlayerPosition()
    {
        playerPosition = playerRef.transform.position;
    }

    /// <summary>
    /// Assigns the NavMesh agent
    /// </summary>
    /// <param name="_navMeshAgent"></param>
    protected void AssignNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        navMeshAgent = _navMeshAgent;
    }

    /// <summary>
    /// Returns a random waypoint for the nav mesh agent
    /// </summary>
    /// <returns></returns>
    protected Vector3 SearchRandomWayPoint()
    {
        wayPointsAmount = wayPoints.wayPoints.Length;
        int randomCount = Random.Range(0, wayPointsAmount);
        Vector3 randomWayPoint = wayPoints.wayPoints[randomCount];
        return randomWayPoint;
    }

    /// <summary>
    /// Checks if the agent has reached his destination
    /// </summary>
    /// <returns>true, if destination reached</returns>
    protected bool DestinationReached()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }
}
