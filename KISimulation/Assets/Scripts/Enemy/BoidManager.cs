using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: Empty list after calculation behaviour of these boids
public class BoidManager : MonoBehaviour
{

    private BoidManager instance;

    private List<Enemy> enemyBoids;

    private bool listEmpty = true;

    [SerializeField]
    float cohesionForce;
    [SerializeField]
    float alignmentForce;
    [SerializeField]
    float separationForce;
    [SerializeField]
    float targetForce;

    [SerializeField]
    PlayerManager playerRef;

    private Vector3 playerPos;

    public List<Enemy> EnemyBoids { get => enemyBoids; }

    public float CohesionForce { get => cohesionForce; }
    public float AlignmentForce { get => alignmentForce; }
    public float SeparationForce { get => separationForce; }

    public float TargetForce { get => targetForce; }
    public Vector3 PlayerPos { get => playerPos; }
   

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void AddToBoidList(Enemy _objToAdd)
    {
        listEmpty = false;
        enemyBoids.Add(_objToAdd);
    }


    public void Update()
    {
        //Get player pos 
        playerPos = playerRef.transform.position;
    }
    public void FixedUpdate()
    {
        if (!listEmpty)
        {
            for (int i = 0; i < enemyBoids.Count; i++)
            {
                enemyBoids[i].ApplyBoidsMovement();
            }
        }
    }
}
