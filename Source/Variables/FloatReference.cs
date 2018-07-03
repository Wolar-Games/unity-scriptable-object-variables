using System;

namespace WolarGames.Variables
{
    [Serializable]
    public class FloatReference : Reference<float>
    {
        public FloatVariable VariableReference;

        public override Variable<float> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}