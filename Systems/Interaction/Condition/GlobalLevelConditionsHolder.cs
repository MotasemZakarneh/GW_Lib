using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class GlobalLevelConditionsHolder : MonoBehaviour 
	{
		public GlobalLevelConditions conditionsOfLevel;
		
		void OnDisable()
		{
            if (conditionsOfLevel)
            {
                conditionsOfLevel.ReSet();
            }
		}
	}
}