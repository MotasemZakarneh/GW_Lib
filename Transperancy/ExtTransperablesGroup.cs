using UnityEngine;

namespace GW_Lib.Utility
{
    public class ExtTransperablesGroup : MonoBehaviour
    {
        ExtTransperable[] transperables
        {
            get
            {
                if(m_transperables.Length == 0)
                {
                    m_transperables = GetComponentsInChildren<ExtTransperable>(true);
                }
                return m_transperables;
            }
        }

        [SerializeField] ExtTransperable[] m_transperables = new ExtTransperable[0];
        void Reset()
        {
            m_transperables = GetComponentsInChildren<ExtTransperable>(true);
        }

        public void FadeToTarget(float target, float dur)
        {
            foreach (ExtTransperable transperable in transperables)
            {
                transperable.FadeToTarget(target, dur);
            }
        }
        public void ResetToOriginalMats()
        {
            foreach (ExtTransperable transperable in transperables)
            {
                transperable.ResetToOriginalMats();
            }
        }
        public void FadeOut(float dur)
        {
            foreach (ExtTransperable transperable in transperables)
            {
                transperable.FadeOut(dur);
            }
        }
        public void FadeIn(float dur)
        {
            FadeToTarget(1, dur);
        }
        public void FadeOut()
        {
            foreach (ExtTransperable transperable in transperables)
            {
                transperable.FadeOut();
            }
        }
        public void FadeIn()
        {
            foreach (ExtTransperable transperable in transperables)
            {
                transperable.FadeIn();
            }
        }
    }
}