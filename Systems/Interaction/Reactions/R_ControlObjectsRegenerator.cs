using System.Collections;
using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_ControlObjectsRegenerator : Reaction
    {
        enum Command{Run,TimedRun,Stop}

        [SerializeField] ObjectsRegenerator regenerator = null;

        [SerializeField] Command command = Command.Run;
        [Header("Timed Run Settings")]
        [SerializeField] float timeBetweenRegens = 2;

        protected override IEnumerator Activate()
        {
            if(command == Command.Run)
            {
                regenerator.Run();
            }
            else if(command == Command.TimedRun)
            {
                regenerator.Run(timeBetweenRegens);
            }
            else if(command == Command.Stop)
            {
                regenerator.Stop();
            }
            yield break;
        }

        protected override void SpecialInit()
        {

        }
    }
}