using Plum.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakParticles : MonoBehaviour, IDynamicPoolable
{
    private static DynamicPool<BreakParticles> pool = new DynamicPool<BreakParticles>(5);

    private static BreakParticles CreateNewInstance()
    {
        GameObject g = new GameObject();
        g.name = "BreakParticle";
        return g.AddComponent<BreakParticles>();
    }
    public static BreakParticles RequestInstance(Rule source, Vector2 vel)
    {
        BreakParticles p = pool.GetInstance(CreateNewInstance);
        p.Initialize(source, vel);
        return p;
    }


    private SpriteRenderer renderer;
    private Rigidbody2D rb;
    private float lifeTime = 0.0f;
    public void Initialize(Rule source, Vector2 vel)
    {
        if(renderer == null) renderer = gameObject.AddComponent<SpriteRenderer>();
        if(rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        renderer.sprite = source.DamagedSprite;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.0f;
        rb.AddForce(new Vector2(LevelGeneration.ScrollDir * -2 - vel.y, -vel.x * Random.Range(1, 5)), ForceMode2D.Impulse);
        renderer.color = Color.grey;
    }


    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > 5.0f)
        {
            pool.MarkInstanceUnused(this);
            enabled = false;
        }
    }
}
