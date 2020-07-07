using System;
using UnityEngine;
namespace GW_Lib.Utility
{
    [Serializable]
    public class Ref_int : Ref_Abstract<int>
    {
        [SerializeField] Var_int var_Int = null;
        protected override Var_SO<int> variable
        {
            get
            {
                return var_Int;
            }
        }
    }
}