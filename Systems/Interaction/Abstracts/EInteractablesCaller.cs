using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class EInteractablesCaller : MonoBehaviour
    {
        public void SpreadEvent(string e)
        {
            foreach(EInteractable i in FindObjectsOfType<EInteractable>())
            {
                i.RecieveEvent(e);
            }
        }
    }
}