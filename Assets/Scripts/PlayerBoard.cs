using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerBoardColor
{
    White,
    Black,
    Normal,
}
public class PlayerBoard : MonoBehaviour
{
    [SerializeField] PlayerBoardColor _boardColor;
    ColorSwitchComponent _ballColorSwitch;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_boardColor == PlayerBoardColor.Normal)
            return;
        if (collision.transform.TryGetComponent<Ball>(out var ball))
        {
            if (_ballColorSwitch == null)
                _ballColorSwitch = ball.GetComponent<ColorSwitchComponent>();
            SwitchBallColor();
        }
    }
    void SwitchBallColor()
    {
        switch (_boardColor)
        {
            case PlayerBoardColor.Black:
                _ballColorSwitch.SwitchColor(ElementColor.black);
                _ballColorSwitch.SwitchCollideColor(ColorCollide.CollideWhite);
                break;
            case PlayerBoardColor.White:
                _ballColorSwitch.SwitchColor(ElementColor.white);
                _ballColorSwitch.SwitchCollideColor(ColorCollide.CollideBlack);
                break;
            case PlayerBoardColor.Normal:
                break;
        }

    }
}
