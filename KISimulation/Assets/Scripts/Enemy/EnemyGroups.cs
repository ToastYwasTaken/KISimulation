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
    public static List<List<Enemy>> mainGroupList = new List<List<Enemy>>();
    public static int groupCount = 0;

    public static List<Enemy> subGroupList = new List<Enemy>();
    public static int enemyCount = 0;

    public static void AddCurrentGroupToAllGroups(List<Enemy> _groupToSave)
    {
        mainGroupList.Add(_groupToSave);
        subGroupList = _groupToSave;
        groupCount++;
    }

    public static void AddEnemyToCurrentList(Enemy _enemy)
    {
        subGroupList.Add(_enemy);
        enemyCount++;
    }

    public static int GetGroupNumberOfEnemy(Enemy _enemy)
    {
        int count = 0;
        for(int i = 0; i < groupCount; i++)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                if (subGroupList[j].name.Equals(_enemy.name))
                {
                    count = i;
                }
            }
        }
        return count;
    }

    public static void DisplayGroups()
    {
        for (int i = 0; i < groupCount; i++)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                Debug.Log($"Group {i} : member {enemyCount}");
            }
        }
    }


}

