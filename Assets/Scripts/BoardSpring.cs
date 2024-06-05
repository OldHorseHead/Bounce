using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpring : MonoBehaviour
{
    Rigidbody2D rigidBody;
    SpringJoint2D spring;
    // public float force;
    public float distance;
    public float normalDistance;
    [SerializeField] bool isLeftBoard;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();

        RegisterToGameManager();

    }
    void RegisterToGameManager()
    {
        GameManager.Instance._Gameplay_StartOnStartAction.AddListener(ResetSpring);

        if (isLeftBoard)
        {
            GameManager.Instance._holdLeftShiftKeyUpdateAction.AddListener(KeepSpringDistanceOnUpdate);
            GameManager.Instance._upLeftShiftAction.AddListener(ResetSpring);
        }
        else
        {
            GameManager.Instance._holdRightShiftKeyUpdateAction.AddListener(KeepSpringDistanceOnUpdate);
            GameManager.Instance._upRightShiftAction.AddListener(ResetSpring);
        }

    }

    void KeepSpringDistanceOnUpdate()
    {
        Debug.Log("Keeping Spring Distance OnFixedUpdate");
        spring.distance = distance;
    }
    void ResetSpring()
    {
        spring.distance = normalDistance;
        rigidBody.freezeRotation = true;
    }
}
