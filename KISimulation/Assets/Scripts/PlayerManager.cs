using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: KISimulation
 * File: PlayerManager.cs
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
 *  05.10.2021  created
 *  
 *****************************************************************************/
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    CharacterController playerController;
    private Vector3 playerPosition;

    public Vector3 PlayerPosition { get => playerPosition; }

    void Awake()
    {
        playerPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {

        playerPosition = transform.position;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        playerController.Move(direction * Time.deltaTime * playerSpeed);
    }

    private void RotatePlayer()
    {
        float targetMousePositionX = Input.mousePosition.x - transform.position.x;
        float targetMousePositionZ = Input.mousePosition.z - transform.position.z;

        //calculate angle between mouseposition and playerposition
        Vector3 targetVector = new Vector3(targetMousePositionX, 0f, targetMousePositionZ);
        float angle = Vector3.Angle(targetVector, transform.forward);

        //calculate final rotation
        transform.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    private void OnDrawGizmos()
    {
        //Draw Line between mouse and playerposition (Debug)
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector2(Input.mousePosition.x, Input.mousePosition.z), new Vector2(transform.position.x, transform.position.z));
    }
}
