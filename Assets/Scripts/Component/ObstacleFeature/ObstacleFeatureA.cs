using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFeatureA : MonoBehaviour
{
    ColorSwitchComponent _colorSwitch;
    // Start is called before the first frame update
    void Start()
    {
        _colorSwitch = GetComponent<ColorSwitchComponent>();
    }
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Ball>(out var ball))
        {
            _colorSwitch.FilpElementColor();
            _colorSwitch.FlipCollideColor();
        }
    }
}
