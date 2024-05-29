using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.SetComponentScore(GetComponent<Text>());
    }

    void Update()
    {
        
    }
}
