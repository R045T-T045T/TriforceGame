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
    }
}
