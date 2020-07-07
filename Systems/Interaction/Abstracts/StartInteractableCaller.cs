using System.Collections;
using Doozy.Engine.UI;
using Honor.Saving;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof(Interactable))]
    public class StartInteractableCaller : MonoBehaviour
    {
        public UIView cutSceneFader => GetComponentInChildren<UIView>(true);
        public bool RunOnStart
        {
            get
            {
                return runOnStart;
            }
            set
            {
                runOnStart = value;
            }
        }
        [SerializeField] bool runOnStart = true;
        [SerializeField] bool saveAfterInteractable = true;
        [SerializeField] float delay = 0.1f;
        [SerializeField] bool autoFadeOut = true;

        Interactable i = null;

        void Awake()
        {
            i = GetComponent<Interactable>();
            if(cutSceneFader)
                cutSceneFader.gameObject.SetActive(true);
        }

        IEnumerator Start()
        {
            yield return 0;
            yield return 0;
            if(!runOnStart)
            {
                yield break;
            }

            if (autoFadeOut)
            {
                Hide();
            }

            yield return StartCoroutine(i.Interact(this));
            if(!saveAfterInteractable)
            {
                yield break;
            }
            yield return new WaitForSeconds(delay);

            RunOnStart = false;
            PlayerSaver.instance.SaveData();
        }
        public void Show()
        {
            cutSceneFader.Show();
        }
        public void Hide()
        {
            cutSceneFader?.Hide(0);
        }
    }
}