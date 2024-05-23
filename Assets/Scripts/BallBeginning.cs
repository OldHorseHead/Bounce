using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBeginning : MonoBehaviour
{
    public Transform ballTransform;
    public Transform directPoint;
    public float shootPower;
    Vector2 shootDirection;

    void Start()
    {
        ballTransform.position = transform.position;
        shootDirection = (directPoint.position - transform.position).normalized;
        ShootBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShootBall()
    {
        ballTransform.GetComponent<Rigidbody2D>().velocity = shootDirection * shootPower;
    }
    public void ResetPositionShoot()
    {
        ballTransform.position = transform.position;
        ShootBall();
    }
}
