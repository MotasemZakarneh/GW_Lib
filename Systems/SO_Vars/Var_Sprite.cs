using UnityEngine;

namespace GW_Lib.Utility
{
    [CreateAssetMenu(fileName = "Var_Sprite", menuName = "GW_Lib/Data Storage/SO_Variables/Sprite")]
    public class Var_Sprite : Var_SO<Sprite>
    {
        public int ID;
    }
}