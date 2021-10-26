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
 *  
 *****************************************************************************/

/// <summary>
/// FSM states are:
/// FSM_ATTACK, FSM_EVADE, FSM_IDLE, FSM_PATROL
/// FSM bool switches are:
/// playerInReach, playerSpotted, gettingHit
/// </summary>

public class FSM : StateMachineBehaviour
{
    #region important variables accessible from inheriting subclass behaviours
    [SerializeField]
    protected GameObject gameObject;

    [SerializeField]
    protected NavMeshAgent navMeshAgent;

    protected Vector3[] wayPoints;
    protected int wayPointsAmount;
    #endregion

    private GameObject groundRef;
    private Ground ground;
    /// <summary>
    /// Assigning waypoints relative to the size of the ground, ignores obstacles
    /// They are only assigned, as soon as the first enemy exits the IDLE state
    /// in order for the wayPoints to be already fully accessible in OnStateEnter() of PATROL
    /// </summary>
    public void AssignWayPoints()
    {
        //references needed to assign the waypoints
        groundRef = GameObject.FindGameObjectWithTag("Ground");
        ground = groundRef.GetComponent<Ground>();
        RandomSpawnpoint randomSpawnpointRef = groundRef.GetComponent<RandomSpawnpoint>();
        //create as many waypoints as the lesser of width and height
        wayPointsAmount = ground.GetAreaOfGround / (Mathf.Min(ground.GetCurrentWidthX, ground.GetCurrentHeightZ));
        //assign array to fill with wayPoints
        wayPoints = new Vector3[wayPointsAmount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = randomSpawnpointRef.ReturnValidSpawnPoint();
            //Instantiate wayPoints at the desired positions
            GameObject wayPoint = new GameObject("WayPoint");
            GameObject instantiatedWayPoint = Instantiate(wayPoint, wayPoints[i], Quaternion.identity);
            //Set WayPoints mother obj
            instantiatedWayPoint.transform.parent = GameObject.FindGameObjectWithTag("WayPointsMother").transform;
            //tagging
            instantiatedWayPoint.gameObject.tag = "WayPoint";
            Debug.Log("waypoint at " + i + " is: " + wayPoints[i]);
        }
    }

    /// <summary>
    /// Sets the current enemy as referenced GO
    /// </summary>
    /// <param name="_gameObject">the gameobject of current enemy</param>
    public void SetGO(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }

    public void SetNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        navMeshAgent = _navMeshAgent;
    }

    
}
