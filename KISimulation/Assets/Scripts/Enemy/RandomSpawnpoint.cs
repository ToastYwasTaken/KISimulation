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
 *  
 *****************************************************************************/
public class RandomSpawnpoint : MonoBehaviour
{
    #region Vectors
    private Vector3 spawnPosition;
    private Vector3 cornerTopLeft;
    private Vector3 cornerTopRight;
    private Vector3 cornerBottomLeft;
    private Vector3 platformCentre;
    #endregion

    [SerializeField]
    private GameObject groundReference;

    private List<Vector3> spawnPointsList = new List<Vector3>();

    private float maxRadiusToCheck = 0f;    //assigned later

    private const float offset = 0.2f;
    private int currentScaleX;
    private int currentScaleZ;
    public Vector3 SpawnPosition { get => spawnPosition; }

    private void Awake()
    {
        platformCentre = groundReference.transform.position;

        //Get corners of plane

        currentScaleX = (int)groundReference.transform.localScale.x;
        currentScaleZ = (int)groundReference.transform.localScale.z;

        cornerTopLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[0];
        cornerTopRight = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[10];
        cornerBottomLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[110];

        Debug.Log("Top left corner: " + cornerTopLeft + " Top right corner: " + cornerTopRight + " Bottom left corner: " + cornerBottomLeft);

        //assign correct radiusToCheck 

        maxRadiusToCheck = cornerTopLeft.x*currentScaleX - cornerTopRight.x*currentScaleX;
    }
    public void GenerateRandomSpawnPoint()
    {
        //Generate spawn points
        //x and z are randomly calculated with offsets
        //y is always the same position 

        int randomPositionX = (int)Random.Range(((cornerTopLeft.x*currentScaleX)-offset), ((cornerTopRight.x*currentScaleX)+offset));
        int positionY = (int)groundReference.transform.position.y + 1;
        int randomPositionZ = (int)Random.Range(((cornerTopLeft.z*currentScaleZ)-offset), ((cornerBottomLeft.z*currentScaleZ)+offset));
        
        Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);

        //Check if the spawnposition is already taken
        if (spawnPointsList.Contains(desiredSpawnPosition))
        {
            GenerateRandomSpawnPoint();
        }
        else
        {

            //ignores layer 6: Ground for spawning
            if (!Physics.CheckSphere(platformCentre, maxRadiusToCheck, 6))
            {
                spawnPosition = desiredSpawnPosition;
                Debug.Log("Spawn Pos: " + spawnPosition);
                spawnPointsList.Add(spawnPosition);
            }
            else return;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(platformCentre, maxRadiusToCheck);
    }

}
