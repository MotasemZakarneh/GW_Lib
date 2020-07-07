using System.Collections;

namespace GW_Lib.Interaction_System
{
    public class R_CommandDoor : Reaction
    {
        public DoorSwitch doorToCommand;
        public DoorCommand doorCommand;

        public R_CommandDoor()
        {
            waitEndType = WaitEndType.TillDone;
        }
        protected override IEnumerator Activate()
        {
            yield return StartCoroutine(doorToCommand.CommandDoor(doorCommand));
            isDone = true;
        }
        protected override void SpecialInit()
        {

        }
    }
}