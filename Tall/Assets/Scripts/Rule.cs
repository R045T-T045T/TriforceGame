using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    private RuleData currentData;
    public Image UiSprite => currentData.UISprite;
    public uint EffectIndex => currentData.EffectIndex;

    private SpriteRenderer renderer;
    private BoxCollider2D collider;
    private bool wasInsideOnce = false; public bool WasInside { get => wasInsideOnce; set => wasInsideOnce = value; }
    private Vector4 moveLane; public Vector4 MoveLane { get => moveLane; set => moveLane = value; }


    public void InitializeAs(RuleData data)
    {
        currentData = data;
        if (renderer == null) renderer = gameObject.AddComponent<SpriteRenderer>();
        if(collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        renderer.sprite = data.WorldSprite_Def;

        Vector2 newSize = new Vector2();
        newSize.x = renderer.sprite.bounds.max.x;
        newSize.y = renderer.sprite.bounds.max.y;
        collider.size = newSize;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RuleManager.DispatchEffect(this);
        //Play Effect
    }
}
