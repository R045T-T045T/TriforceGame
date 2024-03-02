using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plum.VFX;

namespace Plum{
    public partial class MainCamera{
        [System.Flags]
        private enum ShakeApplication
        {
            POS_X = 1, POS_Y = 2, POS_Z = 4,
            ROT_X = 8, ROT_Y = 16, ROT_Z = 32,
        }

        [SerializeField, Header("Shake-Settings")] public Shaker screenShake;
        [SerializeField] private ShakeApplication shake = ShakeApplication.POS_X;

        private static void InitShake() => activeInstance.screenShake.AddObserver(RecieveShake);

        public static void Shake(float additionalAmplitude = 0, float additionalDuration = 0, System.Action<float, float> onShake = null, float ampMul = 1, float durMul = 1)
        {
            activeInstance.StartCoroutine(
                activeInstance.screenShake.Shake(additionalAmplitude, additionalDuration, onShake, ampMul, durMul)
            );
        }

        protected static void RecieveShake(float x, float y){
            ShakeApplication cached = activeInstance.shake;
            if (cached.HasFlag(ShakeApplication.POS_X)) offsetPos.x = x;
            if (cached.HasFlag(ShakeApplication.POS_Y)) offsetPos.y = y;
            if (cached.HasFlag(ShakeApplication.POS_Z)) offsetPos.z = x;

            if (cached.HasFlag(ShakeApplication.ROT_X)) offsetRot.x = x;
            if (cached.HasFlag(ShakeApplication.ROT_Y)) offsetRot.y = y;
            if (cached.HasFlag(ShakeApplication.ROT_Z)) offsetRot.z = x;
        }
    }

}
