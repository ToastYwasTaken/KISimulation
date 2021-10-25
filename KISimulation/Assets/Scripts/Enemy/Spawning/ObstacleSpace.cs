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
 *  25.10.2021  added additional list to save child objects without mother
 *  
 *****************************************************************************/
public class ObstacleSpace : MonoBehaviour
{
    private static Transform[] childrenAndMother;
    //list holds only child obj
    private static List<Transform> childrenList;
    //obstacleCount excluding mother obj
    public static int obstacleCount;
    private static int[,] obstacleCoordinates;
    private static int count = 0;

    void Awake()
    {
        //Get all obstacles in scene
        AssignChildren();
        obstacleCount = childrenAndMother.Length-1;
        obstacleCoordinates = new int[obstacleCount, 4];
    }


    /// <summary>
    /// Calculates the area where enemies shouldn't be instantiated
    /// </summary>
    /// <returns>Array[obstaclesCount, 4] holding the values xFrom, xTo, zFrom, zTo</returns>
    public static int[,] CalculateSpaceTaken()
    {
        foreach (Transform child  in childrenList)
        {

                //Using renderer to get the corner x and z coordinates of the obstacles
                Renderer renderer = child.GetComponent<Renderer>();

                int xFrom = (int)renderer.bounds.min.x;
                int xTo = (int)renderer.bounds.max.x;
                int zFrom = (int)renderer.bounds.min.z;
                int zTo = (int)renderer.bounds.max.z;

                Debug.Log($"Obstacle {count}: xFrom {xFrom} | xTo {xTo} | zFrom {zFrom} | zTo {zTo}");

                //add to array
                obstacleCoordinates[count, 0] = xFrom;
                obstacleCoordinates[count, 1] = xTo;
                obstacleCoordinates[count, 2] = zFrom;
                obstacleCoordinates[count, 3] = zTo;

                count++;
        }
        //resetting counter
        count = 0;
        return obstacleCoordinates;
    }

    /// <summary>
    /// Assigning children / exclude motherObj
    /// </summary>
    private void AssignChildren()
    {
        childrenAndMother = GetComponentsInChildren<Transform>();
        childrenList = new List<Transform>();
        foreach (Transform child in childrenAndMother)
        {
            //Filter by tag
            if (child.tag.Equals("Obstacle"))
            {
                childrenList.Add(child);
            }
        }
    }

}
