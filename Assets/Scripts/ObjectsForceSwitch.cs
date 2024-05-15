using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private ConstantForce2D cForce;
    private Vector2 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        cForce = GetComponent<ConstantForce2D>();
        forceDirection = new Vector2(0, -10);
        cForce.force = forceDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            forceDirection *= -1;
            cForce.force = forceDirection;
        }
    }

    public void ModifyForceDirection(bool enableSuperAttraction)
    {
        if (enableSuperAttraction)
        {
            forceDirection *= 2;
        }
        else
        {
            forceDirection /= 2;
        }

        cForce.force = forceDirection;
    }
}
