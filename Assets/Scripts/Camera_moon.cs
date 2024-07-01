using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_moon : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private Transform target;
    private Vector3 vel = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    private void LateUpdate(){
        if (target == null) return;
        
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }

    public void FocusOnTarget(Transform newTarget, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SwitchTarget(newTarget, duration));
    }

    private IEnumerator SwitchTarget(Transform newTarget, float duration)
    {
        target = newTarget;
        yield return new WaitForSeconds(duration);
        target = GameObject.FindGameObjectWithTag("Player").transform;  
    }
}
