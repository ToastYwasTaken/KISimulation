using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: RandomSpawnpoint.cs
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
 *  17.10.2021  adjusted
 *  20.10.2021  bugfixing
 *              added method description
 *  
 *****************************************************************************/

public class RandomSpawnpoint : MonoBehaviour
{
    #region Vectors
    private Vector3 spawnPosition;
    private Vector3 cornerTopLeft;
    private Vector3 cornerTopRight;
    private Vector3 cornerBotLeft;
    #endregion

    [SerializeField]
    private GameObject groundReference;

    //previously spawned enemies list
    private List<Vector3> spawnPointsList = new List<Vector3>();

    //currentScale is used for non square ground
    private int currentScaleX;
    private int currentScaleZ;

    //Obstacle position coordinates x and z
    private int[,] invalidSpawnSpaces;

    private List<bool> validSpawnPositions;

    public Vector3 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    private void Awake()
    {
        //current Scale of ground
        currentScaleX = (int)groundReference.transform.localScale.x;
        currentScaleZ = (int)groundReference.transform.localScale.z;

        //Get corners of plane
        cornerTopLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[0];
        cornerTopRight = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[10];
        cornerBotLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[110];

        //adjust with current scale
        cornerTopLeft.x *= currentScaleX;
        cornerTopLeft.z *= currentScaleZ;
        cornerTopRight.x *= currentScaleX;
        cornerTopRight.z *= currentScaleZ;
        cornerBotLeft.x *= currentScaleX;
        cornerBotLeft.z *= currentScaleZ;
        Debug.Log("Top left corner: " + cornerTopLeft + " Top right corner: " + cornerTopRight + " Bottom left corner: " + cornerBotLeft);

        
    }

    /// <summary>
    /// Generates a random Spawnpoint on the ground plane for enemies 
    /// and shouldn't spawn them inside of obstacles
    /// </summary>
    /// <param name="_first">only Calculate the area of the obstacles on first runthrough</param>
    //public void GenerateRandomSpawnPoint(bool _first)
    //{
    //    //Calculate invalid Spawnpoints on first runthrough
    //    if (_first)
    //    {
    //        invalidSpawnSpaces = ObstacleSpace.CalculateSpaceTaken();
    //        _first = false;
    //    }

    //    //x and z are randomly calculated
    //    //y is always the same position 
    //    int randomPositionX = (int)Random.Range(cornerTopLeft.x, cornerTopRight.x);
    //    int positionY = (int)groundReference.transform.position.y + 1;
    //    int randomPositionZ = (int)Random.Range(cornerTopLeft.z, cornerBotLeft.z);

    //    Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);
    //    Debug.Log("New desired spawn position: " + desiredSpawnPosition);

    //    bool spawnPointIsValid = false;

    //    for (int i = 0; i < invalidSpawnSpaces.GetLength(0) - 1; i++)
    //    {
    //        Debug.Log("Checking spawn point iteration " + i);
    //        spawnPointIsValid = SpawnPointIsValid(desiredSpawnPosition, invalidSpawnSpaces[i, 0], invalidSpawnSpaces[i, 1], invalidSpawnSpaces[i, 2], invalidSpawnSpaces[i, 3], _first);

    //        if (!spawnPointIsValid)
    //        {
    //            GenerateRandomSpawnPoint(_first);
    //        }
    //    }
    //    if (spawnPointIsValid)
    //    {
    //        spawnPointsList.Add(desiredSpawnPosition);
    //        Debug.Log("Setting spawnPosition: " + desiredSpawnPosition);
    //        SpawnPosition = desiredSpawnPosition;
    //    }
    //}
    public void GenerateRandomSpawnPoint(bool _first)
    {
        validSpawnPositions = new List<bool>(ObstacleSpace.obstacleCount);
        //Calculate invalid Spawnpoints on first runthrough
        if (_first)
        {
            invalidSpawnSpaces = ObstacleSpace.CalculateSpaceTaken();
            _first = false;
        }

        //x and z are randomly calculated
        //y is always the same position 
        int randomPositionX = (int)Random.Range(cornerTopLeft.x, cornerTopRight.x);
        int positionY = (int)groundReference.transform.position.y + 1;
        int randomPositionZ = (int)Random.Range(cornerTopLeft.z, cornerBotLeft.z);

        Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);
        Debug.Log("New desired spawn position: " + desiredSpawnPosition);

        bool spawnPointIsValid = false;

        for (int i = 0; i < invalidSpawnSpaces.GetLength(0) - 1; i++)
        {
            Debug.Log("Checking spawn point iteration " + i);
            spawnPointIsValid = SpawnPointIsValid(desiredSpawnPosition, invalidSpawnSpaces[i, 0], invalidSpawnSpaces[i, 1], invalidSpawnSpaces[i, 2], invalidSpawnSpaces[i, 3], _first);
            if (!spawnPointIsValid)
            {
                validSpawnPositions = null;
                GenerateRandomSpawnPoint(_first);
            }
            else validSpawnPositions.Add(spawnPointIsValid);
        }
        if (!validSpawnPositions.Contains(false))
        {
            spawnPointsList.Add(desiredSpawnPosition);
            Debug.Log("Setting spawnPosition: " + desiredSpawnPosition);
            SpawnPosition = desiredSpawnPosition;
        }
    }
    /// <summary>
    /// Checks if desired spawn position is valid
    /// Compares if within other objects bounds
    /// </summary>
    /// <param name="_desiredSpawnPosition">desired spawn position</param>
    /// <param name="_xFrom">the x coordinate of area from where to start</param>
    /// <param name="_xTo">the x coordinate of area of where to go</param>
    /// <param name="_zFrom">the z coordinate of area from where to start</param>
    /// <param name="_zTo">the z coordinate of area of where to go</param>
    /// <returns>true if valid, false if invalid</returns>
    private bool SpawnPointIsValid(Vector3 _desiredSpawnPosition, float _xFrom, float _xTo, float _zFrom, float _zTo, bool _first)
    {
        //Check for Obstacles
        if (_desiredSpawnPosition.x > _xFrom && _desiredSpawnPosition.x < _xTo && _desiredSpawnPosition.z > _zFrom && _desiredSpawnPosition.z < _zTo)
        {
            //Spawn position invalid
            Debug.Log("Spawn invalid, obstacle blocking space");
            return false;
        }
        //Check for previous taken spawnpositions
        else if (spawnPointsList.Contains(_desiredSpawnPosition))
        {
            //Spawn position invalid
            Debug.Log("Spawn invalid, enemy blocking space");
            return false;
        }
        //Check for overlapping colliders
        //else if (Physics.CheckSphere(groundReference.transform.position, Mathf.Max((_xTo-_xFrom), (_zFrom - _zTo))))
        //{

        //}
        else
        {
            //Spawn position valid
            Debug.Log("Spawn valid");
            return true;
        }
    }

}
