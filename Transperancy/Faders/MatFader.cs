using System;
using System.Collections;
using UnityEngine;

namespace GW_Lib.Utility
{
    [System.Serializable]
    public class MatFader:AbsMatFader
    {
        protected override IEnumerator DoFade(Action onDoneFading, float target, float fadeTime,Material m)
        {
            Shader fadeShader = Shader.Find("Standard");
            m.shader = fadeShader;

            m.SetFloat("_Mode", 2);
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;

            float counter = 0;
            Color c = m.color;
            float from = c.a;
            float to = target;
            float current = from;

            while (counter < 1)
            {
                yield return 0;
                counter += Time.deltaTime / fadeTime;
                current = Mathf.Lerp(from, to, counter);
                c.a = current;
                m.color = c;
            }
            counter = 0;
            onDoneFading?.Invoke();
        }
    }
}