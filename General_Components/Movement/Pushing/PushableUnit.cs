using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using GW_Lib.Characters;
using Invector;
using System;

namespace GW_Lib.Utility
{
    public class PushableUnit : Pushable
    {
        public int MaxPushesCount => maxPushesCount;

        [SerializeField] float stoppedDist = 0.05f;
        [SerializeField] float stoppedDur = 0.5f;

        [SerializeField] float returningDur = 0.15f;
        [SerializeField] int maxPushesCount = 3;
        [SerializeField] bool isActive = true;

        [Header("Read Only")]
        [SerializeField] State state = State.Clear;

        enum State { Clear, Pushed, Returning, Dead }
        NavMeshAgent agent;
        bool reachedMaxPushes => pushesCount >= maxPushesCount;
        float stoppedCounter;
        Vector3 lastPos;
        Quaternion lastRot;
        int pushesCount;
        vHealthController health => GetComponent<vHealthController>();
        PlayMakerFSM[] fsms;
        Action<Transform> lastOnRecovered;
        float lastDamage;

        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
            fsms = GetComponents<PlayMakerFSM>();
            lastPos = rb3d.position;
        }

        public bool TryPush(PushableData pushableData, Vector3 atPos, Action<Transform> onRecovered,float damage)
        {
            if (!isActive || reachedMaxPushes || (health && health.isDead))
            {
                return false;
            }

            lastOnRecovered = onRecovered;
            lastDamage = damage;
            TransitionToState(State.Pushed);
            PushExplosive(pushableData, atPos);
            return true;
        }

        void FixedUpdate()
        {
            if (state != State.Pushed)
            {
                return;
            }

            bool isStopped = (rb3d.position - lastPos).magnitude < stoppedDist;
            lastPos = rb3d.position;

            if (!isStopped)
            {
                stoppedCounter = 0;
                return;
            }

            stoppedCounter = stoppedCounter + Time.deltaTime / stoppedDur;
            if (stoppedCounter > 1)
            {
                stoppedCounter = 0;
                TransitionToState(State.Returning);
            }
        }
        private void TransitionToState(State s)
        {
            if (s == state)
            {
                return;
            }
            state = s;
            //on new state enter
            switch (state)
            {
                case State.Clear:
                    UnfreezeUnit();
                    break;
                case State.Pushed:
                    FreezeUnit();
                    break;
                case State.Returning:
                    if (!health.isDead && !health.DoesDamageKill(lastDamage))
                    {
                        transform.DORotateQuaternion(lastRot, returningDur).OnComplete(
                            () => TransitionToState(State.Clear));
                    }
                    else
                    {
                        TransitionToState(State.Clear);
                    }
                    break;
                case State.Dead:
                    FinishPush();
                    break;
                default:
                    break;
            }
        }

        private void FreezeUnit()
        {
            foreach (PlayMakerFSM fsm in fsms)
            {
                fsm.enabled = false;
            }
            GetComponent<ActionsScheduler>().StartAction(null);
            lastRot = transform.rotation;
            pushesCount = pushesCount + 1;
            agent.enabled = false;
            rb3d.isKinematic = false;
        }
        private void UnfreezeUnit()
        {
            foreach (PlayMakerFSM fsm in fsms)
            {
                fsm.enabled = true;
            }
            pushesCount = 0;
            agent.enabled = true;
            rb3d.isKinematic = true;
            FinishPush();
        }
        private void FinishPush()
        {
            lastOnRecovered?.Invoke(transform);
            lastOnRecovered = null;
        }
    }
}