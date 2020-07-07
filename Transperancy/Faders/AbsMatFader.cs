using System;
using System.Collections;
using UnityEngine;

namespace GW_Lib.Utility
{
    public abstract class AbsMatFader
    {
        MonoBehaviour lastOwner;
        Coroutine activeFade;
        public void Fade(MonoBehaviour owner, Action onDoneFading, float target, float fadeTime, Material m)
        {
            this.lastOwner = owner;
            if (activeFade != null)
            {
                lastOwner.StopCoroutine(activeFade);
                activeFade = null;
            }
            activeFade = owner.StartCoroutine(DoFade(onDoneFading, target, fadeTime, m));
        }
        protected abstract IEnumerator DoFade(Action onDoneFading, float target, float fadeTime, Material m);
    }
}