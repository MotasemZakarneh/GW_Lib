using System;
using System.Collections;
using System.Linq;
using DarkTonic.MasterAudio;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public enum DoorCommand
    {
        Open, Close, Switch
    }
    public enum DoorState
    {
        Opened, Closed
    }

    [RequireComponent(typeof(InstanceConditionsSource))]
    public class DoorSwitch : MonoBehaviour
    {
        public Transform DoorBody=>doorBody;

        [SerializeField] Interactable switchInteractable = null;
        [SerializeField] Interactable openInteractable = null;
        [SerializeField] Interactable closeInteractable = null;

        [SerializeField] DoorState doorState = DoorState.Closed;
        [SerializeField] string openDoorEvent = "Open Door";
        [SerializeField] string closeDoorEvent = "Close Door";

        [Header("Door Specific")]
        [SerializeField] Transform doorBody = null;

        private DoorState requestedState = DoorState.Closed;
        int doorStatesSize = 0;

        public DoorState CurrDoorState => doorState;

        private void Awake()
        {
            doorStatesSize = Enum.GetValues(typeof(DoorState)).Cast<int>().Max()+1;
        }

        public bool IsGoodCommand(DoorCommand command)
        {
            switch (command)
            {
                case DoorCommand.Close:
                    if (doorState != DoorState.Closed)
                    {
                        return true;
                    }
                    break;
                case DoorCommand.Open:
                    if (doorState != DoorState.Opened)
                    {
                        return true;
                    }
                    break;
                case DoorCommand.Switch:
                    return true;
            }
            return false;
        }

        public IEnumerator CommandDoor(DoorCommand command)
        {
            switch (command)
            {
                case DoorCommand.Open:
                    if (doorState!= DoorState.Opened)
                    {
                        requestedState = DoorState.Opened;
                        yield return StartCoroutine(openInteractable.Interact(openInteractable));
                    }
                    break;
                case DoorCommand.Close:
                    if (doorState != DoorState.Closed)
                    {
                        requestedState = DoorState.Closed;
                        yield return StartCoroutine(closeInteractable.Interact(closeInteractable));
                    }
                    break;
                case DoorCommand.Switch:
                    requestedState = (DoorState)(((int)doorState+1)%doorStatesSize);
                    yield return StartCoroutine(switchInteractable.Interact(switchInteractable));
                    break;
            }
            PlaySoundEvent(doorState);
            yield return new WaitUntil(HasReachedTargetedState);
        }

        private void PlaySoundEvent(DoorState doorState)
        {
            string eventToFire="";
            switch (doorState)
            {
                case DoorState.Opened:
                    eventToFire = openDoorEvent;
                    break;
                case DoorState.Closed:
                    eventToFire = closeDoorEvent;
                    break;
            }
            MasterAudio.FireCustomEvent(eventToFire, transform);
        }

        private bool HasReachedTargetedState()
        {
            return requestedState == doorState;
        }
        public void AssignState(DoorState state)
        {
            doorState = state;
        }
    }
}