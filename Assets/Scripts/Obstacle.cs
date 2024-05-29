using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ObstacleType
{
    A,
    B,
    C,
}

public class Obstacle : MonoBehaviour
{

    public ObstacleType obstacleType;
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;
        AddObstacleFeatureComponent(obstacleType);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddObstacleFeatureComponent(ObstacleType obstacleType)
    {
        switch (obstacleType)
        {
            case ObstacleType.A:
                transform.AddComponent<ObstacleFeatureA>();
                break;
            case ObstacleType.B:
                transform.AddComponent<ObstacleFeatureB>();
                break;
            case ObstacleType.C:
                transform.AddComponent<ObstacleFeatureC>();
                break;
        }
    }
}
