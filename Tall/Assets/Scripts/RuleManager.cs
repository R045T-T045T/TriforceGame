using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static void DispatchEffect(Rule rule) => effects[(int)rule.EffectIndex]();


    private void Awake()
    {
        InitActions();
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
