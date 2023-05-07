using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Object", menuName = "SceneType")]
public class SceneType : ScriptableObject
{
    public Material finishMaterial;
    public Material safeMaterial;
    public Material unsafeMaterial;
    public GameObject obstacleType;
    public GameObject winObstacle;
    public GameObject[] obstacleModel;
}
