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
 *  30.10.2021  temporarily deleted monosurrogate, prob unnessecary
 *  
 *****************************************************************************/
public class FSM_IDLE : FSM
{
    //TODO Add intermediate state with exitTime as extra node
    //private MonoBehaviour monoSurrogate;
    private float desiredRotationY;
    private float currentRotationY;
    private int rotationMultiplier;
    private float rotationSpeed = 5f;
    private bool rotatingForward;
    private EnemyGroup currentEnemyGroup;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get enemy group
        FSM_GROUP fsmGroup = animator.GetBehaviour<FSM_GROUP>();
        currentEnemyGroup = fsmGroup.CurrentEnemyGroup;
        //Get a MonoBehaviour in scene
        //monoSurrogate = GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonoBehaviour>();
        SetRotationAndMultiplier();
        //Debug.Log($"Enter | GO: {gameObject} GO in base: {base.gameObject}");
    }

    //IDLE behaviour is coded here
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentRotationY = animator.gameObject.transform.rotation.eulerAngles.y;
        //CASE: rotating forward AND currentRotationY > desiredRotationY
        if (rotatingForward && currentRotationY > desiredRotationY)
        {
            //destination angle reached
            rotatingForward = false;
            SetRotationAndMultiplier();
        }
        //CASE: rotating forward AND currentRotationY < desiredRotationY
        else if (rotatingForward && currentRotationY < desiredRotationY)
        {
            animator.gameObject.transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime * rotationMultiplier, Space.Self);
        }
        //CASE: rotating backward AND currentRotationY > desiredRotationY
        else if (!rotatingForward && currentRotationY > desiredRotationY)
        {
            animator.gameObject.transform.Rotate(new Vector3(0f, -rotationSpeed, 0f) * Time.deltaTime * rotationMultiplier, Space.Self);
        }
        //CASE: rotating backward AND currentRotationY < desiredRotationY
        else if (!rotatingForward && currentRotationY < desiredRotationY)
        {
            //destination angle reached
            rotatingForward = true;
            SetRotationAndMultiplier();
        }
        //Debug.Log($"Update | GO: {gameObject} GO in base: {base.gameObject}");
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
