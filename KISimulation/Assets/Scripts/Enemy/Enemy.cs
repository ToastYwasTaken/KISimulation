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
 *  
 *****************************************************************************/
public class Enemy : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private Rigidbody rb;

    private FSM myFSMState;

    float speedMultiplier = 2f, maxVelocity = 10f;

    #region Detection stuff
    private GameObject playerGO;
    private PlayerManager playerRef;

    private float radiusPlayerInReach;
    private float radiusNextToOtherEnemy;
    private float playerSpottedAngle;

    private bool idling;
    private bool patroling;
    private bool groupingUp;
    private bool attacking;
    private bool evading;
    #endregion

    #region BOIDS
    BoidManager boidManager;

    List<Enemy> enemyBoids;

    private float cohesionRadius, alignmentRadius, separationRadius;
    private float cohesionForce, alignmentForce, separationForce, targetForce;
    #endregion
    public Vector3 Velocity { get => rb.velocity; }

    private void Awake()
    {
        //setting default stuff
        idling = true;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRef = playerGO.GetComponent<PlayerManager>();

        //Initialize FSM
        myFSMState = anim.GetBehaviour<FSM>();
        myFSMState.SetGO(this.gameObject);

        //Set Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //assign boids
        boidManager = GameObject.FindGameObjectWithTag("BoidManager").GetComponent<BoidManager>();

        //calculate radii and forces
        float radius = transform.localScale.x / 2f;
        cohesionRadius = radius * radius * 2f;
        alignmentRadius = radius * radius * 2f;
        separationRadius = radius * radius * radius;
        cohesionForce = boidManager.CohesionForce;
        alignmentForce = boidManager.AlignmentForce;
        separationForce = boidManager.SeparationForce;
        targetForce = boidManager.TargetForce;


    }
    private void Update()
    {
        CheckState();
    }

    #region Boids
    /// <summary>
    /// Applies the boid forces on their movement
    /// </summary>
    public void ApplyBoidsMovement()
    {
        Vector3 target = CalculateTarget(boidManager.PlayerPos);
        Vector3 separation = CalculateSeparation();
        Vector3 cohesion = CalculateCohesion();
        Vector3 alignment = CalculateAlignment();

        Vector3 totalForce = separation * separationForce + cohesion * cohesionForce + alignment * alignmentForce + target * targetForce;
        rb.AddForce(totalForce);
    }

    /// <summary>
    /// Calculates the target position relative to player position
    /// </summary>
    /// <param name="_playerPos">player position</param>
    /// <returns>target position</returns>
    private Vector3 CalculateTarget(Vector3 _playerPos)
    {
        Vector3 target = (_playerPos - transform.position).normalized * speedMultiplier;
        target -= rb.velocity;
        if(target.magnitude > maxVelocity)
        {
            target = target.normalized * maxVelocity;
        }
        return target;
    }

    /// <summary>
    /// Calculates separation between the boidds
    /// </summary>
    /// <returns>separation vector</returns>
    private Vector3 CalculateSeparation()
    {
        Vector3 separation = Vector3.zero;
        int numberOfBoidsTooClose = 0;
        for (int i = 0; i < enemyBoids.Count; i++)
        {
            //assign the enemies in state group which are next to each other
            Enemy neighbour = enemyBoids[i];
            Vector3 tempSeparation = transform.position - neighbour.transform.position;
            float distance = tempSeparation.magnitude;
            //calculate separation if near to another enemy
            if(distance > 0f && distance < separationRadius)
            {
                tempSeparation.Normalize();
                tempSeparation /= distance;
                separation += tempSeparation;
                numberOfBoidsTooClose++;
            }
        }
        //separating enemies if needed
        if (numberOfBoidsTooClose > 0)
        {
            Vector3 separationAvg = separation / numberOfBoidsTooClose;
            separationAvg = separationAvg.normalized * speedMultiplier;
            Vector3 target = separationAvg - rb.velocity;
            if (target.magnitude > maxVelocity)
            {
                target = target.normalized * maxVelocity;
            }
            return target;
        }
        return separation;
    }

    /// <summary>
    /// calculates the alignment of the boids 
    /// </summary>
    /// <returns>alignment vector</returns>
    private Vector3 CalculateAlignment()
    {
        Vector3 alignment = Vector3.zero;
        int numberOfBoidsTooClose = 0;
        for(int i = 0; i < enemyBoids.Count; i++)
        {
            Enemy neighbour = enemyBoids[i];
            Vector3 separation = transform.position - neighbour.transform.position;
            float distance = separation.magnitude;
            if (distance > 0f && distance < alignmentRadius)
            {
                alignment += neighbour.Velocity.normalized;
                numberOfBoidsTooClose++;
            }
        }
        if(numberOfBoidsTooClose > 0)
        {
            Vector3 alignmentAvg = alignment / numberOfBoidsTooClose;
            Vector3 target = alignmentAvg.normalized * speedMultiplier;
            if(target.magnitude > maxVelocity)
            {
                alignmentAvg = alignmentAvg.normalized * maxVelocity;
            }
            return alignmentAvg;
        }
        return alignment;
    }


    /// <summary>
    /// calculates cohesion of the boids
    /// </summary>
    /// <returns>cohesion vector</returns>
    private Vector3 CalculateCohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int numberOfBoidsTooClose = 0;
        for (int i = 0; i < enemyBoids.Count; i++)
        {
            Enemy neighbour = enemyBoids[i];
            Vector3 separation = transform.position - neighbour.transform.position;
            float distance = separation.magnitude;
            if (distance > 0f && distance < cohesionRadius)
            {
                cohesion += neighbour.transform.position;
                numberOfBoidsTooClose++;
            }
        }
        if (numberOfBoidsTooClose > 0)
        {
            Vector3 cohesionAvg = cohesion / numberOfBoidsTooClose;
            return CalculateTarget(cohesionAvg);
        }
        return cohesion;
    }
    #endregion

    #region StateSwitches

    private void SetFSM_IDLE()
    {
        idling = true;
    }
    private void SetFSM_PATROL()
    {
        //Set state
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
        boidManager.AddToBoidList(this);

        enemyBoids = boidManager.EnemyBoids;
        anim.SetBool("nextToOtherEnemy", true);
        groupingUp = true;
    }
    #endregion

    public void CheckState()
    {
        if (idling)
        {
            CheckForPlayerInReach();
        }
        else if (patroling)
        {
            
        }
        else if (groupingUp)
        {
            
        }
        else if (attacking)
        {
            
        }else if (evading)
        {
            
        }
    }

    private void CheckForPlayerInReach()
    {
        //distance between player and enemy
        float distanceToCheck = (playerRef.transform.position - this.transform.position).sqrMagnitude;
        float offset = 0.3f;
        //added offset to trigger sooner, otherwise the trigger only considers the objects pivots
        if (distanceToCheck < (radiusPlayerInReach*radiusPlayerInReach) + offset)
        {
            SetFSM_PATROL();
            patroling = true;
        }
        else
        {
            //i think this is obsolete
            SetFSM_IDLE();
            idling = true;
        }
    }

}
