using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: Enemy.cs
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
 *  17.10.2021  added boid behaviour
 *  20.10.2021  added method description
 *  30.10.2021  deleted boids added Check methods and some variables
 *              moved wayPoints assigning to own script -> calling from gameManager
 *  
 *****************************************************************************/
public class Enemy : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private Rigidbody rb;
    
    public float ehealth = 100f;

    private MyGameManager gameManager;

    //float speedMultiplier = 1f, maxVelocity = 10f;

    #region Detection stuff
    private GameObject playerGO;
    private PlayerManager playerRef;
    private Enemy[] otherEnemies;
    private Enemy enemyInReach;
    private bool isGrouped;  //for FSM_GROUP

    private float radiusPlayerInReach;
    private float radiusNextToOtherEnemy;
    private float playerSpottedAngle;

    private float currentAngle;

    private bool idling;
    private bool patroling;
    private bool attacking;
    private bool evading;
    #endregion

    public Vector3 Velocity { get => rb.velocity; }
    public bool IsGrouped { get => isGrouped; set => isGrouped = value; }

    private void Awake()
    {
        //setting default stuff
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRef = playerGO.GetComponent<PlayerManager>();
        otherEnemies = FindObjectsOfType<Enemy>();
        gameManager = FindObjectOfType<MyGameManager>();
        radiusPlayerInReach = gameManager.RadiusPlayerInReach;
        radiusNextToOtherEnemy = gameManager.RadiusNextToOtherEnemy;
        playerSpottedAngle = gameManager.PlayerSpottedAngle;
        SetFSM_IDLE();

        //Set Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //Can be deleted later //debugs inaccurate
        //GameObject debugSpherePrefab = Resources.Load("DebugSphere") as GameObject;
        //debugSpherePrefab.transform.localScale = new Vector3(radiusPlayerInReach*radiusPlayerInReach, radiusPlayerInReach*radiusPlayerInReach, radiusPlayerInReach*radiusPlayerInReach);
        //Instantiate(debugSpherePrefab, this.transform);
        //GameObject debugSpherePrefab2 = Resources.Load("DebugSphere2") as GameObject;
        //debugSpherePrefab2.transform.localScale = new Vector3(radiusNextToOtherEnemy*radiusNextToOtherEnemy, radiusNextToOtherEnemy*radiusNextToOtherEnemy, radiusNextToOtherEnemy*radiusNextToOtherEnemy);
        //Instantiate(debugSpherePrefab2, this.transform);

    }
    private void Update()
    {
        CheckState();
    }

    #region StateSwitches

    private void SetFSM_IDLE()
    {
        //set all per default to false
        anim.SetBool("playerInReach", false);
        anim.SetBool("playerSpotted", false);
        anim.SetBool("gettingHit", false);
        anim.SetBool("nextToOtherEnemy", false);
        idling = true;
    }
    private void SetFSM_PATROL()
    {
        anim.SetBool("playerInReach", true);
        patroling = true;
    }

    private void SetFSM_ATTACK()
    {
        anim.SetBool("playerSpotted", true);
        attacking = true;
    }

    private void SetFSM_EVADE()
    {
        anim.SetBool("gettingHit", true);
        evading = true;
    }

    private void SetFSM_GROUP()
    {
        anim.SetBool("nextToOtherEnemy", true);
        //go directly back to patroling
        SetFSM_PATROL();
    }
    #endregion

    private void CheckState()
    {
        Debug.Log($"IDLING: {idling} | PATROLING: {patroling} | ATTACKING: {attacking} | EVADING: {evading}");
        if (idling)
        {
            CheckForPlayerInReach();
        }
        else if (patroling)
        {
            CheckForNextToOtherEnemy();
            CheckForPlayerSpotted();
        }
        else if (attacking)
        {
            CheckForGettingHit();
        }else if (evading)
        {
            CheckForEvadedSuccessfull();
        }
    }

    #region StateCheckers

    /// <summary>
    /// Checks if the enemy has evaded successfully
    /// </summary>
    private void CheckForEvadedSuccessfull()
    {
        Vector3 directionToPlayer = playerRef.transform.position - this.transform.position;
        //Raycast from enemy to player | if obstacle is between -> evaded successfully
        Physics.Raycast(this.transform.position, directionToPlayer, out RaycastHit hit);
        if((hit.transform.tag == "Obstacle"))
        {
            Debug.Log("evading was successfull");
            evading = false;
            SetFSM_PATROL();
            return;
        }

    }

    /// <summary>
    /// Checks if the enemy is getting hit by the player
    /// </summary>
    private void CheckForGettingHit()
    {
        if(this.ehealth <= 50)
        {
            Debug.Log("Evading");
            attacking = false;
            patroling = false;
            SetFSM_EVADE();
            return;
        }
    }

    /// <summary>
    /// Checks if the enemy can see the player 
    /// </summary>
    private void CheckForPlayerSpotted()
    {
        Vector3 enemyToPlayerVector = this.transform.position - playerRef.transform.position;
        //ignore y coordinate
        enemyToPlayerVector.y = 0f;
        Vector3 playerVectorForward = playerRef.transform.forward;
        float dotProduct = Vector3.Dot(enemyToPlayerVector.normalized, playerVectorForward.normalized);
        currentAngle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        //Debug.Log("currentAngle: " + currentAngle);
        if (currentAngle <= playerSpottedAngle)
        {
            Debug.Log("Player spotted -> attacking");
            patroling = false;
            SetFSM_ATTACK();
            return;
        }
    }

    /// <summary>
    /// Checks if the enemy is near another enemy to group up with him
    /// </summary>
    private void CheckForNextToOtherEnemy()
    {
        float enemyDistanceToCheck;
        for (int i = 0; i < otherEnemies.Length; i++)
        {
            if(otherEnemies[i].gameObject == this.gameObject)
            {
                continue;
            }
            enemyDistanceToCheck = (otherEnemies[i].transform.position-this.transform.position).sqrMagnitude;
            //Debug.Log("distance between enemies: " + enemyDistanceToCheck + " radius * radius: " + radiusNextToOtherEnemy*radiusNextToOtherEnemy);
            //Change state to FSM_GROUP when in reach 
            if (enemyDistanceToCheck < radiusNextToOtherEnemy * radiusNextToOtherEnemy)
            {
                //assign the enemy thats in reach
                enemyInReach = otherEnemies[i];
                Debug.Log("Other enemy " + enemyInReach + " in reach");
                patroling = false;
                SetFSM_GROUP();
                return;
            }
        }
    }

    /// <summary>
    /// Checks if the player is in reach to start patroling
    /// </summary>
    private void CheckForPlayerInReach()
    {
        //distance between player and enemy
        float distanceToCheck = (playerRef.transform.position - this.transform.position).sqrMagnitude;
        //Debug.Log("distance to check: " + distanceToCheck + " | radius*radius: " + radiusPlayerInReach*radiusPlayerInReach);
        if (distanceToCheck < (radiusPlayerInReach * radiusPlayerInReach))
        {
            Debug.Log("Player is in reach");
            idling = false;
            SetFSM_PATROL();
            return;
        }
        else SetFSM_IDLE();
    }
    #endregion

    private void OnDrawGizmos()
    {
        //DEBUG: shows player spotting mechanism
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerRef.transform.position, this.transform.position);
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2);
    }

}
