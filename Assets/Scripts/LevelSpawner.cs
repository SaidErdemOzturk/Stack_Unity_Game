using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatformType
{
    Circle,
    Cube,
    Flower,
    Spike,
    Square,
    Win
}
public class LevelSpawner : MonoBehaviour
{
    public GameObject winPrefab;
    public SceneType[] sceneTypes;
    private GameObject obstacleType;

    [HideInInspector]
    public SceneType sceneType;

    private int playerLevel;
    private int i;
    private GameObject tempObstacle;
    private GameObject obstacle;
    private Transform lastTempObstacleTransform;
    // Start is called before the first frame update
    void Start()
    {
        RandomLevel();
        playerLevel = PlayerPrefs.GetInt("Level");
        for (i = 0; i < playerLevel+20; i++)
        {
            tempObstacle = Instantiate(sceneType.obstacleType);
            if (i != 0)
            {
                tempObstacle.GetComponent<ObstacleController>().Init(sceneType, lastTempObstacleTransform);
            }
            else
            {
                tempObstacle.GetComponent<ObstacleController>().Init(sceneType, tempObstacle.transform);

            }

            lastTempObstacleTransform = tempObstacle.transform;

        }
        GameObject finish = Instantiate(sceneType.winObstacle);
        finish.GetComponent<ObstacleController>().Init(sceneType, lastTempObstacleTransform);
    }

    public void RandomLevel()
    {
        int randLevel = Random.Range(0, sceneTypes.Length);
        sceneType = sceneTypes[randLevel];
    }
}
