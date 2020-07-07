using System;
using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public abstract class Interactivity : MonoBehaviour
    {
        [SerializeField] protected Transform interactionSite;
        Action lastOnInterrupted;

        public Transform InteractionSite { get { return interactionSite; } }
        public abstract IEnumerator Interact(MonoBehaviour caller);
        public IEnumerator Interact(MonoBehaviour caller, Action callBack)
        {
            yield return StartCoroutine(Interact(caller));
            callBack?.Invoke();
        }
        public IEnumerator Interact(MonoBehaviour caller, Action callBack, Action onInterrupted)
        {
            yield return StartCoroutine(Interact(caller,callBack));
            lastOnInterrupted = null;
        }
        protected virtual void Reset()
        {
            if (GetComponent<BoxCollider>() == null)
            {
                gameObject.AddComponent<BoxCollider>();
            }
            GetComponent<BoxCollider>().isTrigger = true;
            //Create And Set Interaction Site
            if (interactionSite == null)
            {
                GameObject interactionSite = new GameObject("Interaction Site");
                interactionSite.transform.SetParent(transform);
                this.interactionSite = interactionSite.transform;
                interactionSite.transform.localPosition = Vector3.zero;
                InteractionSite.transform.localRotation = Quaternion.identity;
            }
            I_Reset();
        }
        protected virtual void OnDisable()
        {
            lastOnInterrupted?.Invoke();
        }
        protected virtual void I_Reset() { }
    }
}