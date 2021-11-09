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
 *  23.10.2021  added method description
 *  02.11.2021  added Attack mechanic
 *  09.11.2021  removed character controller, added movement via animation
 *  
 *****************************************************************************/
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    Animator animator;

    #region Rotation
    private Vector3 playerPos;
    private Vector3 mousePos;
    #endregion

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

    /// <summary>
    /// Player movement
    /// </summary>
    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal > 0)
        {
            Debug.Log("walking right");
            animator.SetBool("walkRight", true);
        }
        else
         if (vertical > 0)
        {
            Debug.Log("walking forward");
            animator.SetBool("walkForward", true);
        }
        else if (horizontal < 0)
        {
            Debug.Log("walking left");
            animator.SetBool("walkLeft", true);
        }
        else
        {
            animator.SetBool("walkRight", false);
            animator.SetBool("walkForward", false);
            animator.SetBool("walkLeft", false);
        }
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
