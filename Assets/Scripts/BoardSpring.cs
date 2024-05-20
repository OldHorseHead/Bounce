using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpring : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpringJoint2D spring;
   // public float force;
    public float distance;
    public float normalDistance;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(Vector2.right);
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("getkey");
            spring.distance = distance;
            //spring.GetReactionForce(force);
        }
        else
        {
            spring.distance = normalDistance;
           

        }
    }
   
}
