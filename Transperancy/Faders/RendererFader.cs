using System;
using System.Collections;
using UnityEngine;

namespace GW_Lib.Utility
{
    [System.Serializable]
    public class RendererFader
    {
        Renderer renderer;
        MonoBehaviour owner;
        Material[] originalMaterials;
        AbsMatFader[] matFaders;

        Action onFinishedFading;
        bool resetOnEnd;
        int fadesCounter;

        public RendererFader(Renderer renderer, MonoBehaviour owner)
        {
            this.renderer = renderer;
            this.owner = owner;

            originalMaterials = renderer.sharedMaterials;
            matFaders = new AbsMatFader[originalMaterials.Length];

            for (int i = 0; i < matFaders.Length; i++)
            {
                Material m = renderer.materials[i];
                matFaders[i] = new MatFader();

                if (IsToon(m))
                {
                    matFaders[i] = new MatToonFader();
                }
            }
        }
        
        public void ResetToOriginalMats()
        {
            renderer.materials = originalMaterials;
        }
        public void Fade(Action onFinishedFading, float target, float fadetime, bool resetOnEnd)
        {
            this.resetOnEnd = resetOnEnd;
            this.onFinishedFading = onFinishedFading;
            fadesCounter = 0;
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material m = renderer.materials[i];
                matFaders[i].Fade(owner, OnFinishedFading, target, fadetime, m);
            }
        }

        private bool IsToon(Material m)
        {
            if (m == null)
            {
                return false;
            }
            if (m.shader == Shader.Find("Standard"))
            {
                return false;
            }

            return MatToonFader.IsValidName(m.shader.name);
        }
        private void OnFinishedFading()
        {
            fadesCounter++;
            if (fadesCounter < originalMaterials.Length)
            {
                return;
            }

            onFinishedFading?.Invoke();
            if (resetOnEnd)
            {
                ResetToOriginalMats();
            }
        }
    }
}