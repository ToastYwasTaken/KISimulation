using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: Ground.cs
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
 *  25.10.2021  created bc ground needs to be used for 
 *              multiple calculations in other scripts
 *  
 *****************************************************************************/

public class Ground : MonoBehaviour
{

    private int currentScaleX;
    private int currentScaleZ;
    private int currentWidthX;
    private int currentHeightZ;
    private int areaOfGround;

    private Vector3 cornerTopLeft;
    private Vector3 cornerTopRight;
    private Vector3 cornerBotLeft;

    public int GetCurrentWidthX { get => currentWidthX; }
    public int GetCurrentScaleX { get => currentScaleX; }
    public int GetCurrentScaleZ { get => currentScaleZ; }
    public int GetCurrentHeightZ { get => currentHeightZ; }
    public int GetAreaOfGround { get => areaOfGround; }
    public Vector3 GetCornerTopLeft { get => cornerTopLeft; }
    public Vector3 GetCornerTopRight { get => cornerTopRight; }
    public Vector3 GetCornerBotLeft { get => cornerBotLeft; }


    // Start is called before the first frame update
    void Awake()
    {
        AssignValues();
    }

    public void AssignValues()
    {
        //assign scale
        currentScaleX = (int)this.transform.localScale.x;
        currentScaleZ = (int)this.transform.localScale.z;

        //Get corners of plane
        cornerTopLeft = this.GetComponent<MeshFilter>().sharedMesh.vertices[0];
        cornerTopRight = this.GetComponent<MeshFilter>().sharedMesh.vertices[10];
        cornerBotLeft = this.GetComponent<MeshFilter>().sharedMesh.vertices[110];

        //multiply corners with scale
        cornerTopLeft.x *= currentScaleX;
        cornerBotLeft.x *= currentScaleX;
        cornerTopRight.x *= currentScaleX;
        cornerTopLeft.z *= currentScaleZ;
        cornerBotLeft.z *= currentScaleZ;
        cornerTopRight.z *= currentScaleZ;

        //assign width and height
        currentHeightZ = Mathf.Abs((int)(cornerTopLeft.z - cornerBotLeft.z));
        currentWidthX = Mathf.Abs((int)(cornerTopLeft.x - cornerTopRight.x));
        
        //assign area
        areaOfGround = currentHeightZ * currentWidthX;
    }
}
