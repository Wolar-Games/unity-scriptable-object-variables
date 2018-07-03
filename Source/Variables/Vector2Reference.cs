using System;
using UnityEngine;

namespace WolarGames.Variables
{
    [Serializable]
    public class Vector2Reference : Reference<Vector2>
    {
        public Vector2Variable VariableReference;

        public override Variable<Vector2> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}