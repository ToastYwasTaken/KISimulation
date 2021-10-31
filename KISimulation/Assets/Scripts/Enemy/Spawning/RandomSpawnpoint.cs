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
 *  25.10.2021  fixed proper assigning of valid spawn positions
 *  
 *****************************************************************************/

public class RandomSpawnpoint : MonoBehaviour
{
    #region Vectors
    private Vector3 cornerTopLeft;
    private Vector3 cornerTopRight;
    private Vector3 cornerBotLeft;
    #endregion

    private Ground groundRef;

    [SerializeField]
    private PlayerManager playerManager;

    //previously spawned enemies list
    private List<Vector3> spawnPointsList = new List<Vector3>();

    //Obstacle position coordinates x and z
    private int[,] invalidSpawnSpaces;

    //Note: MUST assign in Start, bc in Awake() ground might not be instantiated before this
    private void Start()
    {
        groundRef = this.gameObject.GetComponent<Ground>();
        //Add player position to spawnPointsList
        spawnPointsList.Add(playerManager.gameObject.transform.position);
        //Get corners of plane
        cornerTopLeft = groundRef.GetCornerTopLeft;
        cornerTopRight = groundRef.GetCornerTopRight;
        cornerBotLeft = groundRef.GetCornerBotLeft;
        //Debug.Log("cornerTopLeft: " + cornerTopLeft + " | cornerTopRight: " + cornerTopRight + " | cornerBotLeft: " + cornerBotLeft);
    }

    /// <summary>
    /// Generates a random desired spawn position on top of the ground
    /// </summary>
    /// <returns>Vector3 a potential spawn position</returns>
    private Vector3 GenerateRandomSpawnPosition()
    {
        //x and z are randomly calculated
        //y is always the same position 
        int randomPositionX = (int)Random.Range(cornerTopLeft.x, cornerTopRight.x);
        int positionY = (int)groundRef.transform.position.y + 1;
        int randomPositionZ = (int)Random.Range(cornerTopLeft.z, cornerBotLeft.z);

        Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);
        //Debug.Log("New desired spawn position: " + desiredSpawnPosition);
        return desiredSpawnPosition;
    }

    /// <summary>
    /// Returns a valid spawnposition after checking for overlapping other objects
    /// </summary>
    /// <returns>Vector3 desiredSpawnPoint</returns>
    public Vector3 ReturnValidSpawnPoint()
    {
        //Get all invalid spawn areas
        invalidSpawnSpaces = ObstacleSpace.CalculateSpaceTaken();
        int antiCrashCounter = 0;
        Vector3 desiredSpawnPoint = GenerateRandomSpawnPosition();
        bool isValid;
        //Check for Obstacles
        do
        {
            isValid = true;
            for (int i = 0; i < invalidSpawnSpaces.GetLength(0); i++)
            {
                //spawnPosition invalid | desired spawn position blocked by obstacles
                if ((desiredSpawnPoint.x >= invalidSpawnSpaces[i, 0] && desiredSpawnPoint.x <= invalidSpawnSpaces[i, 1]) && (desiredSpawnPoint.z >= invalidSpawnSpaces[i, 2] && desiredSpawnPoint.z <= invalidSpawnSpaces[i, 3]))
                {
                    isValid = false;
                    desiredSpawnPoint = GenerateRandomSpawnPosition();
                    break;
                }
                //spawnPosition invalid | desired spawn position already in list
                else if (spawnPointsList.Contains(desiredSpawnPoint))
                {
                    isValid = false;
                    desiredSpawnPoint = GenerateRandomSpawnPosition();
                    break;
                }
            }
            antiCrashCounter++;
            isValid = antiCrashCounter >= 1000;
        } while (!isValid);
        antiCrashCounter = 0;
        //Debug.Log("desired spawn position in RandomSpawnpoint: " + desiredSpawnPoint);
        spawnPointsList.Add(desiredSpawnPoint);
        return desiredSpawnPoint;
    }

}
