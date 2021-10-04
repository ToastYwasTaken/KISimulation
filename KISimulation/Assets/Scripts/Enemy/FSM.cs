using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM : StateMachineBehaviour
{
    [SerializeField]
    protected GameObject gameObject;

    [SerializeField]
    NavMeshAgent navMeshAgent;


    public void SetGO(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }

    //Called when entering state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //Called when updating state
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //Called when exiting state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
