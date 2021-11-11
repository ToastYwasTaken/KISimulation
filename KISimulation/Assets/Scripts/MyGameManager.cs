using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/******************************************************************************
 * Project: KISimulation
 * File: MyGameManager.cs
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
 *  05.10.2021  created
 *  10.10.2021  changed enemiesInstantiatedList to .Arr 
 *              added Remove() method
 *  23.10.2021  added method description
 *  
 *****************************************************************************/
public class MyGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject groundRef;
    private Ground ground;

    [Header("Enemy-stuff")]
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    int maxEnemyCount;

    [SerializeField]
    TextMeshProUGUI textEnemies;

    [SerializeField]
    WayPoints wayPoints;

    private GameObject [] enemiesInstantiatedArr;
    private Enemy[] allEnemies;
    private GameObject currentInstantiatedObject;
    private int pointer;
    private int counter = 0;

    #region EnemySettings
    [SerializeField]
    private float radiusPlayerInReach = 30f;    //DEFAULT: 20f
    [SerializeField]
    private float radiusNextToOtherEnemy = 4f; //DEFAULT: 1.5f
    [SerializeField]
    private float playerSpottedAngle = 10f; //DEFAULT: 10f (means a x degree wide tolerance to spot the player)
    [SerializeField]
    private float playerSpottingDistance = 25f; //Default 25f
    #endregion

    public Enemy[] AllEnemies { get => allEnemies; set => allEnemies = value; }
    public float RadiusPlayerInReach { get => radiusPlayerInReach;}
    public float RadiusNextToOtherEnemy { get => radiusNextToOtherEnemy;}
    public float PlayerSpottedAngle { get => playerSpottedAngle;}
    public float PlayerSpottingDistance { get => playerSpottingDistance; set => playerSpottingDistance = value; }

    private void Awake()
    {
        currentInstantiatedObject = null;
        enemiesInstantiatedArr = new GameObject[maxEnemyCount];
        AllEnemies = new Enemy[maxEnemyCount];
    }

    private void Update()
    {
        if (!groundRef.TryGetComponent<Ground>(out ground).Equals(null))
        {
            if(counter == 0)
            {
                ground = groundRef.GetComponent<Ground>();
                wayPoints.SpawnWayPoints();
                counter++;
            }
        }
    }

    /// <summary>
    /// Spawns an enemy when pressing the "add enemy" button in scene
    /// </summary>
    public void SpawnEnemy()
    {
        //Don't spawn more than maxEnemyCount enemies
        if (pointer >= maxEnemyCount)
        {
            return;
        }
        else
        {
            //Set random Spawnpoints and random Rotation
            RandomSpawnpoint randomSpawnPoint = groundRef.GetComponent<RandomSpawnpoint>();
            RandomRotation randomSpawnRotation = groundRef.GetComponent<RandomRotation>();
            Vector3 newSpawnPoint = randomSpawnPoint.ReturnValidSpawnPoint();
            Quaternion newSpawnRotation = randomSpawnRotation.GenerateRandomRotation();
            //Debug.Log("Spawn Pos in GameManager: " + newSpawnPoint);
            //Instantiate the Prefab
            currentInstantiatedObject = Instantiate(enemyPrefab, newSpawnPoint, newSpawnRotation);
            //Adjust names
            currentInstantiatedObject.name = "Enemy_" + (pointer+1).ToString();
            //Safe the instantiated GO in the array
            enemiesInstantiatedArr[pointer] = currentInstantiatedObject;
            //increasing pointer when adding something to the array
            pointer++;
            AdjustEnemyCounter();
        }
    }

    /// <summary>
    /// Removes the last instantiated enemy when pressing the "remove enemy" button in scene
    /// </summary>
    public void RemoveEnemy()
    {
        //Don't remove out of empty list
        if (pointer == 0)
        {
            return;
        }
        else
        {
            //decrement pointer before Destroying the GO at that position
            Destroy(enemiesInstantiatedArr[--pointer]);
            //override reference in array to null
            enemiesInstantiatedArr[pointer] = null;
            AdjustEnemyCounter();
        }
    }
    
    private void AdjustEnemyCounter()
    {
        textEnemies.text = "Enemies: " + pointer.ToString();
    }


}
