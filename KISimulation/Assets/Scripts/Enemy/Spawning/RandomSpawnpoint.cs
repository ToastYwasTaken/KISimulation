using System.Collections;
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

    private List<Vector3> spawnPointsList = new List<Vector3>();

    //currentScale is used for non square ground
    private int currentScaleX;
    private int currentScaleZ;

    private int[,] invalidSpawnSpaces;
    public Vector3 SpawnPosition { get => spawnPosition; }

    private void Awake()
    {

        //current Scale of ground
        currentScaleX = (int)groundReference.transform.localScale.x;
        currentScaleZ = (int)groundReference.transform.localScale.z;

        //Get corners of plane
        cornerTopLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[0];
        cornerTopRight = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[10];
        cornerBotLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[110];

        Debug.Log("Top left corner: " + cornerTopLeft + " Top right corner: " + cornerTopRight + " Bottom left corner: " + cornerBotLeft);

    }
    public Vector3 GenerateRandomSpawnPoint(bool _first)
    {
        //Generate spawn points
        //x and z are randomly calculated
        //y is always the same position 
        //Calculate invalid Spawnpoints
        if (_first)
        {
            invalidSpawnSpaces = ObstacleSpace.CalculateSpaceTaken();
            _first = false;
        }

        int randomPositionX = (int)Random.Range((cornerTopLeft.x*currentScaleX), (cornerTopRight.x*currentScaleX));
        int positionY = (int)groundReference.transform.position.y + 1;
        int randomPositionZ = (int)Random.Range((cornerTopLeft.z*currentScaleZ), (cornerBotLeft.z*currentScaleZ));
        
        Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);
        Debug.Log("New desired spawn position: " + desiredSpawnPosition);

        for (int i = 0; i < invalidSpawnSpaces.GetLength(0); i++)
        {
            Debug.Log("Checking spawn point iteration " + i);
            CheckSpawnPoint(desiredSpawnPosition, invalidSpawnSpaces[i, 0], invalidSpawnSpaces[i, 1], invalidSpawnSpaces[i, 2], invalidSpawnSpaces[i, 3], _first);
        }

         spawnPosition = desiredSpawnPosition;
         //Debug.Log("Spawn Pos in RandomSpawnPoint: " + spawnPosition);
         spawnPointsList.Add(spawnPosition);
         return spawnPosition;
    }

    /// <summary>
    /// Checks if desired spawn position is valid
    /// Compares if within other objects bounds
    /// </summary>
    /// <param name="_desiredSpawnPosition">desired spawn position</param>
    /// <param name="_xFrom"></param>
    /// <param name="_xTo"></param>
    /// <param name="_zFrom"></param>
    /// <param name="_zTo"></param>
    private void CheckSpawnPoint(Vector3 _desiredSpawnPosition, float _xFrom, float _xTo, float _zFrom, float _zTo, bool _first)
    {
        //Check for Obstacles
        if (_desiredSpawnPosition.x > _xFrom && _desiredSpawnPosition.x < _xTo && _desiredSpawnPosition.z > _zFrom && _desiredSpawnPosition.z < _zTo)
        {
            //Spawn position invalid
            Debug.Log("Spawn invalid, obstacle blocking space");
            GenerateRandomSpawnPoint(_first);
        }
        //Check for previous taken spawnpositions
        else if (spawnPointsList.Contains(_desiredSpawnPosition))
        {
            //Spawn position invalid
            Debug.Log("Spawn invalid, enemy blocking space");
            GenerateRandomSpawnPoint(_first);
        }
        //Check for overlapping colliders
        //else if (Physics.CheckSphere(groundReference.transform.position, Mathf.Max((_xTo-_xFrom), (_zFrom - _zTo))))
        //{

        //}
        else
        {
            //Spawn position valid
            Debug.Log("Spawn valid");
        }
    }

}
