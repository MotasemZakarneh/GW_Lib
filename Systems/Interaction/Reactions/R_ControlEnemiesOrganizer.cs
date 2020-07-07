using System.Collections;
using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_ControlEnemiesOrganizer : Reaction
    {
        enum ControlCommand{Activate,DeActivate,ActivateAndAutoAdvance}
        [SerializeField] ControlCommand controlCommand = ControlCommand.Activate;
        [SerializeField] EnemiesOrganizer enemiesOrganizer = null;
        protected override IEnumerator Activate()
        {
            if(controlCommand == ControlCommand.Activate)
            {
                enemiesOrganizer.ActivateOrganizer(false);
            }
            else if(controlCommand == ControlCommand.DeActivate)
            {
                enemiesOrganizer.DeActivateOrganizer();
            }
            else if(controlCommand == ControlCommand.ActivateAndAutoAdvance)
            {
                enemiesOrganizer.ActivateOrganizer(true);
            }
            yield break;
        }

        protected override void SpecialInit()
        {

        }
    }
}