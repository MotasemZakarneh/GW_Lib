using System;
using UnityEngine;
namespace GW_Lib.Utility
{
    [Serializable]
    public class Ref_string : Ref_Abstract<string>
    {
        [SerializeField] Var_string var_String = null;
        protected override Var_SO<string> variable
        {
            get
            {
                return var_String;
            }
        }
    }
}