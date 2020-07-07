using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public enum DoorInteractWith{Player,Ais, Both}
    public class DoorInteractable : MonoBehaviour
    {
        [SerializeField] DoorInteractWith interactsWith = DoorInteractWith.Both;
        [SerializeField] DoorSwitch door = null;

        [Header("Interaction Slots")]
        [SerializeField] GameObject[] aiInteractionPoints = new GameObject[0];
        [Tooltip("The playerInteractable variable, will be sent to this intermediate interactable and played there" +
                                    "\n make sure it plays Door Command Interactable")]
        [SerializeField] IntermediateInteractable playerClickInteractableSlot = null;

        [Header("Intermediate Interactables")]
        [Tooltip("Interactable that will be played when Player Clicks (ensure it calls Door Command Interactable)")]
        [SerializeField] Interactable playerInteractable = null;
        [Tooltip("Interactable that will be played when ais trigger (ensure it calls Door Command Interactable)")]
        [SerializeField] Interactable aisInteractable = null;

        [Header("Door Specifics")]
        [SerializeField] DoorCommand doorCommand = DoorCommand.Switch;

        private void Awake()
        {
            SendDoorSwitch();

            SetUpPlayerInteractionSite();
            aisInteractable.SubscribeToUsageChecks(DoorUsageCheck);

            SetUpPlayerInteractable();

            UpdateInteractsWith(interactsWith);
        }

        private void SendDoorSwitch()
        {
            R_CommandDoor[] doorCommandReactions = GetComponentsInChildren<R_CommandDoor>();
            foreach (R_CommandDoor commandReaction in doorCommandReactions)
            {
                commandReaction.doorToCommand = door;
                commandReaction.doorCommand = doorCommand;
            }
        }
        private void SetUpPlayerInteractionSite()
        {
            InteractionSite site = GetComponentInChildren<InteractionSite>();
            site.physicalDoorTransform = door.DoorBody;
        }
        private void SetUpPlayerInteractable()
        {
            playerClickInteractableSlot.SetInteractable(playerInteractable);
            playerInteractable.SubscribeToUsageChecks(DoorUsageCheck);
        }

        public void UpdateInteractsWith(DoorInteractWith interactsWith)
        {
            this.interactsWith = interactsWith;
            switch (interactsWith)
            {
                case DoorInteractWith.Player:
                    SetPlayerInteractionState(true);
                    SetAiInteractionState(false);
                    break;
                case DoorInteractWith.Ais:
                    SetPlayerInteractionState(false);
                    SetAiInteractionState(true);
                    break;
                case DoorInteractWith.Both:
                    SetPlayerInteractionState(true);
                    SetAiInteractionState(true);
                    break;
            }
        }

        private void SetPlayerInteractionState(bool playerInteractionState)
        {
            playerClickInteractableSlot.gameObject.SetActive(playerInteractionState);
        }
        private void SetAiInteractionState(bool aiInteractionState)
        {
            foreach (GameObject point in aiInteractionPoints)
            {
                point.SetActive(aiInteractionState);
            }
        }
        private bool DoorUsageCheck()
        {
            return door.IsGoodCommand(doorCommand);
        }
    }
}