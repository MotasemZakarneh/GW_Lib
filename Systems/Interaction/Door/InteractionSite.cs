using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class InteractionSite : MonoBehaviour
    {
        [SerializeField] float timeBetweenUpdates = 0.1f;
        [SerializeField] Transform[] interactionPoints = new Transform[0];

        [SerializeField] Transform realInteractionSite = null;
        public Transform physicalDoorTransform = null;
        [Header("Read Only")]
        [SerializeField] Transform chosenInteractionPoint = null;

        float counter = 0;
        GameObject player = null;
        private void Start()
        {
            player = Constants.TagFindPlayer();
            Extentions.SetLayerTo(this,Constants.IgnoreRayCastLayer,true);
        }
        private void UpdateInteractionSitePosition()
        {
            if (player == null)
            {
                return;
            }
            Vector3 playerToDoor = (physicalDoorTransform.position - player.transform.position).normalized;

            int activePoint = 0;
            float maxInDir = float.MinValue;

            for (int i = 0; i < interactionPoints.Length; i++)
            {
                Transform interactionPoint = interactionPoints[i];
                Vector3 pointToDoor = (physicalDoorTransform.position-interactionPoint.position).normalized;
                float testInDir = Vector3.Dot(playerToDoor, pointToDoor);

                if (testInDir >= maxInDir)
                {
                    maxInDir = testInDir;
                    activePoint = i;
                }
            }

            chosenInteractionPoint = interactionPoints[activePoint];
            realInteractionSite.position = chosenInteractionPoint.position;
            realInteractionSite.rotation = chosenInteractionPoint.rotation;
        }
        private void Update()
        {
            if (player == null)
            {
                return;
            }
            counter = counter + Time.deltaTime / timeBetweenUpdates;
            if (counter < 1)
            {
                return;
            }
            counter = 0;
            UpdateInteractionSitePosition();
        }
    }

}