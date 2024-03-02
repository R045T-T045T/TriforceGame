using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plum.Base
{
    public interface IDynamicPoolable
    {
        public void OnMarkUnuse() { }
        public void OnMarkUse() { }
        public void OnMarkReUse() { }
    }

    public class DynamicPool<T> where T : class, IDynamicPoolable
    {
        private int maxAmount;
        private GameObject parent; public GameObject Parent => parent;
        private List<T> unused = new List<T>(), inUsage = new List<T>();

        public DynamicPool(int maxAmount){
            this.maxAmount = maxAmount;
        }




#region INTERN_UTILS
        private void SetAsChild(MonoBehaviour reference){
            if(parent == null){
                parent = new GameObject();
                parent.gameObject.name = typeof(T) + " - dynamic pool";
            }

            parent.name = reference.GetType() + " - dynamicPool parent";
            reference.transform.parent = parent.transform;
        }
#endregion




#region EXTERN_UTILS
        public T[] GetAll_Unused() => unused.ToArray();
        public T[] GetAll_Inuse() => inUsage.ToArray();
        public int InUseAmount => inUsage.Count;
        public int UnusedAmount => unused.Count;
        public bool HasUnusedInstances() => unused.Count > 0;
        public bool HasMoreSpace() => inUsage.Count < maxAmount;


        public void MarkInstanceUnused(T instance)
        {
            unused.Add(instance);
            if (inUsage.Contains(instance)) inUsage.Remove(instance);
            instance.OnMarkUnuse();
        }

        public void MarkInstanceInUse(T instance){
            if(instance is MonoBehaviour) SetAsChild(instance as MonoBehaviour);
            inUsage.Add(instance);
            if (unused.Contains(instance)) unused.Remove(instance);
            instance.OnMarkUse();
        }

        public void RegressAll(System.Action<T> perInstanceAction = null){
            foreach(T instance in inUsage){
                perInstanceAction?.Invoke(instance);
            }

            foreach (T item in inUsage) item.OnMarkUnuse();
            unused.AddRange(inUsage);
            inUsage.Clear();
        }


        public T GetLasttUnusedInstance()
        {
            if(unused.Count <= 0){
                Debug.LogWarning("WARNING - tried to get unused instance, while there were none!");
                return null;
            }

            T p = unused[0];
            unused.Remove(p);
            inUsage.Add(p);
            p.OnMarkUse();
            return p;
        }

        public T GetLastInUseInstance(){
            if(inUsage.Count <= 0){
                Debug.LogWarning("WARNING - tried to get in-use instance, while there were none!");
                return null;
            }

            T p = inUsage[0];
            inUsage.Remove(p);
            inUsage.Add(p);
            p.OnMarkReUse();
            return p;
        }

#endregion




#region EXTERN_REQUESTS
        public T GetInstance(System.Func<T> createNewInstance){
            T instance;
            if(HasUnusedInstances()){
                instance = GetLasttUnusedInstance();
            } else{
                if(HasMoreSpace()){
                    instance = createNewInstance();
                    MarkInstanceInUse(instance);
                }
                else{
                    instance = GetLastInUseInstance();
                }
            }

            return instance;
        }
#endregion
    }
}
