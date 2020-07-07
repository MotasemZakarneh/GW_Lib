using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class ExtTransperable : MonoBehaviour
    {
        [SerializeField] List<Renderer> rends = null;
        [SerializeField] TransperencerConfig config = null;

        RendererFader[] faders
        {
            get
            {
                if (rends.Count == 0)
                    SetRenderers();
                for (int i = 0; i < rends.Count; i++)
                {
                    if (rends[i] == null)
                    {
                        SetRenderers();
                        break;
                    }
                }

                bool isEmpty = m_faders == null || m_faders.Length == 0;
                if (!isEmpty)
                {
                    return m_faders;
                }

                m_faders = new RendererFader[rends.Count];
                for (int i = 0; i < rends.Count; i++)
                {
                    Renderer r = rends[i];
                    faders[i] = new RendererFader(r, this);
                }
                return m_faders;
            }
        }
        RendererFader[] m_faders = new RendererFader[0];

        void Reset()
        {
            SetRenderers();
        }

        private void SetRenderers()
        {
            rends = GetComponentsInChildren<Renderer>().ToList();
            rends.RemoveAll((r) =>
            {
                return r.GetComponent<ParticleSystemRenderer>() || r.name.Contains("single mesh");
            });
        }

        public void FadeToTarget(float target, float dur)
        {
            foreach (RendererFader fader in faders)
            {
                fader.Fade(null, target, dur, false);
            }
        }
        public void ResetToOriginalMats()
        {
            foreach (RendererFader fader in faders)
            {
                fader.ResetToOriginalMats();
            }
        }

        public void FadeOut(float dur)
        {
            FadeToTarget(config.fadeToInvisAmount, dur);
        }
        public void FadeIn(float dur)
        {
            FadeToTarget(1, dur);
        }
        public void FadeOut()
        {
            FadeOut(config.fadeToInvisTime);
        }
        public void FadeIn()
        {
            FadeIn(config.fadeToViewTime);
        }
    }
}