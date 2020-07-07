using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GW_Lib.Utility
{
    public class JoyStick : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [System.Serializable]
        struct GraphicsToFadeData
        {
            public CanvasGroup graphic;
            public float minAlpha;
            public float maxAlpha;
            public float during;
        }

        public enum AxisOption { Both, OnlyHorizontal, OnlyVertical }

        [Header("Core")]
        [SerializeField, Tooltip("The child graphic that will be moved around.")]
        private RectTransform m_Handle = null;
        [SerializeField, Tooltip("The handling area that the handle is allowed to be moved in.")]
        private RectTransform m_HandlingArea = null;
        [SerializeField, Tooltip("How fast the joystick will go back to the center")]
        private float m_Spring = 25f;
        [SerializeField, Tooltip("How close to the center that the axis will be output as 0")]
        private float m_DeadZone = 0.1f;

        [Header("Target Graphic Data")]
        [SerializeField] GraphicsToFadeData[] activeGraphics = new GraphicsToFadeData[0];

        [Header("Axis Data")]
        [SerializeField] bool useFlatAxis = true;
        [Tooltip("Customize the output that is sent in OnValueChange")]
        public AnimationCurve outputCurve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
        [SerializeField] private AxisOption m_AxesToUse = AxisOption.Both;

        [Header("ReadOnly")]
        [SerializeField] private Vector2 m_Axis = new Vector2();

        private bool m_IsDragging = false;

        public Vector2 Axis
        {
            get
            {
                float mag = m_Axis.magnitude;
                if (m_Axis == Vector2.zero || mag < m_DeadZone)
                {
                    return Vector2.zero;
                }
                if (useFlatAxis)
                {
                    return m_Axis;
                }

                float curveMatcher = outputCurve.Evaluate(mag);
                return m_Axis * curveMatcher;
            }
            private set
            {
                Vector2 a = value;
                if (a.magnitude > 1)
                {
                    a = NormalizeAxis(value);
                }
                a = FitAxis(a);
                m_Axis = Vector2.ClampMagnitude(a, 1);
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            // Fix anchors
            if (m_HandlingArea != null)
            {
                m_HandlingArea.anchorMin = new Vector2(0.5f, 0.5f);
                m_HandlingArea.anchorMax = new Vector2(0.5f, 0.5f);
            }

            // Hide active
            FadeGraphicsToMin();

            UpdateHandle();
        }
#endif

        protected override void OnEnable()
        {
            base.OnEnable();

            if (m_HandlingArea == null)
                m_HandlingArea = transform as RectTransform;

            FadeGraphicsToMin();
        }
        protected void LateUpdate()
        {
            if (m_IsDragging == false && m_Axis != Vector2.zero)
            {
                Vector2 newAxis = m_Axis - (m_Axis * Time.unscaledDeltaTime * m_Spring);

                if (newAxis.sqrMagnitude <= 0.0001f)
                    newAxis = Vector2.zero;

                Axis = newAxis;
                UpdateHandle();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsActive() || m_HandlingArea == null)
                return;

            Axis = m_HandlingArea.InverseTransformPoint(eventData.position);
            UpdateHandle();
            m_IsDragging = true;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (m_HandlingArea == null)
                return;

            Vector2 pressPos = eventData.position;
            Camera cam = eventData.pressEventCamera;
            Vector2 localPressPos = m_HandlingArea.InverseTransformPoint(pressPos);
            RectTransformUtility.
                    ScreenPointToLocalPointInRectangle(m_HandlingArea, pressPos, cam, out localPressPos);

            Axis = localPressPos - m_HandlingArea.rect.center;
            UpdateHandle();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            m_IsDragging = false;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            FadeGraphicsToMax();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            FadeGraphicsToMin();
        }

        //Helper Functions
        private void UpdateHandle()
        {
            if (m_Handle && m_HandlingArea)
            {
                float x = m_Axis.x * (m_HandlingArea.sizeDelta.x * 0.5f);
                float y = m_Axis.y * (m_HandlingArea.sizeDelta.y * 0.5f);
                m_Handle.anchoredPosition = new Vector2(x, y);
            }
        }
        private Vector2 NormalizeAxis(Vector2 a)
        {
            Vector2 axis = a;
            axis.x = axis.x / (m_HandlingArea.sizeDelta.x * 0.5f);
            axis.y = axis.y / (m_HandlingArea.sizeDelta.y * 0.5f);
            return axis;
        }
        private Vector2 FitAxis(Vector2 a)
        {
            Vector2 axis = a;
            switch (m_AxesToUse)
            {
                case AxisOption.Both:
                    break;
                case AxisOption.OnlyHorizontal:
                    axis.y = 0;
                    break;
                case AxisOption.OnlyVertical:
                    axis.x = 0;
                    break;
                default:
                    break;
            }
            return axis;
        }
        private void SetGraphicsAlphaTo(float val)
        {
            foreach (GraphicsToFadeData d in activeGraphics)
            {
                if(d.graphic == null) continue;
                d.graphic.alpha = val;
            }
        }
        private void FadeGraphicsToMin()
        {
            foreach(GraphicsToFadeData d in activeGraphics)
            {
                if(d.graphic == null) continue;
                d.graphic.DOFade(d.minAlpha,d.during);
            }
        }
        private void FadeGraphicsToMax()
        {
            foreach(GraphicsToFadeData d in activeGraphics)
            {
                if(d.graphic == null) continue;
                d.graphic.DOFade(d.maxAlpha,d.during);
            }
        }
    }
}