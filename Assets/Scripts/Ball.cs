using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRigid;
    [SerializeField] Vector3 _continueForce;
    [SerializeField] int _damage;

    public int Damage { get => _damage; set => _damage = value; }

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
        ballRigid.AddForce(_continueForce) ;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collide " + collision.gameObject.name);
    }
    public void OnCollideSpike()
    {
        GameManager.Instance.OnBallFall();
    }

}
