using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: KISimulation
 * File: PlayerManager.cs
 * Version: 1.01
 * Autor:  Franz M�rike (FM);
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

    #region Rotation
    private Vector3 playerPos;
    private Vector3 mousePos;
    #endregion

    public Vector3 PlayerPosition { get => playerPos; }

    void Awake()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        playerController.Move(direction * Time.deltaTime * playerSpeed);
    }

    private void RotatePlayer()
    {
        //Ray from Camera to mousePos
        Ray rayCamToGround = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(rayCamToGround, out RaycastHit rayHitsGround))
        {
            //calculate pos of mouse
            mousePos = rayHitsGround.point;
            //calculate distance between player and mouse
            Vector3 distancePlayerToMouse = mousePos - playerPos;
            //set y to 0, otherwise it rotates around y
            distancePlayerToMouse.y = 0;
            //Calculate and set rotation
            Quaternion rotation = Quaternion.LookRotation(distancePlayerToMouse);
            transform.rotation = rotation;
        }
        
    }

    private void OnDrawGizmos()
    {
        //Draw Line between mouse and playerposition (Debug)
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(playerPos, mousePos);
    }
}
