using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRigid;
    [SerializeField] Vector3 _gravityContinueForce;
    [SerializeField] float _damage;
   // public Vector3  _initPosition;
    public float Damage { get => _damage; set => _damage = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        ballRigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        GameManager.Instance._ballCount += 1;
    }

    public void OnShooted()
    {
        SetKeepGravityForceToGamePlay_RunningFixedUpdate(true);
    }
    void KeepAddGravityForce()
    {
        ballRigid.AddForce(_gravityContinueForce);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Ball collide " + collision.gameObject.name);
    }
    public void OnCollideSpike()
    {

        GameManager.Instance._ballCount -= 1;
        SetKeepGravityForceToGamePlay_RunningFixedUpdate(false);
        DisableAndEnqueueToPool();
    }
    public void OnCollideBoardClaw(Transform ball)
    {
        //ball count no lessen for keeping gameplay_running
        SetKeepGravityForceToGamePlay_RunningFixedUpdate(false);
        EventChannelsManager.Instance.OnClawGetBall.Invoke(ball);
        EventChannelsManager.Instance.OnGetBall.Invoke();
        DisableAndEnqueueToPool();//BallBeginning will get new ball instead
        GameManager.Instance._ballCount -= 1;

    }
    void DisableAndEnqueueToPool()
    {
        gameObject.SetActive(false);
        GameManager.Instance._ballPool.Push(this);
    }
    void SetKeepGravityForceToGamePlay_RunningFixedUpdate(bool isKeep)
    {
        if (isKeep)
        {
            Debug.Log("regist gravity true");
            GameManager.Instance._Gameplay_RunnningOnFixedUpdateAction.AddListener(KeepAddGravityForce);
        }
        else
        {
            Debug.Log("regist gravity false");
            GameManager.Instance._Gameplay_RunnningOnFixedUpdateAction.RemoveListener(KeepAddGravityForce);

        }

    }

}
