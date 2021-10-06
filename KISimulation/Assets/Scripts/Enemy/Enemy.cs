using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 spawnPosition;

    [SerializeField]
    Animator anim;

    private Rigidbody rb;

    private FSM myFSMState;


    private void Awake()
    {
        spawnPosition = transform.position;

        //Initialize FSM
        myFSMState = anim.GetBehaviour<FSM>();
        myFSMState.SetGO(this.gameObject);

        //Set Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        Idle();

       
    }

    private void Update()
    {

    }

    private void Idle()
    {
        //Set delay before spawning
        StartCoroutine("Delay");
        //Set state
        anim.SetBool("enemySpawned", true);
    }

    private void Patrol()
    {

    }

    private void Attack()
    {

    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
    }

}
