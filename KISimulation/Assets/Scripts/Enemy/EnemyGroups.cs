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
 *  17.11.2021  updated access modifiers, added comments
 *  
 *****************************************************************************/
public static class EnemyGroups
{
    public static Dictionary<List<Enemy>, int> mainGroupList = new Dictionary<List<Enemy>, int>();  //includes all groups
    public static int groupCount = 0;

    public static List<Enemy> subGroupList = new List<Enemy>();
    public static int enemyCount = 0;

    /// <summary>
    /// Add a list of enemys to the mainGroupList
    /// </summary>
    /// <param name="_groupToSave">List of the group to save</param>
    public static void AddCurrentGroupToAllGroups(List<Enemy> _groupToSave)
    {
        mainGroupList.Add(_groupToSave, groupCount);
        subGroupList = _groupToSave;
        groupCount++;
    }

    /// <summary>
    /// Adds an enemy to the list
    /// </summary>
    /// <param name="_enemy">An enemy object</param>
    public static void AddEnemyToCurrentList(Enemy _enemy)
    {
        subGroupList.Add(_enemy);
        enemyCount++;
    }

    /// <summary>
    /// Returns the count of a certain enemy within a list
    /// </summary>
    /// <param name="_enemy">the enemy whose position in the list is to be determined</param>
    /// <returns>int count</returns>
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

    /// <summary>
    /// Returns the list in the dictionary at a certain value
    /// </summary>
    /// <param name="_groupCountOfList">value of the list in the dictionary</param>
    /// <returns>List<Enemy> an enemy list</Enemy></returns>
    public static List<Enemy> GetCurrentList(int _groupCountOfList)
    {
        return mainGroupList.FirstOrDefault(x => x.Value == _groupCountOfList).Key;
    }

    /// <summary>
    /// Searches for an enemy at a certain value in the dictionary
    /// at a certain position in the list
    /// </summary>
    /// <param name="_groupCountOfList">position in dictionary</param>
    /// <param name="_enemyCountInList">position in list</param>
    /// <returns>Enemy enemy</returns>
    private static Enemy GetCurrentEnemy(int _groupCountOfList ,int _enemyCountInList)
    {
        List<Enemy> curList = GetCurrentList(_groupCountOfList);
        return curList[_enemyCountInList];
    }


}

