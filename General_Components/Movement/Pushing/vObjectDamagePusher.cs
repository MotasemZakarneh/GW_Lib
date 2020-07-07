using System;
using System.Collections.Generic;
using Invector;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class vObjectDamagePusher : vObjectDamage
    {
        [SerializeField] PushableData pushData = null;
        List<Transform> storedTargets = new List<Transform>();
        Dictionary<Transform,Vector3> storedHitPoints = new Dictionary<Transform,Vector3>();
        //[SerializeField] PushableUnitData[] storedPushables = new PushableUnitData[0];
        //[Serializable] 
        //class PushableUnitData
        //{
        //    public Transform t = null;
        //    public Vector3 storedHitPint = new Vector3();
        //    public int maxCount = 0;
        //    public int currCount = 0;

        //    public PushableUnitData(){ }
        //    public PushableUnitData(Transform t, Vector3 storedHitPint, int maxCount)
        //    {
        //        this.t = t;
        //        this.storedHitPint = storedHitPint;
        //        this.maxCount = maxCount;
        //    }
        //    public void OnPushed()
        //    {
        //        currCount = currCount + 1;
        //    }
        //    public bool IsMaxed() => currCount >= maxCount;
        //}

        protected override void ApplyDamage(Transform target, Vector3 hitPoint)
        {
            if(storedTargets.Contains(target))
            {
                return;
            }

            PushableUnit pUnit = target.GetComponent<PushableUnit>();
            vHealthController healthController = target.GetComponent<vHealthController>();
            if (healthController == null)
            {
                vDamageRecieverSimple receiver = target.GetComponent<vDamageRecieverSimple>();
                if(receiver==null)
                {
                    return;
                }
                healthController = receiver.targetReciever.GetComponent<vHealthController>();
            }
            if(healthController == null)
            {
                return;
            }
            if (pUnit == null)
            {
                pUnit = healthController.GetComponent<PushableUnit>();
            }
            if (pUnit == null)
            {
                base.ApplyDamage(target, hitPoint);
                return;
            }

            OnContactHappened(healthController.transform);
            bool didPush = pUnit.TryPush(pushData, transform.position, OnRecovered,damage.damageValue);

            if(!didPush)
            {
                return;
            }
            //storedPushables = new PushableUnitData(healthController.transform,hitPoint,pUnit.)
            storedTargets.Add(healthController.transform);
            storedHitPoints[healthController.transform] = hitPoint;
        }
        private void OnRecovered(Transform recoveredTarget)
        {
            Vector3 hitPoint = storedHitPoints[recoveredTarget];
            DealDamage(recoveredTarget,hitPoint);

            storedTargets.Remove(recoveredTarget);
            storedHitPoints.Remove(recoveredTarget);
        }
    }
}