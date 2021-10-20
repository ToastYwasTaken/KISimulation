using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/******************************************************************************
 * Project: KISimulation
 * File: MyGameManager.cs
 * Version: 1.01
 * Autor:  Franz M�rike (FM);
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
 *  
 *****************************************************************************/
public class MyGameManager : MonoBehaviour
{

    private GameObject [] enemiesInstantiatedArr;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject groundRef;

    [SerializeField]
    int maxEnemyCount;

    [SerializeField]
    TextMeshProUGUI textEnemies;


    private GameObject currentInstantiatedObject;
    private int pointer;
    private bool firstEnemy;

    private void Awake()
    {
        currentInstantiatedObject = null;
        enemiesInstantiatedArr = new GameObject[maxEnemyCount];
        firstEnemy = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

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
            RandomSpawnpoint spawnPointRef = groundRef.GetComponent<RandomSpawnpoint>();
            RandomRotation spawnRotationRef = groundRef.GetComponent<RandomRotation>();
            if(pointer > 0)
            {
                firstEnemy = false;
            }
            Vector3 spawnPoint = spawnPointRef.GenerateRandomSpawnPoint(firstEnemy);
            Quaternion spawnRotation = spawnRotationRef.GenerateRandomSpawnRotation();
            Debug.Log("Spawn Pos in GameManager: " + spawnPoint);
            //Instantiate the Prefab
            currentInstantiatedObject = Instantiate(enemyPrefab, spawnPoint, spawnRotation);
            //Safe the instantiated GO in the array
            enemiesInstantiatedArr[pointer] = currentInstantiatedObject;
            //increasing pointer when adding something to the array
            pointer++;
            AdjustEnemyCounter();
        }
    }

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
