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
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //Change state to PATROL
       
    }

    private void Update()
    {
        if (true)
        {

        }
    }

    private void Idle()
    {
        //Change state to IDLE
        myFSMState = anim.GetBehaviour<FSM_IDLE>();
        rb = GetComponent<Rigidbody>();
        //Freeze Rigidbody
    }

    private void Patrol()
    {

    }

    private void Attack()
    {

    }



}
