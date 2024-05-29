using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ElementColor
{
    white,
    black,
}
public enum ColorCollide
{
    CollideWhite,
    CollideBlack,
    CollideBoth,
}
public class ColorSwitchComponent : MonoBehaviour
{
    public ElementColor _elementColor;
    public ColorCollide _collideColor;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        SwitchColor(_elementColor);
        SwitchCollideColor(_collideColor);
    }
    public void SwitchColor(ElementColor color)
    {
        if (color == ElementColor.white)
            image.color = new Color(255, 255, 255, 255);
        else if (color == ElementColor.black)
            image.color = new Color(0, 0, 0, 255);
    }
    public void SwitchCollideColor(ColorCollide collideColor)
    {
        switch (collideColor)
        {
            case ColorCollide.CollideBlack:
                gameObject.layer = LayerMask.NameToLayer("CollideBlack");
                break;
            case ColorCollide.CollideWhite:
                gameObject.layer = LayerMask.NameToLayer("CollideWhite");
                break;
            case ColorCollide.CollideBoth:
                gameObject.layer = LayerMask.NameToLayer("CollideBoth");
                break;
        }
    }
    public void FilpElementColor()
    {
        if (_elementColor == ElementColor.white)
        {
            _elementColor = ElementColor.black;
            SwitchColor(ElementColor.black);
        }
        else if (_elementColor == ElementColor.black)
        {
            _elementColor = ElementColor.white;
            SwitchColor(ElementColor.white);
        }
    }
    public void FlipCollideColor()
    {
        if (_collideColor == ColorCollide.CollideWhite)
        {
            _collideColor = ColorCollide.CollideBlack;
            gameObject.layer = LayerMask.NameToLayer("CollideBlack");
        }
        else if (_collideColor == ColorCollide.CollideBlack)
        {
            _collideColor = ColorCollide.CollideWhite;
            gameObject.layer = LayerMask.NameToLayer("CollideWhite");
        }
    }
}
