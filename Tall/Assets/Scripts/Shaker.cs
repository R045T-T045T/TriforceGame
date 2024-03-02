using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=fn3hIPLbSn8

namespace Plum.VFX
{
    [System.Serializable]
    public class Shaker
    {
        [SerializeField] private float baseAmplitude = 1.0f, baseDuration = .5f;
        [SerializeField] private AnimationCurve falloff;
        [SerializeField] private bool invertCurve = true;

        private List<System.Action<float, float>> observers = new List<System.Action<float, float>>();
        public void AddObserver(System.Action<float, float> observer) => observers.Add(observer);
        public void RemoveObserver(System.Action<float, float> observer) => observers.Remove(observer);
        public void ClearObservers() => observers.Clear();

        private void UpdateObservers(float x, float y, System.Action<float, float> onShake = null){
            foreach (System.Action<float, float> item in observers)
            {
                item?.Invoke(x, y);
            }
            onShake?.Invoke(x, y);
        }

        public IEnumerator Shake(float additionalAmplitude, float additionalDuration, System.Action<float, float> onShake = null, float ampMul = 1.0f, float durMul = 1.0f)
        {
            float elapsed = 0;
            float t = 0;
            float rand = Random.Range(0, 150);

            float usedAmplitude = (baseAmplitude + additionalAmplitude) * ampMul;
            float usedDuration = (baseDuration + additionalDuration) * durMul;
            while (elapsed < usedDuration)
            {
                elapsed += Time.deltaTime;
                t = (elapsed / usedDuration);
                t = invertCurve? 1 - t : t;
                t = falloff.Evaluate(t);

                float x = GetShake(elapsed + rand, usedAmplitude * t, Mathf.Sin);
                float y = GetShake(elapsed + rand, usedAmplitude * t, Mathf.Cos);

                UpdateObservers(x, y);
                yield return null;
            }

            UpdateObservers(0, 0);
        }

        private float GetShake(float elapsed, float intensity, System.Func<float, float> formula, float size = 50){
            return formula(elapsed * size * intensity) * intensity;
        }
    }

}
