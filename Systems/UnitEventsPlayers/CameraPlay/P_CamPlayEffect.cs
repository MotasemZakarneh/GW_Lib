using UnityEngine;

namespace GW_Lib.Utility
{
    public abstract class P_CamPlayEffect : UnitPlayable
    {
        Camera main = null;
        protected Vector3 GetPosInViewPort()
        {
            if (main == null)
            {
                main = Camera.main;
            }
            return main.WorldToViewportPoint(transform.position);
        }
    }
}