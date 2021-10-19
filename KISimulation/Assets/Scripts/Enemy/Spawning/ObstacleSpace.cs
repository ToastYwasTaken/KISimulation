using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: ObstacleSpace.cs
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
 *  19.10.2021  created
 *  
 *****************************************************************************/
public class ObstacleSpace : MonoBehaviour
{
    private static Transform[] children;
    private static int obstacleCount;
    private static float[,] obstacleCoordinates;
    private static int count = 0;

    void Awake()
    {
        //Get all obstacles in scene
        children = GetComponentsInChildren<Transform>();
        obstacleCount = children.Length;
        obstacleCoordinates = new float[obstacleCount, 4];
        
    }


    /// <summary>
    /// Calculates the area where enemies shouldn't be instantiated
    /// </summary>
    /// <returns>Array[obstaclesCount, 4] holding the values xFrom, xTo, zFrom, zTo</returns>
    public static float[,] CalculateSpaceTaken()
    {
        bool excludeFirst = true;
        foreach (Transform child  in children)
        {
            //exclude mother object
            if (excludeFirst)
            {
                excludeFirst = false;
            }
            else
            {
                ////Assign scale
                //currentScaleX = child.localScale.x;
                //currentScaleZ = child.localScale.z;

                //Get corners of plane
                //cornerTopLeft = child.GetComponent<MeshFilter>().sharedMesh.vertices[0];
                //cornerTopRight = child.GetComponent<MeshFilter>().sharedMesh.vertices[10];
                //cornerBotLeft = child.GetComponent<MeshFilter>().sharedMesh.vertices[110];
                Renderer renderer = child.GetComponent<Renderer>();

                //Calculate the corners x and z coordinates of that object
                float xFrom = renderer.bounds.min.x;
                float xTo = renderer.bounds.max.x;
                float zFrom = renderer.bounds.min.z;
                float zTo = renderer.bounds.max.z;

                Debug.Log($"xFrom {xFrom} | xTo {xTo} | zFrom {zFrom} | zTo {zTo}");

                //add to array
                obstacleCoordinates[count, 0] = xFrom;
                obstacleCoordinates[count, 1] = xTo;
                obstacleCoordinates[count, 2] = zFrom;
                obstacleCoordinates[count, 3] = zTo;

                count++;
            }
        }
        return obstacleCoordinates;
    }

}