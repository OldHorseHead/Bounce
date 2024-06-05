using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventChannelsManager : MonoBehaviour
{
    private static EventChannelsManager _instance;
    public static EventChannelsManager Instance => _instance;
    private EventChannelsManager() { }

    public UnityEvent<Transform> OnClawGetBall;//listen by ballBeginning to get the ball

    public UnityEvent OnNoMoreLevel;//listen by ConfirmPanel to quit game

    public UnityEvent OnGetBall;//listen by UI to notice each time get ball

    public UnityEvent OnGetBallAward;//listen by ballBeginning to active a new ball

    private void Awake()
    {
        _instance = this;
    }
  
}
