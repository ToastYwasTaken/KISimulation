using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: ObstacleSpace.cs
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
 *  19.10.2021  created
 *  20.10.2021  adjusted obstacleCoordinates to int arr
 *              added method description
 *  
 *****************************************************************************/
public class ObstacleSpace : MonoBehaviour
{
    private static Transform[] children;
    public static int obstacleCount;
    private static int[,] obstacleCoordinates;
    private static int count = 0;

    void Awake()
    {
        //Get all obstacles in scene
        children = GetComponentsInChildren<Transform>();
        obstacleCount = children.Length;
        obstacleCoordinates = new int[obstacleCount, 4];
        
    }


    /// <summary>
    /// Calculates the area where enemies shouldn't be instantiated
    /// </summary>
    /// <returns>Array[obstaclesCount, 4] holding the values xFrom, xTo, zFrom, zTo</returns>
    public static int[,] CalculateSpaceTaken()
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
                //Using renderer to get the corner x and z coordinates
                Renderer renderer = child.GetComponent<Renderer>();

                //Calculate the corners x and z coordinates of that object
                //round the values accordingly
                //int xFrom = FloorToInt(renderer.bounds.min.x);
                //int xTo = FloorToInt(renderer.bounds.max.x);
                //int zFrom = FloorToInt(renderer.bounds.min.z);
                //int zTo = FloorToInt(renderer.bounds.max.z);
                int xFrom = (int)renderer.bounds.min.x;
                int xTo = (int)renderer.bounds.max.x;
                int zFrom = (int)renderer.bounds.min.z;
                int zTo = (int)renderer.bounds.max.z;

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

    /// <summary>
    /// Rounds floats to Integers
    /// </summary>
    /// <param name="_valueToFloor">the value to convert</param>
    /// <returns>int value of a float</returns>
    private static int FloorToInt(float _valueToFloor)
    {
        _valueToFloor = _valueToFloor < 0 ? _valueToFloor-0.5f:_valueToFloor+0.5f;
        int res = (int)_valueToFloor;
        return res;
    }

}
