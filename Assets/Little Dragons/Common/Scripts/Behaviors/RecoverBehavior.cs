using UnityEngine;
using System.Collections;

public class RecoverBehavior : StateMachineBehaviour
{

    RaycastHit JumpRay;
    float dragoFloat, MaxHeight;

     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            if (animator.applyRootMotion != true)
            {
                animator.applyRootMotion = true;
                animator.GetComponent<Rigidbody>().drag = 0;
            }
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.applyRootMotion != true)
        {
            animator.applyRootMotion = true;
            animator.GetComponent<Rigidbody>().drag = 0;
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Smooth Stop when RecoverFalls
        animator.applyRootMotion = false;
        if (stateInfo.normalizedTime < 0.9f)
        {
            animator.GetComponent<Rigidbody>().drag = Mathf.Lerp(animator.GetComponent<Rigidbody>().drag, 3, Time.deltaTime * 10f);
        }
        else
        {
            animator.applyRootMotion = true;
            animator.GetComponent<Rigidbody>().drag = 0;
        }
    }
}
