using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_CommandDoorInteractable : Reaction
    {
        [Header("Door Interactable")]
        [SerializeField] DoorInteractable[] doorInteractables = null;
        [SerializeField] DoorInteractWith commandable = DoorInteractWith.Ais;

        protected override IEnumerator Activate()
        {
            foreach(DoorInteractable doorInteractable in doorInteractables)
            {
                doorInteractable.UpdateInteractsWith(commandable);
            }
            yield break;
        }
        protected override void SpecialInit()
        {


        }
    }
}