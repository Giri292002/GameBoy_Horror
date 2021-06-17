using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FSM_Chase : AI_FSM_Parent
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _ai._aiState = AIController.AIStates.chase;
        _ai._ai.maxSpeed = _ai.maxChaseSpeed;
        _ai.StartAttacking();
        _ai.SetPatrolDestination(_ai._player.transform.position);
        _ai._WalkSource.Play();
        _ai._SFXSource.volume = 0.2f;
        _ai._SFXSource.PlayOneShot(_ai._sawPlayer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ai.SetPatrolDestination(_ai._player.transform.position);
        if (_ai._ai.reachedDestination)
        {
            Debug.Log("Caught up to player");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
