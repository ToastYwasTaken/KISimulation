using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: KISimulation
 * File: EnemyGroup.cs
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
 *  
 *****************************************************************************/
public class EnemyGroup 
{
    private List<Enemy> groupMembers = new List<Enemy>();
    private int size;
    public string name;
    public List<Enemy> GroupMembers { get => groupMembers;}
    public int Size{ get => size; }


    public EnemyGroup(string _name)
    {
        size = 0;
        _name = name;
    }

    public void AddMember(Enemy _enemyToAdd)
    {
        groupMembers.Add(_enemyToAdd);
        size++;
    }
    
    public void DisplayEnemyGroup()
    {
        foreach (Enemy enemy in groupMembers)
        {
            Debug.Log("List member: " + enemy.name);
        }
    }
}

