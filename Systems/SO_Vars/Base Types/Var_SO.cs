using UnityEngine;
namespace GW_Lib.Utility
{
    public abstract class Var_SO : ScriptableObject
    {

    }
    public abstract class Var_SO<T> : Var_SO
    {
        public T Value;
    }
}