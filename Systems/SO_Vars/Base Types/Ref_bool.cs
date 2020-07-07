using System;
using UnityEngine;
namespace GW_Lib.Utility
{
    [Serializable]
    public class Ref_bool : Ref_Abstract<bool>
    {
        [SerializeField] Var_bool var_Bool = null;
        protected override Var_SO<bool> variable
        {
            get
            {
                return var_Bool;
            }
        }
    }
}