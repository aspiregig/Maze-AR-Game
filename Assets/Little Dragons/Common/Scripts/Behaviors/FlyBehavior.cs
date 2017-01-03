using UnityEngine;
using System.Collections;

public class FlyBehavior : StateMachineBehaviour {
    int restore = 10;
    Transform dragoTransform;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetFloat("DragoFloat", 0);
        dragoTransform = animator.transform;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (!animator.IsInTransition(0))
        {
            // if it is falling and start to fly
            if (animator.applyRootMotion != true)
            {
               animator.GetComponent<Rigidbody>().drag = Mathf.Lerp(animator.GetComponent<Rigidbody>().drag, restore, Time.deltaTime * 10f);
                if (animator.GetComponent<Rigidbody>().drag > restore - 0.1f)
                {
                    animator.applyRootMotion = true;
                    animator.GetComponent<Rigidbody>().drag = 0;
                }
                //From Fall to Fly Uptade the Foward speed
                dragoTransform.position = Vector3.Lerp(dragoTransform.position, dragoTransform.position + animator.velocity * animator.GetFloat("Vertical")/dragoTransform.GetComponent<DragonController>().MaxSpeed/animator.GetFloat("FlySpeed"), Time.deltaTime);

            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       // animator.GetComponent<DragonController>().MaxHeight = 0;
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
