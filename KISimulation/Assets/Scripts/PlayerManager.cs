using System.Collections;
using System.Collections.Generic;
using TMPro;
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
 *  23.10.2021  added method description
 *  02.11.2021  added Attack mechanic
 *  09.11.2021  removed character controller, added movement via animation
 *  17.22.2021  added health mechanics + display
 *  
 *****************************************************************************/
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;

    private float playerHealth;
    [SerializeField]
    private TextMeshPro debugHealth; //update when getting hit

    private CharacterController characterController;
    private const float gravityConstant = -9.81f;

    private Vector3 playerPos;
    private Vector3 mousePos;

    public float PlayerHealth { get => playerHealth; set => playerHealth = value; }


    void Awake()
    {
        playerHealth = 100f;
        debugHealth.text = PlayerHealth.ToString();
        playerPos = transform.position;
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    /// <summary>
    /// Player movement
    /// </summary>
    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 fallDrag = Vector3.zero;
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(direction * playerSpeed * Time.deltaTime);
        //calculate drag so the player doesn't fly
        fallDrag.y = gravityConstant * Time.deltaTime;
        characterController.Move(fallDrag);
    }

    /// <summary>
    /// Player rotation
    /// </summary>
    private void RotatePlayer()
    {
        //Update playerPos
        playerPos = transform.position;
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

    public void UpdatePlayerHealth()
    {
        debugHealth.text = playerHealth.ToString();
    }

    private void OnDrawGizmos()
    {
        //DEBUG | Draw Line between mouse and playerposition 
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(playerPos, mousePos);
        //DEBUG | Draw Ray between cam and mouseposition
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Camera.main.transform.position, mousePos);
    }

}
