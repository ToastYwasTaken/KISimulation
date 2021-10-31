using UnityEngine;

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
            instantiatedWayPoint.transform.parent = GameObject.FindGameObjectWithTag("WayPointsMother").transform;
            //tagging
            instantiatedWayPoint.gameObject.tag = "WayPoint";
            //Debug.Log("waypoint at " + i + " is: " + wayPoints[i]);
        }
    }
}
