using UnityEngine;
using System.Collections;

public class RandomBehavior : StateMachineBehaviour {

    public int Range;
    
	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
        animator.SetInteger("DragoInt", Random.Range(0, Range));


      
        //If is in idle, start to count , to get to sleep
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
           
            animator.GetComponent<DragonController>().Tired++;
            //Debug.Log(animator.GetComponent<DragonController>().Tired);
            if (animator.GetComponent<DragonController>().Tired >= animator.GetComponent<DragonController>().GotoSleep-1)
            {
                animator.SetInteger("DragoInt", -1);
                animator.GetComponent<DragonController>().Tired=0;
            }
          
        }
	
    }

}
