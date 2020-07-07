using System.Collections;
using System.Collections.Generic;
using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_SpawnGameObj : Reaction
    {
        [Header("Core")]
        [SerializeField] GameObject prefab = null;
        [SerializeField] Transform spawnPoint = null;
        [SerializeField] bool spawnPointIsParent = false;

        [Header("Effect")]
        [SerializeField] GameObject effectPrefab = null;
        [SerializeField] float timeBeforeSpawn = 0.5f;

        Transform SpawnPoint => spawnPoint == null ? transform : spawnPoint;
        protected override IEnumerator Activate()
        {
            if (effectPrefab)
            {
                PoolsManager.instance.SafeMakeObject(effectPrefab, SpawnPoint, 0,spawnPointIsParent);
            }
            yield return PoolsManager.instance.SafeMakeObject(prefab, SpawnPoint, timeBeforeSpawn, spawnPointIsParent);
            isDone = true;
        }
        protected override void SpecialInit()
        {

        }
    }
}