using UnityEngine;

namespace GW_Lib.Utility
{
    [CreateAssetMenu(fileName = "Transperencer Config", menuName = "GW_Lib/Others/Transperencer Config")]
    public class TransperencerConfig : ScriptableObject
    {
        public float fadeToInvisAmount = 0.3f;
        public float fadeToInvisTime = 0.5f;
        public float fadeToViewTime = 0.250f;
    }
}