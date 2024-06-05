using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ColorImageChangeable
{
    [SerializeField] int _hpMax;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Image _targetImage;
    [SerializeField] bool _moveable;
    [SerializeField] float _moveSpeed;
    [SerializeField] List<Transform> _roadTargetList;
    [Header("SPIKE")]
    [SerializeField] bool _spikeRotatable;
    [SerializeField] float _spikeRotateSpeed;
    [SerializeField] bool _isSpikeClockwise;
    [SerializeField] List<Transform> _spikeList;
    ColorSwitchComponent _ballSwitch;
    ColorSwitchComponent _colorSwitch;
    float _currentHp;
    Vector3 _originPosition;
    Vector3 _moveDirection;
    int _roadTargetIndex = 0;
    Vector3[] _roadTargetPositions;
    Quaternion _originSpikeRotation;
    Vector3 _originSpikeLocation;
    Quaternion[] _originSpikeRotations;
    Vector3[] _originSpikeLocations;

    void Start()
    {
        _colorSwitch = GetComponent<ColorSwitchComponent>();


        FeatrueRegisterInit();

        _originPosition = transform.position;
        GameManager.Instance._Gameplay_StartOnStartAction.AddListener(ResetProperty);

    }
    void FeatrueRegisterInit()
    {
        if (_moveable)
        {
            GetRoadPositions();
            GameManager.Instance._Gameplay_StartOnUpdateAction.AddListener(Movement);
            GameManager.Instance._Gameplay_RunnningOnUpdateAction.AddListener(Movement);
        }
        if (_spikeRotatable)
        {
            List<Quaternion> spikeRotations = new();
            List<Vector3> spikeLocations = new();
            foreach (var spike in _spikeList)
            {
                spikeRotations.Add(spike.rotation);
                spikeLocations.Add(spike.localPosition);
            }
            _originSpikeRotations = spikeRotations.ToArray();
            _originSpikeLocations = spikeLocations.ToArray();

            if (_isSpikeClockwise)
                _spikeRotateSpeed = -_spikeRotateSpeed;

            GameManager.Instance._Gameplay_StartOnUpdateAction.AddListener(SpikeRotation);
            GameManager.Instance._Gameplay_RunnningOnUpdateAction.AddListener(SpikeRotation);
      
        }
    }
    void GetRoadPositions()
    {
        List<Vector3> positions = new();
        foreach (var targetTransform in _roadTargetList)
            positions.Add(targetTransform.position);
        _roadTargetPositions = positions.ToArray();

    }
    void Movement()
    {
        _moveDirection = (_roadTargetPositions[_roadTargetIndex] - transform.position).normalized;
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);

        CheckAndSwitchRoadTarget();
    }
    void SpikeRotation()
    {
        foreach (var spike in _spikeList)
            spike.RotateAround(transform.position, transform.forward, _spikeRotateSpeed * Time.deltaTime);
    }
    void CheckAndSwitchRoadTarget()
    {
        //Debug.Log(distance);
        if (Vector3.Distance(transform.position, _roadTargetPositions[_roadTargetIndex]) <=2f)
            _roadTargetIndex += 1;
        if (_roadTargetIndex >= _roadTargetList.Count)
            _roadTargetIndex = 0;
    }

    private void OnEnable()
    {
        Invoke("InitToGameManager", 0.05f);//wait for GameManager prepare completely when init game
    }
    private void OnDisable()
    {
    }
    void InitToGameManager()//call by Invoke() when OnEnable
    {
        ResetProperty();
        GameManager.Instance._enemyCount += 1;
    }
    void ResetProperty()
    {
        gameObject.SetActive(true);
        ResetHp();
        transform.position = _originPosition;
        ResetSpike();
        _roadTargetIndex = 0;
    }
    void ResetSpike()
    {
        if (_spikeList == null)
            return;
        for (int i = 0; i < _spikeList.Count; i++)
        {
            _spikeList[i].rotation = _originSpikeRotations[i];
            _spikeList[i].localPosition = _originSpikeLocations[i];
        }
     
    }
    void ResetHp()
    {
        _currentHp = _hpMax;
        _hpSlider.value = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collide " + collision.gameObject.name);
        if (collision.transform.TryGetComponent<Ball>(out var ball))
        {
            if (_ballSwitch == null)
                _ballSwitch = ball.GetComponent<ColorSwitchComponent>();
            CheckBallDamage(ball);
        }

    }
    void CheckBallDamage(Ball ball)
    {
        if (_ballSwitch._elementColor == _colorSwitch._elementColor)
        {
            _currentHp -= ball.Damage;
            _hpSlider.value = _currentHp / _hpMax;
        }
        if (_currentHp <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance._enemyCount -= 1;
            EventChannelsManager.Instance.OnGetBallAward.Invoke();
            EventChannelsManager.Instance.OnGetBall.Invoke();
        }
    }
    public Image GetColorTargetImage() => _targetImage;

}
