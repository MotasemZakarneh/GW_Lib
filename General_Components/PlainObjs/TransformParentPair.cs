using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class TransformParentPair
    {
        public Transform t = null;
        public Transform parent = null;
        public Vector3 startLocalPos = new Vector3();
        public Quaternion startLocalRot = Quaternion.identity;
        //public bool followsParent = false;
        public void Restore()
        {
            t.SetParent(parent);
            t.localPosition = startLocalPos;
            t.localRotation = startLocalRot;
        }
        public TransformParentPair(Transform t, Transform parent)
        {
            this.t = t;
            this.parent = parent;
            //followsParent = r.followsParent;
            startLocalPos = t.localPosition;
            startLocalRot = t.localRotation;
        }
    }
}