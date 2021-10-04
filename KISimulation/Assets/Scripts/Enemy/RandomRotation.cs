using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Quaternion spawnRotation;

    public Quaternion SpawnRotation { get => spawnRotation;}

    public void GenerateRandomSpawnRotation()
    {
        float rotationX = 0f, rotationZ = 0f;
        float rotationY = Random.Range(-1f, 1f), rotationMagnitudeW = Random.Range(-1f, 1f);

        spawnRotation = new Quaternion(rotationX, rotationY, rotationZ, rotationMagnitudeW);
    }

}
