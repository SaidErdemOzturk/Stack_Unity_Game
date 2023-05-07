using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Safe,
    Unsafe,
    Finish
}
public class PartController : MonoBehaviour,IPart
{
    
    private SceneType sceneType;
    private PartType partType;
    public static Action<ObstacleController> OnBallHitSafe;
    public static Action<bool> OnBallHitUnsafe;
    public static Action<bool> OnBallHitFinish;
    public static Action<ObstacleController> OnBallHitSpeed;


    public void Init(PartType partType, SceneType sceneType)
    {
        this.partType = partType;

        this.sceneType = sceneType;
        switch (partType)
        {
            case PartType.Safe:
                ChangeSafePart();
                break;
            case PartType.Unsafe:
                ChangeUnsafePart();
                break;
            case PartType.Finish:
                ChangeFinishPart();
                break;
            default:
                break;
        }
    }
    public void ChangeSafePart()
    {
        gameObject.GetComponent<Renderer>().material = sceneType.safeMaterial;
    }
    public void ChangeUnsafePart()
    {
        gameObject.tag = "Unsafe";
        gameObject.GetComponent<Renderer>().material = sceneType.unsafeMaterial;
    }
    public void ChangeFinishPart()
    {
        gameObject.tag = "Finish";
        gameObject.GetComponent<Renderer>().material = sceneType.finishMaterial;

    }
    public void OnBallHitWithClick()
    {
        switch (partType)
        {
            case PartType.Safe:
                SafeClick();
                break;
            case PartType.Unsafe:
                UnsafeClick();
                break;
            case PartType.Finish:
                FinishClick();
                break;
            default:
                break;
        }
    }

    public void SafeClick()
    {
        OnBallHitSafe?.Invoke(transform.parent.GetComponent<ObstacleController>());
    }
    public void UnsafeClick()
    {
        OnBallHitUnsafe?.Invoke(true);
    }
    public void FinishClick()
    {
        OnBallHitFinish?.Invoke(true);
    }

    public void OnBallSpeedHitWithClick()
    {
        OnBallHitSpeed?.Invoke(transform.parent.GetComponent<ObstacleController>());
    }
}
