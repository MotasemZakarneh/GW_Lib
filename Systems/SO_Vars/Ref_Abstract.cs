using System;
using UnityEngine;
namespace GW_Lib.Utility
{
    [Serializable]
    public abstract class Ref_Abstract<T>
    {
        protected abstract Var_SO<T> variable { get; }
        [SerializeField] protected T referenceValue;
        [SerializeField] private bool useVariable = false;
        public T GetValue()
        {
            return useVariable ? variable.Value : referenceValue;
        }
        public void SetValue(T val)
        {
            if (useVariable)
            {
                variable.Value = val;
            }
            else
            {
                referenceValue = val;
            }
        }

    }
}