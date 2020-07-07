using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_ParticleSystemControl : Reaction
    {
        [SerializeField] ParticleSystem particleSystemToControl = null;
        [SerializeField] bool appliesForChildren = true;
        [SerializeField] ParticleControlMode particleControlMode = ParticleControlMode.None;

        enum ParticleControlMode
        {
            None, Play, Stop
        }

        public R_ParticleSystemControl()
        {
            reactionDuration = 0.75f; 
            waitEndType = WaitEndType.Time;
            particleControlMode = ParticleControlMode.Play;
        }
        protected override IEnumerator Activate()
        {
            switch (particleControlMode)
            {
                case ParticleControlMode.None:
                    break;
                case ParticleControlMode.Play:
                    particleSystemToControl.Play(appliesForChildren);
                    break;
                case ParticleControlMode.Stop:
                    particleSystemToControl.Stop(appliesForChildren);
                    break;
                default:
                    Debug.LogWarning("Not Implemented ParticleControlMode");
                    break;
            }
            isDone = true;
            yield break;
        }
        protected override void SpecialInit()
        {
           
        }
    }
}