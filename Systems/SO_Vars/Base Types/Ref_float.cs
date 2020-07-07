using System;
using UnityEngine;

namespace GW_Lib.Utility
{
    [Serializable]
    public class Ref_float : Ref_Abstract<float>
    {
        [SerializeField] Var_float var_Float = null;
        protected override Var_SO<float> variable
        {
            get
            {
                return var_Float;
            }
        }
    }
}