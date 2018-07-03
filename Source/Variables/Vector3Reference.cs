using System;
using UnityEngine;

namespace WolarGames.Variables
{
    [Serializable]
    public class Vector3Reference : Reference<Vector3>
    {
        public Vector3Variable VariableReference;

        public override Variable<Vector3> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}