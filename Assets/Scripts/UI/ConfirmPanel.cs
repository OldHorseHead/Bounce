using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] Text _title;
    [SerializeField] Text _buttonText;
    [SerializeField] Button _button;
    void Start()
    {
        UIManager.Instance._confirmPanel = this;
        gameObject.SetActive(false);
    }


    public void OnWinResult()
    {
        _title.text = "You Win";
        _buttonText.text = "Next";
        _button.onClick.AddListener(UIManager.Instance.OnClickNextAtConfirmPanel);
    }
    public void OnLoseResult()
    {
        _title.text = "You Lose";
        _buttonText.text = "Restart";

        _button.onClick.AddListener(Action);

        void Action() => UIManager.Instance.OnClickRestartAtConfirmPanel(_button, Action);//for remove listener after each call
    }
    public void OnNoMoreLevel()
    {
        _title.text = "Thanks!";
        _buttonText.text = "Quit";
        _button.onClick.AddListener(Application.Quit);
    }
}
