using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class DoorAnimState : StateMachineBehaviour
    {
        [SerializeField] bool updateState = false;
        [SerializeField] DoorState doorState = DoorState.Closed;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateState)
            {
                DoorSwitch doorSwitch = animator.GetComponent<DoorSwitch>();
                doorSwitch.AssignState(doorState);
            }
        }
    }
}