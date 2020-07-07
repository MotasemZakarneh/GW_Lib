using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_HighlightToon : Reaction
    {
        [SerializeField] Transform t = null;
        [SerializeField] bool applyToMainMatOnly = false;
        [SerializeField] bool scanChildren = false;

        [SerializeField] float width = 2;
        [ColorUsage(false,true)]
        [SerializeField] Color targetColor = Color.green;
        [SerializeField] float dur = 1;

        const string _Width = "_OutlineWidth";
        const string _Color = "_OutlineColor";

        protected override IEnumerator Activate()
        {
            if(applyToMainMatOnly)
            {
                if(scanChildren)
                {
                    MeshRenderer[] rends = t.GetComponentsInChildren<MeshRenderer>();
                    foreach (var mr in rends)
                    {
                        StartCoroutine(ProcessMat(mr.material));
                    }
                }
                else
                {
                    MeshRenderer mr = t.GetComponent<MeshRenderer>();
                    yield return StartCoroutine(ProcessMat(mr.material));
                }
            }
            else
            {
                if (scanChildren)
                {
                    MeshRenderer[] rends = t.GetComponentsInChildren<MeshRenderer>();
                    foreach (var mr in rends)
                    {
                        StartCoroutine(ProcessMats(mr.materials));
                    }
                }
                else
                {
                    MeshRenderer mr = t.GetComponent<MeshRenderer>();
                    yield return StartCoroutine(ProcessMats(mr.materials));
                }
            }
            yield return new WaitForSeconds(dur + 0.01f);

            isDone = true;
        }
        private IEnumerator ProcessMats(Material[] materials)
        {
            foreach (var m in materials)
            {
                if (!m.shader.name.Contains("Toon"))
                    continue;

                StartCoroutine(ProcessMat(m));
            }
            yield return 0;
        }
        private IEnumerator ProcessMat(Material material)
        {
            if (!material.shader.name.Contains("Toon"))
                yield break;

            float lerper = 0;
            Color startColor = material.GetColor(_Color);
            float startWidth = material.GetFloat(_Width);

            while (lerper<1)
            {
                yield return 0;
                lerper = lerper + Time.deltaTime / dur;
                Color smoothColor = Color.Lerp(startColor, targetColor, lerper);
                float smoothWidth = Mathf.Lerp(startWidth, width, lerper);

                material.SetColor(_Color, smoothColor);
                material.SetFloat(_Width, smoothWidth);
            }
        }

        protected override void SpecialInit()
        {

        }
    }
}