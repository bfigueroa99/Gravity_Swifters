using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] movementPoints;  
    [SerializeField] private float movementSpeed;         
    [SerializeField] private float delayAtPoint = 0f;     

    private int nextPointIndex = 1;
    private bool isMovingForward = true;
    private bool isWaiting = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            if (!isWaiting)
            {
                if (isMovingForward && nextPointIndex + 1 >= movementPoints.Length)
                {
                    isMovingForward = false;
                }
                else if (!isMovingForward && nextPointIndex <= 0)
                {
                    isMovingForward = true;
                }

                if (Vector2.Distance(transform.position, movementPoints[nextPointIndex].position) < 0.1f)
                {
                    // Llegó al punto, cambiar la animación
                    StartCoroutine(SetAnimationForPoint(nextPointIndex));

                    // Esperar en el punto
                    isWaiting = true;
                    yield return new WaitForSeconds(delayAtPoint);
                    isWaiting = false;

                    // Decidir el siguiente punto
                    nextPointIndex = isMovingForward ? nextPointIndex + 1 : nextPointIndex - 1;
                }

                transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextPointIndex].position, movementSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    private IEnumerator SetAnimationForPoint(int pointIndex)
    {
        if (pointIndex == 0 || pointIndex == 3)
        {
            animator.Play("Idle");
            yield return new WaitForSeconds(1.5f);  
            animator.Play("Fly");
        }
        else
        {
            animator.Play("Fly");
        }

        yield return null;
    }
}
