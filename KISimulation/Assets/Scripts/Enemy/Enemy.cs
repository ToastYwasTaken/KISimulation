using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: Enemy.cs
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
 *  07.10.2021  created
 *  
 *****************************************************************************/
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
        //Set state
        anim.SetBool("playerInReach", true);
    }

    private void Patrol()
    {
        //Set delay before patroling
        StartCoroutine("Delay");

    }

    private void Attack()
    {

    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
    }

}
