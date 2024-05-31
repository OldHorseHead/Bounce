using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private GameManager() { }

    static int _enemiesCount;


    private void Awake()
    {
        _instance = new();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateOnLevelClear();
    }
    public void LoadNextLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings - 1)
        {

            SceneManager.LoadScene(index + 1);
            Debug.Log("SeceneLoaded");
        }
        else
            Debug.LogError("No more Scene!");
    }
    public void AddEnemy()
    {
        _enemiesCount++;
    }
    public void AddEnemies(int enemiesCount)
    {
        _enemiesCount += enemiesCount;
    }

    public void UpdateOnLevelClear()
    {
        if (_enemiesCount == 0)
        {
            Debug.Log("LEVEL CLEAR");
            UIManager.Instance.LevelClearNotice();
        }
    }

    public void OnBallFall()
    {
        Debug.Log("ball fall!");
    }
}
