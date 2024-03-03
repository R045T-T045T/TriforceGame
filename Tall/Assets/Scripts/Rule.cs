using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    private RuleData currentData;
    public Image UiSprite => currentData.UISprite;
    public Sprite WorldSprite => currentData.WorldSprite_Def;
    public Sprite DamagedSprite => currentData.WorldSprite_Damaged;
    public uint EffectIndex => currentData.EffectIndex;

    private SpriteRenderer renderer;
    private BoxCollider2D collider;
    private bool wasInsideOnce = false; public bool WasInside { get => wasInsideOnce; set => wasInsideOnce = value; }
    private Vector4 moveLane; public Vector4 MoveLane { get => moveLane; set => moveLane = value; }
    private bool isActive = true;


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
        newSize.x = renderer.sprite.bounds.max.x * 1.7f;
        newSize.y = renderer.sprite.bounds.max.y * 1.7f;
        collider.size = newSize;
        UpdateState(true);
    }

    private void UpdateState(bool state)
    {
        isActive = state;
        renderer.enabled = isActive;
        collider.enabled = isActive;    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 v = collision.GetComponent<Rigidbody2D>().velocity; 
        RuleManager.DispatchEffect(this);
        BreakParticles bp = BreakParticles.RequestInstance(this, v);
        bp.transform.position = transform.position;
        VFX.HitStop();
        VFX.ScreenShake();
        SoundEffects.PlayImpactSFX();
        ScoreManager.Increase();
        UpdateState(false);
    }
}
