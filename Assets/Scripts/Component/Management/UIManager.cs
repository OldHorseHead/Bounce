using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance => _instance;
    private UIManager() { }


    public ConfirmPanel _confirmPanel;//not child
    [SerializeField] Image _getBallPopNotice;


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        GameManager.Instance._Gameplay_ResultOnStartAction.AddListener(ResultConfirm);//pop result panel when enter gameplay_result state
        EventChannelsManager.Instance.OnNoMoreLevel.AddListener(NoMoreLevelConfirm);
        EventChannelsManager.Instance.OnGetBall.AddListener(PopGetBallNotice);

    }
    public void OnClickRestartAtConfirmPanel(Button button, UnityAction action)
    {
        GameManager.Instance.SwitchGameState(GameState.Gameplay_Start);
        button.onClick.RemoveListener(action);
    }
    public void OnClickNextAtConfirmPanel()
    {
        GameManager.Instance.LoadNextLevel();
    }
    void ResultConfirm()//result panel content depend on enemy or ball count 
    {
        if (GameManager.Instance._enemyCount == 0)
        {
            _confirmPanel.gameObject.SetActive(true);
            _confirmPanel.OnWinResult();
        }
        else if (GameManager.Instance._ballCount == 0)
        {
            _confirmPanel.gameObject.SetActive(true);
            _confirmPanel.OnLoseResult();
        }
    }
    void NoMoreLevelConfirm()
    {
        _confirmPanel.gameObject.SetActive(true);
        _confirmPanel.OnNoMoreLevel();
    }
    void PopGetBallNotice()
    {
        _getBallPopNotice.gameObject.SetActive(true);

        Invoke("CloseNoitce", 0.3f);
    }
    void CloseNoitce() => _getBallPopNotice.gameObject.SetActive(false);

}
