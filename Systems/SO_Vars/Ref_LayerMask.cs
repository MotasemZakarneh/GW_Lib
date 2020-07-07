using System;
using UnityEngine;

namespace GW_Lib.Utility
{
    [Serializable]
    public class Ref_LayerMask : Ref_Abstract<LayerMask>
    {
        public Ref_LayerMask()
        {
            //SetValue( 1 << Constants.playerLayer | 1 << Constants.playerSpawnablesLayer );
        }
        [SerializeField] Var_LayerMask var_LayerMask = null;
        protected override Var_SO<UnityEngine.LayerMask> variable
        {
            get
            {
                return var_LayerMask;
            }
        }
    }
}