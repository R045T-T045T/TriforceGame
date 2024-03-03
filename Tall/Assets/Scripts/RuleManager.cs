using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    private const float holdThrehshhold = .5f;
    public static void DispatchEffect(Rule rule) => effects[(int)rule.EffectIndex]();
    private float holdTime = 0.0f;

    private void Awake()
    {
        InitActions();
    }

    private void Update()
    {
        if (Input.GetKey("r"))
        {
            holdTime += Time.deltaTime;
            if(holdTime > holdThrehshhold)
            {
                FullGameReset();
                holdTime = 0.0f;
            }
        }
        else
        {
            holdTime = 0.0f;
        }
    }

    //Static part
    private static List<Action> effects = new List<Action>();

    private static void InitActions()
    {
        effects.Add(StopMoving0);
        effects.Add(StopScroll1);
        effects.Add(ResetPerks2); 
        effects.Add(SetVertical3);
        effects.Add(UnclampFallSpeed4);
        effects.Add(SetScrollDir5);
        effects.Add(ObsCanMove6);
    }

    public static void FullGameReset()
    {
        ResetPerks2();
        LevelGeneration.ResetLevel();
        PlayerMovement.FullReset();
        ScoreManager.ResetScore();
    }


    //Actions
    private static void StopMoving0()
    {
        PlayerMovement.SetMovementStatus(false);
    }

    private static void StopScroll1()
    {
        LevelGeneration.SetScrollStatus(false);
    }

    private static void ResetPerks2()
    {
        PlayerMovement.SetMovementStatus(true);
        LevelGeneration.SetScrollStatus(true);
        PlayerMovement.SetVerticalStatus(false);
        LevelGeneration.SetClampFallSpeedStatus(true);
        LevelGeneration.SetScrollDirection(1.0f);
        LevelGeneration.SetObsMoveStatus(true);
    }

    private static void SetVertical3()
    {
        PlayerMovement.SetVerticalStatus(true);
    }

    private static void UnclampFallSpeed4()
    {
        LevelGeneration.SetClampFallSpeedStatus(false);
        LevelGeneration.SetScrollStatus(true);
    }

    private static void SetScrollDir5()
    {
        LevelGeneration.SetScrollDirection(-1.0f);
    }

    private static void ObsCanMove6()
    {
        LevelGeneration.SetObsMoveStatus(false);
    }
}
