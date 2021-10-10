using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{

    private List<GameObject> enemiesInstantiatedList = new List<GameObject>();

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    RandomSpawnpoint randomSpawnpointRef;

    [SerializeField]
    RandomRotation randomRotationRef;
    [SerializeField]
    int maxEnemyCount;


    private GameObject currentInstantiatedObject;

    private void Awake()
    {
        currentInstantiatedObject = null;
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
        if (enemiesInstantiatedList.Count >= maxEnemyCount)
        {
            return;
        }
        else
        {
            randomSpawnpointRef.GenerateRandomSpawnPoint();
            randomRotationRef.GenerateRandomSpawnRotation();
            currentInstantiatedObject = Instantiate(enemyPrefab, randomSpawnpointRef.SpawnPosition, randomRotationRef.SpawnRotation);
            Debug.Log("Spawn Pos: " + randomSpawnpointRef.SpawnPosition + " SpawnRotation: " + randomRotationRef.SpawnRotation);
            enemiesInstantiatedList.Add(currentInstantiatedObject);
        }
    }

    public void RemoveEnemy()
    {
        if(enemiesInstantiatedList.Count == 0)
        {
            return;
        }
        enemiesInstantiatedList.RemoveAt(enemiesInstantiatedList.Count - 1);
        Destroy(currentInstantiatedObject);
    }
    
    
    
}
