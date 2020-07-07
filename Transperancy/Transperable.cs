using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class Transperable : MonoBehaviour
    {
        [SerializeField] TransperencerConfig uniqueConfig = null;
        [SerializeField] Transform renderersHead = null;

        private List<RendererFader> faders = new List<RendererFader>();
        private MeshRenderer[] renderers = new MeshRenderer[0];
        private TransperencerConfig externalConfig = null;
        private int fadersCounter = 0;
        private Action<Transperable> fadeInCallBack = null;

        private void Reset()
        {
            renderersHead = transform;
            Collider col = GetComponent<Collider>();
            if (col == null)
            {
                col = gameObject.AddComponent<BoxCollider>();
            }
            col.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Transperables");
        }
        private void Start()
        {
            if (renderersHead == null)
            {
                renderersHead = transform;
            }
            renderers = renderersHead.GetComponentsInChildren<MeshRenderer>();
            faders = new List<RendererFader>();

            for (int i = 0; i < renderers.Length; i++)
            {
                MeshRenderer r = renderers[i];
                RendererFader fader = new RendererFader(r,this);
                faders.Add(fader);
            }
        }

        public void FadeOut(TransperencerConfig externalConfig)
        {
            fadersCounter = 0;
            this.externalConfig = externalConfig;
            TransperencerConfig config = GetTransperableConfig();

            for (int i = 0; i < faders.Count; i++)
            {
                faders[i].Fade(null, config.fadeToInvisAmount, config.fadeToInvisTime, false);
            }
        }
        public void FadeIn(Action<Transperable> callBack)
        {
            fadersCounter = 0;
            fadeInCallBack = callBack;
            TransperencerConfig config = GetTransperableConfig();
            faders.RemoveAll(f => {
                if (f == null)
                {
                    Debug.Log("Removing One Null Transperable?");
                    return true;
                }
                return false;
            });
            for (int i = 0; i < faders.Count; i++)
            {
                if (config == null)
                    continue;

                faders[i].Fade(OnRendererFadeFinished, 1, config.fadeToViewTime, true);
            }
            externalConfig = null;
        }

        private void OnRendererFadeFinished()
        {
            fadersCounter++;
            if (fadersCounter >= faders.Count)
            {
                fadeInCallBack?.Invoke(this);
            }
        }
        private TransperencerConfig GetTransperableConfig()
        {
            if (uniqueConfig == null)
            {
                return externalConfig;
            }
            return uniqueConfig;
        }
    }
}