//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///******************************************************************************
// * Project: KISimulation
// * File: FSM.cs
// * Version: 1.01
// * Autor:  Franz Mörike (FM);
// * 
// * 
// * These coded instructions, statements, and computer programs contain
// * proprietary information of the author and are protected by Federal
// * copyright law. They may not be disclosed to third parties or copied
// * or duplicated in any form, in whole or in part, without the prior
// * written consent of the author.
// * 
// * ChangeLog
// * ----------------------------
// *  25.10.2021  created
// *  
// *****************************************************************************/
//public class WayPoints : MonoBehaviour
//{
//    private Transform[] childrenAndMother;
//    private List<Transform> childrenList;
//    private int childrenCount;

//    public List<Transform> ChildrenList { get => childrenList; }

//    void Awake()
//    {
//        AssignChildren();
//        childrenCount = childrenAndMother.Length-1;
//    }

//    /// <summary>
//    /// Assigning children / exclude motherObj
//    /// </summary>
//    private void AssignChildren()
//    {
//        childrenAndMother = GetComponentsInChildren<Transform>();
//        childrenList = new List<Transform>();
//        foreach (Transform child in childrenAndMother)
//        {
//            //Filter by tag
//            if (child.tag.Equals("WayPoint"))
//            {
//                childrenList.Add(child);
//            }
//        }
//    }
//}
