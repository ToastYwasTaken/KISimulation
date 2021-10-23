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
 *  17.10.2021  created
 *  20.10.2021  minor changes
 *  23.10.2021  added method description
 *  
 *****************************************************************************/

//TODO: Empty list after calculating behaviour of these boids
public class BoidManager : MonoBehaviour
{
    //BoidManager script is a Singleton
    private BoidManager instance;

    private List<Enemy> enemyBoids;

    private bool listEmpty = true;

    [SerializeField]
    float cohesionForce;
    [SerializeField]
    float alignmentForce;
    [SerializeField]
    float separationForce;
    [SerializeField]
    float targetForce;

    [SerializeField]
    PlayerManager playerRef;

    private Vector3 playerPos;

    #region Properties
    public List<Enemy> EnemyBoids { get => enemyBoids; }
    public float CohesionForce { get => cohesionForce; }
    public float AlignmentForce { get => alignmentForce; }
    public float SeparationForce { get => separationForce; }
    public float TargetForce { get => targetForce; }
    public Vector3 PlayerPos { get => playerPos; }
    #endregion

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    public void Update()
    {
        //Get player pos 
        playerPos = playerRef.transform.position;
    }
    public void FixedUpdate()
    {
        if (!listEmpty)
        {
            for (int i = 0; i < enemyBoids.Count; i++)
            {
                enemyBoids[i].ApplyBoidsMovement();
            }
        }
    }

    /// <summary>
    /// Adds an enemy to the boid list
    /// </summary>
    /// <param name="_objToAdd">enemy object to add</param>
    public void AddToBoidList(Enemy _objToAdd)
    {
        listEmpty = false;
        enemyBoids.Add(_objToAdd);
    }



}
