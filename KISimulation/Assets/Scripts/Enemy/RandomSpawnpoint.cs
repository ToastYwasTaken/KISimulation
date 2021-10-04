using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnpoint : MonoBehaviour
{

    private Vector3 spawnPosition;
    [SerializeField]
    private GameObject groundReference;

    private List<Vector3> spawnPointsList = new List<Vector3>();

    private const float maxRadiusToCheck = 5f;

    public Vector3 SpawnPosition { get => spawnPosition; }

    public void GenerateRandomSpawnPoint()
    {
        //TODO: check if other object is there already

        //Get corners of plane

        Vector3 cornerTopLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[0];
        Vector3 cornerTopRight = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[10];
        Vector3 cornerBottomLeft = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[110];
        //Vector3 cornerBottomRight = groundReference.GetComponent<MeshFilter>().sharedMesh.vertices[120];

        Debug.Log("Top left corner: " + cornerTopLeft + " Top right corner: " + cornerTopRight + " Bottom left corner: " + cornerBottomLeft);

        //Generate spawn points - only y is always the same (slightly above the ground)

        float randomPositionX = Random.Range(cornerTopLeft.x, cornerTopRight.x);
        float positionY = groundReference.transform.position.y + 1f;
        float randomPositionZ = Random.Range(cornerTopLeft.z, cornerBottomLeft.z);

        Debug.Log("xRandom: " + randomPositionX + " yRelative: " + positionY + " zRandom: " + randomPositionZ);

        Vector3 desiredSpawnPosition = new Vector3(randomPositionX, positionY, randomPositionZ);

        //Check if there is something already on the desired spawnposition
        if (Physics.CheckSphere(desiredSpawnPosition, maxRadiusToCheck))
        {
            spawnPosition = desiredSpawnPosition;
            Debug.Log("Spawn Pos: " + spawnPosition);
            spawnPointsList.Add(spawnPosition);
        }
        else return;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPosition, maxRadiusToCheck);
    }

}
