using Plum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    private const float hitStopSpeed = 10.0f;
    public static void HitStop()
    {
        Time.timeScale = .2f;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public static void ScreenShake()
    {
        MainCamera.Shake();
    }


    private void Update()
    {
        if (PauseMenuUI.isPaused) return;
        Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, hitStopSpeed * Time.deltaTime);
        Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime,.02f, hitStopSpeed * Time.deltaTime);
    }
}
