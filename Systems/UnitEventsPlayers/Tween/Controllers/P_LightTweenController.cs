using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_LightTweenController : MonoBehaviour
    {
        public float GetDur
        {
            get
            {
                List<float> lengths = new List<float>();

                foreach (P_LightTween tweener in tweeners)
                {
                    if (tweener == null)
                    {
                        continue;
                    }
                    float d = tweener.Dur;
                    lengths.Add(d);
                }

                if(lengths.Count == 0)
                {
                    return 0;
                }

                return lengths.Max();
            }
        }
        [SerializeField] P_LightTween[] tweeners = null;

        void Reset()
        {
            tweeners = GetComponentsInChildren<P_LightTween>();
        }
        public void TurnOn()
        {
            foreach (P_LightTween tweener in tweeners)
            {
                if(tweener == null)
                {
                    continue;
                }
                tweener.TurnOn();
            }
        }
        public void TurnOff()
        {
            foreach (P_LightTween tweener in tweeners)
            {
                if (tweener == null)
                {
                    continue;
                }
                tweener.TurnOff();
            }
        }
    }
}