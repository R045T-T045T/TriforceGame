using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plum.VFX;

namespace Plum{
    [RequireComponent(typeof(Camera))]
    public partial class MainCamera : MonoBehaviour
    {
        public static bool IsSet => activeInstance != null;
        private static MainCamera activeInstance;
        private static GameObject intermediate;
        public static Transform Transform{get => intermediate.transform; }

        private static Dictionary<string, Vector3> posOffsets = new Dictionary<string, Vector3>();
        private static Dictionary<string, Vector3> rotOffsets = new Dictionary<string, Vector3>();

        private static Vector3 offsetPos, offsetRot;
        private static Camera cam;
        public static Camera Camera
        {
            get
            {
                if(cam == null)
                {
                    cam = activeInstance.GetComponent<Camera>();
                }
                return cam;
            }
        }

        
        public void OnEnable(){
            activeInstance = this;
            cam = GetComponent<Camera>();
        }

        private void Awake(){
            OnEnable();
            InitShake();
            ConstructIntermediateTransform();
        }

        private void ConstructIntermediateTransform()
        {
            intermediate = new GameObject("MainCamera_MainTransform");
            intermediate.transform.position = transform.position;
            intermediate.transform.parent = transform.parent;
            intermediate.transform.rotation = transform.rotation;
            transform.parent = intermediate.transform;
            transform.localPosition = Vector3.zero;
        }

        #region EXTERN
        public static void Offset_Pos(string key, Vector3 value) { if (!posOffsets.ContainsKey(key)) posOffsets.Add(key, value); posOffsets[key] = value; }
        public static void Offset_Rot(string key, Vector3 value) { if (!rotOffsets.ContainsKey(key)) rotOffsets.Add(key, value); rotOffsets[key] = value; }

        public static void RemoveOffset_Pos(string key) { if (posOffsets.ContainsKey(key)) posOffsets.Remove(key); }
        public static void RemoveOffset_Rot(string key) { if (rotOffsets.ContainsKey(key)) rotOffsets.Remove(key); }
        #endregion


        //Add intermediate transform for shake or alt code!
        private void Update(){
            transform.localPosition = GetOffset_Pos();
            transform.localRotation = Quaternion.Euler(GetOffset_Rot());
        }

        private Vector3 GetOffset_Pos()
        {
            Vector3 offset = offsetPos;
            foreach (KeyValuePair<string, Vector3> v in posOffsets) offset += v.Value;
            return offset;
        }

        private Vector3 GetOffset_Rot()
        {
            Vector3 offset = offsetRot;
            foreach (KeyValuePair<string, Vector3> v in rotOffsets) offset += v.Value;
            return offset;
        }
    }
}
