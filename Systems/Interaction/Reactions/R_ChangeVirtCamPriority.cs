using System.Collections;
using UnityEngine;
using Cinemachine;
using Invector.vCamera;

namespace GW_Lib.Interaction_System
{
    public class R_ChangeVirtCamPriority : Reaction
    {
        [SerializeField] CinemachineVirtualCamera camToModify = null;
        [SerializeField] CinemachineVirtualCamera[] camsToReset = new CinemachineVirtualCamera[0];
        [SerializeField] bool resetAllExceptForMain = false;

        [SerializeField] int newPriority = 0;
        [Tooltip("-1 means we will not change the current blend speed")]
        [SerializeField] float newBlend = -1;
        [SerializeField] bool resetBlend = true;

        protected override IEnumerator Activate()
        {
            yield return 0;
            foreach (CinemachineVirtualCamera c in camsToReset)
            {
                c.Priority = 10;
            }

            if (resetAllExceptForMain)
            {
                CinemachineVirtualCamera[] cams = FindObjectsOfType<CinemachineVirtualCamera>();
                foreach (var c in cams)
                {
                    if (c.CompareTag("MainCamera"))
                        continue;
                    c.Priority = 10;
                }
            }

            if (camToModify != null)
            {
                camToModify.Priority = newPriority;
            }

            vThirdPersonCamera cam = FindObjectOfType<vThirdPersonCamera>();
            cam.ChangePoint(camToModify.name);

            if (newBlend <= 0)
            {
                isDone = true;
                yield break;
            }

            CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
            float prevBlend = brain.m_DefaultBlend.m_Time;
            brain.m_DefaultBlend.m_Time = newBlend;
            yield return new WaitForSeconds(newBlend);;
            brain.m_DefaultBlend.m_Time = prevBlend;
            isDone = true;


        }
        protected override void SpecialInit() { }
    }
}