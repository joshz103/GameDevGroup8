using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_idle : StateMachineBehaviour
{
    [SerializeField] private float timeRandom;


    ////OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeRandom = Random.Range(0.0f, 1.0f);
        animator.SetFloat("Idle_Animation", timeRandom);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime % 1 > 0.98)
        {
            timeRandom = Random.Range(0.0f, 1.0f);
            animator.SetFloat("Idle_Animation", timeRandom);
        }
        
    }

}
