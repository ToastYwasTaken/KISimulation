using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: EnemyGroups.cs
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
 *  06.11.2021  created as helper-class for grouping mechanism
 *  15.11.2021  re-considered functionability to make this class hold all lists of enemygroups
 *  
 *****************************************************************************/
public static class EnemyGroups
{
    private static List<List<Enemy>> allGroups = new List<List<Enemy>>();
    public static int size = 0;

    public static void SaveCurrentGroup(List<Enemy> _groupToSave)
    {
        size++;
        allGroups.Add(_groupToSave);
    }

    public static List<List<Enemy>> GetAllGroups()
    {
        return allGroups;
    }


}

