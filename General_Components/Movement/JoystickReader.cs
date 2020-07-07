using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GW_Lib.Utility
{
    [RequireComponent(typeof(JoyStick))]
    public class JoystickReader : MonoBehaviour,IDragHandler,IEndDragHandler
    {
        public Action<Vector2> OnStickPulled;
        public Action<Vector2> OnStickLetGo;
        
        JoyStick joyStick;
        GameObject[] children;
        CanvasGroup canvasGroup;
        bool isLocked = false;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            joyStick = GetComponent<JoyStick>();
            
            children = new GameObject[transform.childCount];
            for(int i =0;i<transform.childCount;i++)
            {
                children[i] = transform.GetChild(i).gameObject;
            }
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(isLocked)
            {
                return;
            }
            OnStickPulled?.Invoke(joyStick.Axis);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if(isLocked)
            {
                return;
            }
            OnStickLetGo?.Invoke(Vector2.zero);
        }

        //Called from playmakerFSMS or unity events
        public void EnableStick()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            isLocked = false;
        }
        public void DisableStick()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            isLocked = true;
        }
        public Vector3 GetAxisXZ()
        {
            return new Vector3(joyStick.Axis.x,0,joyStick.Axis.y);
        }
    }
}