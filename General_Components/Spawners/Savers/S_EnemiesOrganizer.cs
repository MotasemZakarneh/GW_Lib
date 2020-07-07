using System.Collections.Generic;
using Honor.Saving;
using UnityEngine;

namespace GW_Lib.Utility
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemiesOrganizer))]
    public class S_EnemiesOrganizer : PlayerSavable
    {
        const string _organizerName="Enemies Organizer Name";
        const string _currWave = "Current Wave";
        const string _isActive = "Is Active";
        const string _autoAdvance = "Auto Advance";
        const string _isCurrWaveCleared = "Is Current Wave Cleared";

        EnemiesOrganizer organizer => GetComponent<EnemiesOrganizer>();

        public override object CaptureState()
        {
            Dictionary<string,object> state = GetNewDataStruct();
            state[_organizerName] = transform.name;
            state[_currWave] = organizer.CurrentWave;
            state[_isActive] = organizer.IsActive;
            state[_autoAdvance] = organizer.AutoAdvance;
            state[_isCurrWaveCleared] = organizer.IsCurrentWaveCompleted;
            return state;
        }
        public override void RecoverState(object data)
        {
            Dictionary<string,object> state = GetState(data);

            transform.name = (string)state[_organizerName];
            int currentWave = FileSaver.JToObject<int>(state[_currWave]);
            bool isActive = (bool)state[_isActive];
            bool autoAdvance = (bool)state[_autoAdvance];
            bool isCurrWaveCleared = (bool)state[_isCurrWaveCleared];

            organizer.RestoreData(currentWave,isActive,autoAdvance,isCurrWaveCleared);
        }
    }
}