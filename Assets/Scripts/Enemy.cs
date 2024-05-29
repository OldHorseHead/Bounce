using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ColorSwitchComponent _ballSwitch;
    ColorSwitchComponent _colorSwitch;
    // Start is called before the first frame update
    void Start()
    {
        _colorSwitch = GetComponent<ColorSwitchComponent>();
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
            CheckBall();
        }

    }
    void CheckBall()
    {
        if (_ballSwitch._elementColor != _colorSwitch._elementColor)
            UIManager.Instance.GetScore(1);
    }
}
