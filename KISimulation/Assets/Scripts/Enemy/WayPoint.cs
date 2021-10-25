using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: WayPoint.cs
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
 *  25.10.2021  created
 *  
 *****************************************************************************/

public class WayPoint
{
    public GameObject wayPointGO;
    private int xCoordinate;
    private int zCoordinate;

    public WayPoint(int _xCoordinate, int _zCoordinate)
    {
        xCoordinate = _xCoordinate;
        zCoordinate = _zCoordinate;
    }
}
