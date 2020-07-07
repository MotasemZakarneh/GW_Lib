using Invector;
using Invector.vCharacterController;
using UnityEngine;

namespace GW_Lib.Utility
{
    [RequireComponent(typeof(vObjectDamage))]
    public class Pusher : MonoBehaviour
    {
        [SerializeField] PushableData pushData = null;
        
        void Awake()
        {
            GetComponent<vObjectDamage>().onHit.AddListener(PushUnit);
        }
        public void PushUnit(Collider unit)
        {
            PushableUnit pUnit = unit.GetComponent<PushableUnit>();
            if (pUnit == null)
            {
                vDamageRecieverSimple receiver = unit.GetComponent<vDamageRecieverSimple>();
                if (!receiver)
                {
                    return;
                }
                pUnit = receiver.targetReciever.GetComponent<PushableUnit>();
            }
            if (pUnit == null)
            {
                return;
            }
            float damage = GetComponent<vObjectDamage>().damage.damageValue;
            pUnit.TryPush(pushData, transform.position,null,damage);
        }
    }
}