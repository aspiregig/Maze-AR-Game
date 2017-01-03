using UnityEngine;
using System.Collections;

public class JumpBehavior : StateMachineBehaviour {

    RaycastHit JumpRay;
    DragonController myDrago;
    float dragoFloat, MaxJumpPoint;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Get the Take off Point to compare if is falling
        MaxJumpPoint = animator.transform.position.y;
        animator.SetFloat("DragoFloat", 1);
        animator.SetInteger("DragoInt", 1);
        animator.applyRootMotion = true;
        myDrago = animator.GetComponent<DragonController>();
        myDrago.MaxHeight = 0;



    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //if is still in flat ground stay jumping and not falling
        if (Physics.Raycast(animator.transform.position, -Vector3.up, out JumpRay, 10f,myDrago.GroundLayer))
        {
            
            if (MaxJumpPoint-JumpRay.point.y > 0.03f)
            {
                // Changue to fall if its falling
                animator.SetInteger("DragoInt",0);
            }
            else
            {
                animator.SetInteger("DragoInt", 1);
            }
            
        }

    }
}
