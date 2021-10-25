using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: WayPointManager.cs
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
 *  25.10.2021  created
 *  
 *****************************************************************************/

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject wayPointsMother;

    private Ground groundRef;
    private List<WayPoint> allWayPointsList;
    private int wayPointCount;
    private float fixedY;


    void Awake()
    {
        //TODO: randomly Instantiate Waypoints

        groundRef = ground.GetComponent<Ground>();
        //always relative to ground position
        this.transform.position = groundRef.transform.position;
        fixedY = groundRef.transform.position.y + 1;
        wayPointCount = RandomWayPointCount();
        AssignWayPoints();

    }

    private int RandomWayPointCount()
    {
        //    Debug.Log("ground size: " + groundRef.GetGroundSize());
        //    Debug.Log("widthX: " + groundRef.GetWidthX());
        //    Debug.Log("heightZ: " + groundRef.GetHeightZ());
        int areaOfGround = groundRef.GetAreaOfGround;
        int wayPointsRelativeToArea = areaOfGround / Mathf.Max(groundRef.GetCurrentWidthX, groundRef.GetCurrentHeightZ);
        return wayPointsRelativeToArea;
    }

    private void AssignWayPoints()
    {
        WayPoint currentWayPoint = new WayPoint(4, 3);

        for (int i = 0; i < wayPointCount; i++)
        {
            AddWayPoint(currentWayPoint);
        }
    }

    private void AddWayPoint(WayPoint _wayPointToAdd)
    {
        allWayPointsList.Add(_wayPointToAdd);
        Instantiate(_wayPointToAdd.wayPointGO, new Vector3(0f,fixedY,0f));
        _wayPointToAdd.wayPointGO.transform.parent = wayPointsMother.transform;
    }
}