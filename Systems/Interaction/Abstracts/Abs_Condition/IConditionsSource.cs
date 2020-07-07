using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public interface IConditionsSource
    {
        ConditionsSource GetConditionsSource();
        Object GetHolderObject();
    }
}