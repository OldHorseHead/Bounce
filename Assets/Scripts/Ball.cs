using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRigid;
    public Vector3 continueForce;
    // Start is called before the first frame update
    private void Awake()
    {
        ballRigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ballRigid.AddForce(continueForce) ;


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collide " + collision.gameObject.name);
    }
}
