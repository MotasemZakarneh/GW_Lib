using Invector.vCharacterController.vActions;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof(vTriggerGenericAction))]
    public class vInteractable : Interactable
    {
        [Header("Auto Disable Data")]
        [SerializeField] bool autoDisable = true;
        [Header("Auto Enable Data")]
        [SerializeField] float minReEnableTime = 0.1f;
        [SerializeField] float minReEnableSqrDist = 2.25f;
        [SerializeField] bool autoEnable = true;

        float reEnableCounter = 0;

        vTriggerGenericAction action
        {
            get
            {
                if(m_action == null)
                {
                    m_action = GetComponent<vTriggerGenericAction>();
                }
                return m_action;
            }
        }
        vTriggerGenericAction m_action;

        Coroutine interaction = null;
        bool tryToReEnable = false;
        GameObject player;

        public bool IsActive
        {
            set
            {
                action.enabled = value;
            }
            get
            {
                return action.enabled;
            }
        }

        void Start()
        {
            action.OnPlayerEnter.AddListener(OnPlayerEntered);
        }
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") == false)
            {
                return;
            }
            if (autoEnable)
            {
                tryToReEnable = true;
            }
            player = other.gameObject;
        }
        void Update()
        {
            if (interaction != null)
            {
                return;
            }
            if (autoEnable == false)
            {
                return;
            }
            if (tryToReEnable == false)
            {
                return;
            }
            if (action.enabled)
            {
                return;
            }

            reEnableCounter = reEnableCounter + Time.deltaTime / minReEnableTime;
            if (reEnableCounter < 1)
            {
                return;
            }
            float sqrDist = (player.transform.position - transform.position).sqrMagnitude;
            if (sqrDist < minReEnableSqrDist)
            {
                reEnableCounter = 0;
                return;
            }

            reEnableCounter = 0;
            IsActive = true;
        }

        private void OnPlayerEntered(GameObject g)
        {
            if (interaction != null)
            {
                return;
            }
            if (autoDisable)
            {
                IsActive = false;
            }
            tryToReEnable = false;
            interaction = StartCoroutine(Interact(this, OnStoppedInteraction, OnStoppedInteraction));
        }
        private void OnStoppedInteraction()
        {
            interaction = null;
        }
    }
}