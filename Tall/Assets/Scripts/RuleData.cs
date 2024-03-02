using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RuleData", menuName = "Triforce/RuleData", order = 0)]
public class RuleData : ScriptableObject
{
    [SerializeField] private Image uiSprite; public Image UISprite => UISprite;
    [SerializeField] private Sprite worldSprite_default; public Sprite WorldSprite_Def => worldSprite_default;
    [SerializeField] private Sprite worldSprite_damaged; public Sprite WorldSprite_Damaged => worldSprite_damaged;
    [SerializeField] private uint effectIndex; public uint EffectIndex => effectIndex;
}
