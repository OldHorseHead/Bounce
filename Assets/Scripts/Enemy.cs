using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ColorImageChangeable
{
    [SerializeField] int _hp;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Image _targetImage;
    ColorSwitchComponent _ballSwitch;
    ColorSwitchComponent _colorSwitch;
    int _currentHp;
    // Start is called before the first frame update
    void Start()
    {
        _colorSwitch = GetComponent<ColorSwitchComponent>();
        _currentHp = _hp;
        GameManager.Instance.AddEnemy();
        Debug.Log(_currentHp);
    }

    // Update is called once per frame
    void Update()
    {

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
            _hpSlider.value = (float)_currentHp / _hp;
        }

    }
    public Image GetColorTargetImage() => _targetImage;

}
