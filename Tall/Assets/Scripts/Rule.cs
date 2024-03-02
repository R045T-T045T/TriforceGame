using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    [SerializeField] private RuleData data;
    public Image UiSprite => data.UISprite;
    public uint EffectIndex => data.EffectIndex;
}
