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
    [SerializeField] BoardClaw _claw;
    [SerializeField] bool _isLeft;
    ColorSwitchComponent _ballColorSwitch;
    bool _isClawClosed;
    private void Start()
    {
        _isClawClosed = true;
        GameManager.Instance._Gameplay_RunnningOnUpdateAction.AddListener(ClawIsReadyOnUpdate);
    }

    void ClawIsReadyOnUpdate()
    {
        if (_isLeft)
        {
            if ((int)transform.rotation.eulerAngles.z == 20)
            {
                if (_isClawClosed)//for call once
                {
                    _isClawClosed = false;
                    _claw.ClawPreparedToCatch();
                }
            }
            else if ((int)transform.rotation.eulerAngles.z == 329)
            {
                if (_isClawClosed == false)
                {
                    _isClawClosed = true;
                    _claw.ClawStopOpen();
                }
            }
        }
        else
        {
            if ((int)transform.rotation.eulerAngles.z == 339)
            {
                if (_isClawClosed)//for call once
                {
                    _isClawClosed = false;
                    _claw.ClawPreparedToCatch();
                }
            }
            else if ((int)transform.rotation.eulerAngles.z == 30)
            {
                if (_isClawClosed == false)
                {
                    _isClawClosed = true;
                    _claw.ClawStopOpen();
                }
            }
        }
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
