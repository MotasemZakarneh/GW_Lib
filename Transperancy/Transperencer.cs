using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class Transperencer : MonoBehaviour
    {
        [SerializeField] TransperencerConfig coreConfig = null;
        [Header("Read Only")]
        [SerializeField] List<Transperable> runTimeSet = new List<Transperable>();

        private void OnTriggerEnter(Collider other)
        {
            Transperable transperable = other.GetComponent<Transperable>();
            if (transperable == null || runTimeSet.Contains(transperable))
            {
                return;
            }
            runTimeSet.Add(transperable);
            transperable.FadeOut(coreConfig);
        }

        private void OnTriggerExit(Collider other)
        {
            Transperable transperable = other.GetComponent<Transperable>();

            if (transperable==null || runTimeSet.Contains(transperable) == false)
            {
                return;
            }
            transperable.FadeIn(OnFinishedFadingOut);
        }
        private void OnFinishedFadingOut(Transperable transperable)
        {
            if (runTimeSet.Contains(transperable) == false)
            {
                Debug.LogWarning("Trying To Remove Transperable: " +
                                        transperable.name + " but it is not in the list");
                return;
            }
            runTimeSet.Remove(transperable);
        }
    }
}