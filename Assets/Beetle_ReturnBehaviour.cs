using System.Collections;
using UnityEngine;

public class Beetle_ReturnBehaviour : StateMachineBehaviour
{
    private Transform beetleTransform;
    private Transform startingPosition;
    private float speed = 3f;
    private float delay = 2f; 
    private float timeEnteredState; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (beetleTransform == null)
        {
            beetleTransform = animator.gameObject.transform;
        }
        startingPosition = animator.gameObject.GetComponent<DogController>().startPosition;
        timeEnteredState = Time.time; 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - timeEnteredState > delay) // Check if delay has passed
        {
            if (Vector3.Distance(beetleTransform.position, startingPosition.position) > 0.1f)
            {
                beetleTransform.position = Vector3.MoveTowards(beetleTransform.position, startingPosition.position, speed * Time.deltaTime);
            }
            else
            {
                beetleTransform.position = startingPosition.position;
                animator.SetBool("hasReturned", true);
            }
        }
    }
}