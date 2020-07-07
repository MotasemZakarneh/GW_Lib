using System;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [Serializable]
    public class Condition
    {
        public int Hash { get { return Animator.StringToHash(description); } }
        public int iD;
        public string description;
        public bool satisfied;

        public Condition()
        {

        }
        public Condition(int iD, string description, bool satisfied = false)
        {
            SetInitials(iD, description, satisfied);
        }
        public Condition(Condition condToClone)
        {
            SetInitials(condToClone.iD, condToClone.description, condToClone.satisfied);
        }
        void SetInitials(int iD, string description, bool satisfied = false)
        {
            this.iD = iD;
            this.satisfied = satisfied;
            this.description = description;
        }
        public Condition Clone()
        {
            return MemberwiseClone() as Condition;
        }
        public bool IsSatisfied(Condition sourceCondition)
        {
            return satisfied == sourceCondition.satisfied;
        }
        public static bool operator ==(Condition a, Condition b)
        {
            bool aNull = ReferenceEquals(a, null);
            bool bNull = ReferenceEquals(b, null);
            if (aNull && bNull)
            {
                return true;
            }
            else if (aNull||bNull)
            {
                return false;
            }
            bool goodHash = a.Hash == b.Hash;
            bool goodID = a.iD == b.iD;
            return goodHash && goodID;
        }
        public static bool operator !=(Condition a, Condition b)
        {
            bool areEqual = a == b;
            return !areEqual;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return this == (Condition)obj;
        }
        public override string ToString()
        {
            return description + "_" + iD;
        }
    }
}