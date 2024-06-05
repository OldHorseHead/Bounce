using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
public enum GameState
{
    Gameplay_Start,
    Gameplay_Running,
    Gameplay_Result,
}


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private GameManager() { }

    public int _enemyCount;
    public int _ballCount;
    public Stack<Ball> _ballPool;

    [SerializeField] GameState _gameState;
    [SerializeField] Ball _ball;
    [SerializeField] int _initBallPoolCount;
    [SerializeField] Transform _canvas;

    //Vector3 _originBallPoolInitPosition;
    #region StateStartAction
    [SerializeField] public UnityEvent _Gameplay_StartOnStartAction;
    [SerializeField] public UnityEvent _Gameplay_RunnningOnStartAction;
    [SerializeField] public UnityEvent _Gameplay_ResultOnStartAction;

    #endregion

    #region StateUpdateAndFixedUpdateAction
    [SerializeField] public UnityEvent _Gameplay_StartOnUpdateAction;
    [SerializeField] public UnityEvent _Gameplay_RunnningOnUpdateAction;
    [SerializeField] public UnityEvent _Gameplay_StartOnFixedUpdateAction;
    [SerializeField] public UnityEvent _Gameplay_RunnningOnFixedUpdateAction;
    [SerializeField] public UnityEvent _Gameplay_ResultOnUpdateAction;
    [SerializeField] public UnityEvent _Gameplay_ResultOnFixedUpdateAction;

    #endregion

    #region StateExitAction
    [SerializeField] public UnityEvent _Gameplay_StartOnExitAction;
    [SerializeField] public UnityEvent _Gameplay_RunnningOnExitAction;
    [SerializeField] public UnityEvent _Gameplay_ResultOnExitAction;
    #endregion

    #region InputListen
    //Listen SpaceKey
    [SerializeField] public UnityEvent _downSpaceKeyAction;
    [SerializeField] public UnityEvent _holdSpaceKeyUpdateAction;
    [SerializeField] public UnityEvent _upSpaceAction;

    //Listen LeftShift
    [SerializeField] public UnityEvent _downLeftShiftKeyAction;
    [SerializeField] public UnityEvent _holdLeftShiftKeyUpdateAction;
    [SerializeField] public UnityEvent _upLeftShiftAction;

    //Listen RightShift
    [SerializeField] public UnityEvent _downRightShiftKeyAction;
    [SerializeField] public UnityEvent _holdRightShiftKeyUpdateAction;
    [SerializeField] public UnityEvent _upRightShiftAction;
    #endregion

    private void Awake()
    {
        _instance = this;
        _gameState = GameState.Gameplay_Start;
        _ballPool = new();
        _Gameplay_StartOnUpdateAction.AddListener(UpdateOnListenSpaceKey);
        //_Gameplay_StartOnUpdateAction.AddListener(UpdateOnListenLeftShiftKey);
        //_Gameplay_StartOnUpdateAction.AddListener(UpdateOnListenRightShiftKey);
        _Gameplay_RunnningOnUpdateAction.AddListener(UpdateOnListenSpaceKey);
        _Gameplay_RunnningOnUpdateAction.AddListener(UpdateOnListenLeftShiftKey);
        _Gameplay_RunnningOnUpdateAction.AddListener(UpdateOnListenRightShiftKey);
    }
    void Start()
    {
        //_originBallPoolInitPosition = _poolBallInitPosition;
        InitBallPool();
        Invoke("StartOnGameState", 0.2f);
        _Gameplay_StartOnStartAction.AddListener(DownSpaceKeyToRunningGameplay);
        void DownSpaceKeyToRunningGameplay()
        {
            _downSpaceKeyAction.AddListener(SwitchToRunnningState);
            void SwitchToRunnningState()
            {
                SwitchGameState(GameState.Gameplay_Running);
                _downSpaceKeyAction.RemoveListener(SwitchToRunnningState);
            }
        }

    }
    private void Update()
    {
        UpdateOnGameState();
        UpdateStateTransition();
    }

    private void FixedUpdate() => FixedUpdateOnGameState();

    #region GameStateLife
    public void SwitchGameState(GameState stateToSwitch)
    {
        ExitFromGameState();
        _gameState = stateToSwitch;
        StartOnGameState();
    }

    void StartOnGameState()
    {
        switch (_gameState)
        {
            case GameState.Gameplay_Start:
                _Gameplay_StartOnStartAction.Invoke();
                break;
            case GameState.Gameplay_Running:
                _Gameplay_RunnningOnStartAction.Invoke();
                break;
            case GameState.Gameplay_Result:
                _Gameplay_ResultOnStartAction.Invoke();
                break;
        }
    }
    void ExitFromGameState()
    {
        switch (_gameState)
        {
            case GameState.Gameplay_Start:
                _Gameplay_StartOnExitAction.Invoke();
                break;
            case GameState.Gameplay_Running:
                _Gameplay_RunnningOnExitAction.Invoke();
                break;
        }
    }
    void UpdateOnGameState()
    {
        switch (_gameState)
        {
            case GameState.Gameplay_Start:
                _Gameplay_StartOnUpdateAction.Invoke();
                break;
            case GameState.Gameplay_Running:
                _Gameplay_RunnningOnUpdateAction.Invoke();
                break;
        }
    }
    void FixedUpdateOnGameState()
    {
        switch (_gameState)
        {
            case GameState.Gameplay_Start:
                _Gameplay_StartOnFixedUpdateAction.Invoke();
                break;
            case GameState.Gameplay_Running:
                _Gameplay_RunnningOnFixedUpdateAction.Invoke();
                break;
        }
    }
    #endregion

    #region Input Listener
    void UpdateOnListenSpaceKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _downSpaceKeyAction.Invoke();
        if (Input.GetKey(KeyCode.Space))
            _holdSpaceKeyUpdateAction.Invoke();
        if (Input.GetKeyUp(KeyCode.Space))
            _upSpaceAction.Invoke();
    }
    void UpdateOnListenLeftShiftKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _downLeftShiftKeyAction.Invoke();
        if (Input.GetKey(KeyCode.LeftShift))
            _holdLeftShiftKeyUpdateAction.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("up left shift");
            _upLeftShiftAction.Invoke();
        }
    }
    void UpdateOnListenRightShiftKey()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            _downRightShiftKeyAction.Invoke();
        if (Input.GetKey(KeyCode.RightShift))
            _holdRightShiftKeyUpdateAction.Invoke();
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            Debug.Log("up right shift");
            _upRightShiftAction.Invoke();
        }

    }
    #endregion


    #region GameStateTransition

    GameState _stateTransTo;
    void UpdateStateTransition()
    {
        switch (_gameState)
        {
            case GameState.Gameplay_Start:
                if (Gameplay_StartTransition(out _stateTransTo))
                    SwitchGameState(_stateTransTo);
                break;
            case GameState.Gameplay_Running:
                if (Gameplay_RunningTransition(out _stateTransTo))
                    SwitchGameState(_stateTransTo);
                break;
            case GameState.Gameplay_Result:
                if (Gameplay_ResultTransition(out _stateTransTo))
                    SwitchGameState(_stateTransTo);
                break;
            default:
                break;
        }
    }
    bool Gameplay_StartTransition(out GameState stateTransTo)
    {
        stateTransTo = GameState.Gameplay_Running;
        return false;
    }
    bool Gameplay_RunningTransition(out GameState stateTransTo)
    {
        if (_ballCount == 0 || _enemyCount == 0)
        {
            stateTransTo = GameState.Gameplay_Result;
            Debug.Log("GameState trans to " + stateTransTo);
            return true;
        }
        stateTransTo = GameState.Gameplay_Result;
        return false;
    }
    bool Gameplay_ResultTransition(out GameState stateTransTo)
    {
        stateTransTo = GameState.Gameplay_Start;
        return false;
    }

    #endregion


    public void LoadNextLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings - 1)
        {

            SceneManager.LoadScene(index + 1);
            Debug.Log("SeceneLoaded");
        }
        else
        {
            Debug.LogError("No more Scene!");
            EventChannelsManager.Instance.OnNoMoreLevel.Invoke();
        }
    }

 
    void InitBallPool()
    {
        // _poolBallInitPosition = _originBallPoolInitPosition;
        while (_initBallPoolCount > 0)
        {
            var ball = Instantiate(_ball, _canvas);
           // ball.transform.localPosition = _poolBallInitPosition;
           // ball._initPosition = _poolBallInitPosition;
          // _poolBallInitPosition += _poolBallInitOffset;
            _ballPool.Push(ball);
            ball.gameObject.SetActive(false);
            _initBallPoolCount -= 1;
        }
    }


}
