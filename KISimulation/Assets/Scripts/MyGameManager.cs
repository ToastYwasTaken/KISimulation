using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{

    private List<GameObject> enemiesList = new List<GameObject>();

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    RandomSpawnpoint randomSpawnpointRef;

    [SerializeField]
    RandomRotation randomRotationRef;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        //Don't spawn more than 10 enemies
        if (enemiesList.Count >= 10)
        {
            return;
        }
        else
        {
            randomSpawnpointRef.GenerateRandomSpawnPoint();
            randomRotationRef.GenerateRandomSpawnRotation();
            Instantiate(enemyPrefab, randomSpawnpointRef.SpawnPosition, randomRotationRef.SpawnRotation);
            Debug.Log("Spawn Pos: " + randomSpawnpointRef.SpawnPosition + " SpawnRotation: " + randomRotationRef.SpawnRotation);
            enemiesList.Add(enemyPrefab);
        }
    }
    
    
    
}
