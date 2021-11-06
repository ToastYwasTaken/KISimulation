using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: WayPoints.cs
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
 *  31.10.2021  created
 *  
 * ----------------------------
 * NOTE:    this script was originally handled in Enemy.cs, but moved here, bc 
 *          the instantiation of waypoints is only needed once
 *  
 *  
 *****************************************************************************/
public class WayPoints : MonoBehaviour
{
    private GameObject groundRef;
    private Ground ground;
    public Vector3[] wayPoints;
    private int wayPointsAmount;

    /// <summary>
    ///Assigning waypoints relative to the size of the ground, ignores obstacles
    /// </summary>
    public void SpawnWayPoints()
    {
        //references needed to assign the waypoints
        groundRef = GameObject.FindGameObjectWithTag("Ground");
        ground = groundRef.GetComponent<Ground>();
        RandomSpawnpoint randomSpawnpointRef = groundRef.GetComponent<RandomSpawnpoint>();
        //create as many waypoints as the lesser of width and height
        wayPointsAmount = ground.GetAreaOfGround / (Mathf.Min(ground.GetCurrentWidthX, ground.GetCurrentHeightZ));
        //assign array to fill with wayPoints
        wayPoints = new Vector3[wayPointsAmount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = randomSpawnpointRef.ReturnValidSpawnPoint();
            //Instantiate wayPoints at the desired positions
            GameObject wayPoint = new GameObject("WayPoint");
            GameObject instantiatedWayPoint = Instantiate(wayPoint, wayPoints[i], Quaternion.identity);
            instantiatedWayPoint.gameObject.name = "Waypoint " + (i + 1).ToString();
            //Set WayPoints mother obj
            instantiatedWayPoint.transform.SetParent(GameObject.FindGameObjectWithTag("WayPointsMother").transform);
            //tagging
            instantiatedWayPoint.gameObject.tag = "WayPoint";
            //Debug.Log("waypoint at " + i + " is: " + wayPoints[i]);
            //destroy original bc not needed anymore
            Destroy(wayPoint);
        }
    }
}
