using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_AttackBehaviour : StateMachineBehaviour
{
    public HumanBodyBones leftHand;
    public HumanBodyBones rightHand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.07f && stateInfo.normalizedTime < 0.7f)
        {
            animator.GetBoneTransform(leftHand).GetComponent<SphereCollider>().enabled = true;
            animator.GetBoneTransform(rightHand).GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            animator.GetBoneTransform(leftHand).GetComponent<SphereCollider>().enabled = false;
            animator.GetBoneTransform(rightHand).GetComponent<SphereCollider>().enabled = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetBoneTransform(leftHand).GetComponent<SphereCollider>().enabled = false;
        animator.GetBoneTransform(rightHand).GetComponent<SphereCollider>().enabled = false;
    }

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
