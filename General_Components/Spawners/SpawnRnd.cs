using System;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class SpawnRND : MonoBehaviour
    {
        [SerializeField] Transform spawnPos = null;
        [SerializeField] bool spawnOnDisable = true;
        [SerializeField] bool spawnOnEnable = false;
        [SerializeField] PairSelectionMode pairSelectionMode = PairSelectionMode.All;

        [SerializeField] ObjToChancePair[] spawnablePairs = new ObjToChancePair[0];

        public enum PairSelectionMode { All, RND }
        [Serializable]
        public class ObjToChancePair
        {
            public float spawnChance = 0.1f;
            public GameObject obj = null;
        }

        protected virtual void Reset()
        {
            spawnPos = transform;
        }
        protected virtual void OnEnable()
        {
            if (spawnOnDisable)
                RunSpawner();
        }
        protected virtual void OnDisable()
        {
            if (spawnOnEnable)
                RunSpawner();
        }

        public void RunSpawner()
        {
            switch (pairSelectionMode)
            {
                case PairSelectionMode.All:
                    foreach (var pair in spawnablePairs)
                    {
                        ProcessPair(pair);
                    }
                    break;
                case PairSelectionMode.RND:
                    if (spawnablePairs.Length == 0)
                        return;
                    if (spawnablePairs.Length == 1)
                    {
                        ProcessPair(spawnablePairs[0]);
                        return;
                    }

                    int i = UnityEngine.Random.Range(0, spawnablePairs.Length);
                    ProcessPair(spawnablePairs[i]);
                    break;
            }
        }
        private void ProcessPair(ObjToChancePair pair)
        {
            float rnd = UnityEngine.Random.value;
            if (rnd > pair.spawnChance)
                return;

            GameObject g = PoolsManager.instance.GetPoolableG(pair.obj,spawnPos.position);
            if(g==null)
            {
                g = Instantiate(pair.obj, spawnPos.position, spawnPos.rotation);
            }
        }
    }
}