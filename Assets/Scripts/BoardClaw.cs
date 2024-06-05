using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardClaw : MonoBehaviour
{
    Collider2D _clawCollider;
    [SerializeField] bool _isLeftClaw;
    [SerializeField] Transform _leftFinger;
    [SerializeField] Transform _rightFinger;
    [SerializeField] Vector3 _openFingerOffset;
    Image _testimage;
    Vector3 _leftFingerOriginLocalPos;
    Vector3 _rightFingerOriginLocalPos;
    void Start()
    {
        _testimage = GetComponent<Image>();
        _clawCollider = GetComponent<Collider2D>();
        _leftFingerOriginLocalPos = _leftFinger.localPosition;
        _rightFingerOriginLocalPos = _rightFinger.localPosition;
        RegisterToGameManager();
    }

    void RegisterToGameManager()
    {
        GameManager.Instance._Gameplay_StartOnStartAction.AddListener(ClawStopOpen);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Ball>(out var ball))
        {
            ball.GetComponent<Collider2D>().enabled = false;// prevent trigger multiple collide ball in a frame
            ball.OnCollideBoardClaw(ball.transform);
            Debug.Log("Claw touched ball");
        }
    }

    public void ClawPreparedToCatch()
    {
        _clawCollider.enabled = true;
        _testimage.enabled = true;
        OpenClaws();
    }
    public void ClawStopOpen()
    {
        _clawCollider.enabled = false;
        _testimage.enabled = false;
        CloseClaws();
    }
    void OpenClaws()
    {
        _leftFinger.localPosition += _openFingerOffset;
        _rightFinger.localPosition -= _openFingerOffset;
    }
    void CloseClaws()
    {

        _clawCollider.enabled = false;
        _leftFinger.localPosition = _leftFingerOriginLocalPos;
        _rightFinger.localPosition = _rightFingerOriginLocalPos;
    }
}
