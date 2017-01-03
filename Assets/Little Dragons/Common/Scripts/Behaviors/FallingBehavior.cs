using UnityEngine;
using System.Collections;

public class FallingBehavior : StateMachineBehaviour
{
    RaycastHit JumpRay;
    float dragoFloat, MaxHeight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("DragoFloat", 1);
        animator.GetComponent<DragonController>().MaxHeight = 0;
      
        animator.applyRootMotion = false;
            animator.GetComponent<Rigidbody>().drag = 0;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            if (Physics.Raycast(animator.transform.position, -Vector3.up, out JumpRay, 100))
            {
                if (animator.GetComponent<DragonController>().MaxHeight < JumpRay.distance)
                {
                animator.GetComponent<DragonController>().MaxHeight = JumpRay.distance;
               
            }
                //Fall Blend between fall animations

                float dragoF = Mathf.Lerp(animator.GetFloat("DragoFloat"), JumpRay.distance/ animator.GetComponent<DragonController>().MaxHeight - 0.33f, Time.deltaTime * 5f);
                animator.SetFloat("DragoFloat", dragoF);
            }
    }
}
