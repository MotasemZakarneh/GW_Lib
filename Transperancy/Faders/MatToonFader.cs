using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Utility
{
    [Serializable]
    public class MatToonFader : AbsMatFader
    {
        static readonly List<string> allowedNames = new List<string>() { "Toon", "Wind" };
        static readonly List<string> opacity = new List<string>() { "_Opacity", "_Alpha" };
        float counter = 0;

        public static bool IsValidName(string n) => allowedNames.FindIndex(o => o.Contains(n)) != -1;

        protected override IEnumerator DoFade(Action onDoneFading, float target, float fadetime, Material m)
        {
            m.shader = Shader.Find("RealToon/Version 5/Lite/Fade Transparency");
            string opaictyVar = opacity.Find(o => m.HasProperty(o));
            float startVal = m.GetFloat(opaictyVar);
            bool isFading = true;
            while (isFading)
            {
                yield return 0;
                counter = counter + Time.deltaTime / fadetime;
                float currVal = Mathf.Lerp(startVal, target, counter);
                m.SetFloat(opaictyVar, currVal);
                isFading = counter < 1;
            }
            counter = 0;
            onDoneFading?.Invoke();
        }
    }
}