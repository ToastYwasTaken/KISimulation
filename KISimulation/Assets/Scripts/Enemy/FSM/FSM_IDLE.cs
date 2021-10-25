using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******************************************************************************
 * Project: KISimulation
 * File: FSM_IDLE.cs
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
 *  23.10.2021  added IDLE behaviour
 *  
 *****************************************************************************/
public class FSM_IDLE : FSM
{
    private MonoBehaviour monoSurrogate;
    private float desiredRotationY;
    private float currentRotationY;
    private int rotationMultiplier;
    private float rotationSpeed = 5f;
    private bool rotatingForward;
    //private float randomDelay = 2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get a MonoBehaviour in scene
        monoSurrogate = GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonoBehaviour>();
        SetRotationAndMultiplier();
    }

    //IDLE behaviour is coded here
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentRotationY = base.gameObject.transform.rotation.eulerAngles.y;
        //CASE: rotating forward AND currentRotationY > desiredRotationY
        if (rotatingForward && currentRotationY > desiredRotationY)
        {
            //destination angle reached
            rotatingForward = false;
        }
        //CASE: rotating forward AND currentRotationY < desiredRotationY
        else if (rotatingForward && currentRotationY < desiredRotationY)
        {
            base.gameObject.transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime * rotationMultiplier, Space.Self);
        }
        //CASE: rotating backward AND currentRotationY > desiredRotationY
        else if (!rotatingForward && currentRotationY > desiredRotationY)
        {
            base.gameObject.transform.Rotate(new Vector3(0f, -rotationSpeed, 0f) * Time.deltaTime * rotationMultiplier, Space.Self);
        }
        //CASE: rotating backward AND currentRotationY < desiredRotationY
        else if (!rotatingForward && currentRotationY < desiredRotationY)
        {
            //destination angle reached
            rotatingForward = true;
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /// <summary>
    /// Calculates new desired Rotation y and rotationMultiplier when destination is reached by the enemy
    /// </summary>
    private void SetRotationAndMultiplier()
    {
        desiredRotationY = Random.Range(0, 360);
        rotationMultiplier = Random.Range(5, 15);
    }
}
