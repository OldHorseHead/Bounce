using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BallBeginning : MonoBehaviour
{



    [SerializeField] float _shootPower;
    [SerializeField] float _alterDirectionTime;
    [SerializeField] Transform _currentDirection;
    [SerializeField] Transform _ballQueue;
    [SerializeField] List<Transform> _directPointList;
    [SerializeField] Vector3 _poolBallInitPosition;
    [SerializeField] Vector3 _poolBallInitOffset;
    List<Transform> _ballList;
    Rigidbody2D _ballRigid;
    Vector2 _shootDirection;
    Transform _currentDirectionPoint;
    Transform _ballTransform;
    int _directionIndex;
    bool _directionIsInited;
    int _queueCount;

    private void Awake()
    {
        _ballList = new();
    }
    void Start()
    {

        _currentDirectionPoint = _directPointList[_directionIndex];
        _currentDirection.rotation = _currentDirectionPoint.rotation;

        GameManager.Instance._Gameplay_StartOnStartAction.AddListener(GetBallAndBegginningOnGameplay_Start);//enable beginning when enter Gameplay Start state
        EventChannelsManager.Instance.OnClawGetBall.AddListener(OnBoardClawGetBall);
        EventChannelsManager.Instance.OnGetBallAward.AddListener(OnGetAwardBall);

    }



    void GetBallAndBegginningOnGameplay_Start()
    {
        _directionIsInited = false;
        GetBallFromPoolToList();
        CheckAndSwitchBall();
        EnableBeginning();
    }
    void GetBallFromPoolToList()
    {
        var ball = GameManager.Instance._ballPool.Pop().transform;
        ball.localPosition = _poolBallInitPosition + _poolBallInitOffset * _queueCount;
        _queueCount += 1;
        _ballList.Add(ball);
        ball.gameObject.SetActive(true);

    }
    void OnBoardClawGetBall(Transform ball)
    {
        GetBallFromPoolToList();

        // _ballList.Last().GetComponent<Ball>().SetInheritSetting = ball.GetComponent<Ball>().GetInheritSetting 
        CheckAndSwitchBall();
        if (_ballList.Count == 1)
            EnableBeginning();
    }
    void OnGetAwardBall()
    {
        GetBallFromPoolToList();
        CheckAndSwitchBall();
        if (_ballList.Count == 1)
            EnableBeginning();

    }

    void ShootBall()
    {
        _shootDirection = (_currentDirectionPoint.position - transform.position).normalized;
        _ballRigid.GetComponent<Rigidbody2D>().velocity = _shootDirection * _shootPower;
        _ballTransform.GetComponent<Collider2D>().enabled = true;
        _ballTransform.GetComponent<Ball>().OnShooted();
        _ballList.Remove(_ballTransform);
        _queueCount -= 1;
        CheckAndSwitchBall();
    }
    void HoldingBall() => _ballTransform.position = transform.position;

    void CheckAndSwitchBall()
    {
        if (_ballList.Count == 0)
        {
            DisableBeginning();
            return;
        }
        _ballTransform = _ballList[0];
        _ballRigid = _ballTransform.GetComponent<Rigidbody2D>();

    }
    void EnableBeginning()
    {
        GameManager.Instance._downSpaceKeyAction.AddListener(ShootBall);
        GameManager.Instance._Gameplay_StartOnUpdateAction.AddListener(HoldingBall);
        GameManager.Instance._Gameplay_RunnningOnUpdateAction.AddListener(HoldingBall);

        _ballQueue.gameObject.SetActive(true);
        _currentDirection.gameObject.SetActive(true);
        //only choose direction once at Gameplay_Start
        if (_directionIsInited)
            return;
        else
            _directionIsInited = true;
        foreach (var direct in _directPointList)
            direct.gameObject.SetActive(true);
        Invoke("AlterDirectionClockwise", _alterDirectionTime);
    }
    void DisableBeginning()
    {
        GameManager.Instance._downSpaceKeyAction.RemoveListener(ShootBall);
        GameManager.Instance._Gameplay_StartOnUpdateAction.RemoveListener(HoldingBall);
        GameManager.Instance._Gameplay_RunnningOnUpdateAction.RemoveListener(HoldingBall);

        CancelInvoke();

        foreach (var direct in _directPointList)
            direct.gameObject.SetActive(false);

        // _currentDirection.gameObject.SetActive(false);
        // _ballQueue.gameObject.SetActive(false);
    }

    void AlterDirectionClockwise()
    {
        //Debug.Log("AlterDirectionClockwise");
        _directionIndex += 1;
        if (_directionIndex >= _directPointList.Count)
            _directionIndex = _directPointList.Count - 1;
        _currentDirectionPoint = _directPointList[_directionIndex];
        _currentDirection.rotation = _currentDirectionPoint.rotation;

        if (_directionIndex == _directPointList.Count - 1)
        {
            Invoke("AlterDirectionAnticlockwise", _alterDirectionTime);
            return;
        }
        Invoke("AlterDirectionClockwise", _alterDirectionTime);
    }
    void AlterDirectionAnticlockwise()
    {
        _directionIndex -= 1;
        if (_directionIndex < 0)
            _directionIndex = 0;
        _currentDirectionPoint = _directPointList[_directionIndex];
        _currentDirection.rotation = _currentDirectionPoint.rotation;
        if (_directionIndex == 0)
        {
            Invoke("AlterDirectionClockwise", _alterDirectionTime);
            return;
        }
        Invoke("AlterDirectionAnticlockwise", _alterDirectionTime);

    }



}
