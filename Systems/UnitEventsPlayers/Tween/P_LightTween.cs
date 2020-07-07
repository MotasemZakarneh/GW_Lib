using UnityEngine;
using DG.Tweening;
using System.Collections;
using VLB;

namespace GW_Lib.Utility
{
    public class P_LightTween : UnitPlayable
    {
        public float Dur => dur;
        enum TweenType { Intensity, Range, Color }
        enum SettingType { SimpleLight, VoluLight }

        [SerializeField] Light l = null;
        [SerializeField] bool toOn = true;
        [SerializeField] TweenType tweenType = TweenType.Intensity;
        [SerializeField] SettingType settingType = SettingType.VoluLight;

        [SerializeField] float target = 3;
        [SerializeField] float offTarget = 0;
        [SerializeField] float dur = 1;
        [SerializeField] Ease ease = Ease.Linear;
        [SerializeField] bool wait = false;
        [Header("Color Tween Settings")]
        [SerializeField] Color onColor = Color.yellow;
        [SerializeField] Color offColor = Color.black;


        bool isTweening = false;

        public override IEnumerator Behavior()
        {

            if (l == null)
            {
                l = GetComponent<Light>();
            }
            if (l == null)
            {
                Debug.Log("We must have a light attached to the field of this script");
                yield break;
            }

            if (settingType == SettingType.VoluLight)
            {
                VolumetricLightBeam vlb = l.GetComponent<VolumetricLightBeam>();
                if (vlb)
                {
                    if (!vlb.trackChangesDuringPlaytime)
                    {
                        vlb.trackChangesDuringPlaytime = true;
                    }
                }
            }

            if (toOn)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
            if (wait)
            {
                yield return new WaitForSeconds(dur);
            }
        }
        public void TurnOn()
        {
            RunTween(target, onColor);
        }
        public void TurnOff()
        {
            RunTween(offTarget, offColor);
        }
        private void RunTween(float target, Color targetC)
        {
            isTweening = true;
            if (tweenType == TweenType.Color)
            {
                DOTween.To(GetC, SetC, targetC, dur).OnComplete(OnComplete).SetEase(ease);
            }
            else
            {
                DOTween.To(Get, Set, target, dur).OnComplete(OnComplete).SetEase(ease);
            }
        }

        private void OnComplete()
        {
            isTweening = false;
        }
        private float Get()
        {
            if (!l)
            {
                return 0;
            }
            if (tweenType == TweenType.Intensity)
            {
                return l.intensity;
            }
            return l.range;
        }
        private void Set(float pNewValue)
        {
            if (!l)
            {
                return;
            }
            if (tweenType == TweenType.Intensity)
            {
                l.intensity = (float)pNewValue;
                return;
            }
            l.range = (float)pNewValue;
        }
        private void SetC(Color newVal)
        {
            l.color = newVal;
        }
        private Color GetC()
        {
            return l.color;
        }
    }
}