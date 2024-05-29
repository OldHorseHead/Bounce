using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text _scoreText;
    static GameObject _resetConfirm;
    UnityAction _resetAction;
    int _scoreNum;
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    private UIManager() { }
    private void Awake()
    {
        if (_instance == null)
            _instance = new();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetComponentScore(Text textComponent)
    {
        _scoreText = textComponent;
    }
    public static void SetComponentResetConfirm(GameObject resetConfirm)
    {
        _resetConfirm = resetConfirm;
        Debug.Log(_resetConfirm);
    }
    public void GetScore(int score)
    {
        _scoreNum += score;
        _scoreText.text = "Score: " + _scoreNum;
    }
    public void ResetConfirm()
    {
        _resetConfirm.gameObject.SetActive(true);
    }
    public void RegisterResetAction(UnityAction resetFunc)
    {
        _resetAction = resetFunc;
    }
    public void InvokeResetAction()
    {
        _resetAction.Invoke();
    }
}
