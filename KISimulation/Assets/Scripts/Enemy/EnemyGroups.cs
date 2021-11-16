using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
 *  16.11.2021  added methods | TODO: update access modifiers
 *  
 *****************************************************************************/
public static class EnemyGroups
{
    public static Dictionary<List<Enemy>, int> mainGroupList = new Dictionary<List<Enemy>, int>();  //includes all groups
    public static int groupCount = 0;

    public static List<Enemy> subGroupList = new List<Enemy>();
    public static int enemyCount = 0;

    public static void AddCurrentGroupToAllGroups(List<Enemy> _groupToSave)
    {
        mainGroupList.Add(_groupToSave, groupCount);
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

    /// <summary>
    /// For debugging purposes. group and enemycounts are displayed +1
    /// </summary>
    public static void DisplayGroups()
    {
        for (int i = 0; i < groupCount; i++)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                Debug.Log($"Group {i+1} : member {j+1} : name: {GetCurrentEnemy(i, j).name}");
            }
        }
    }

    public static List<Enemy> GetCurrentList(int _groupCountOfList)
    {
        return mainGroupList.FirstOrDefault(x => x.Value == _groupCountOfList).Key;
    }

    //private?
    public static Enemy GetCurrentEnemy(int _groupCountOfList ,int _enemyCountInList)
    {
        List<Enemy> curList = GetCurrentList(_groupCountOfList);
        return curList[_enemyCountInList];
    }


}

