using System;

namespace WolarGames.Variables
{
    [Serializable]
    public class BoolReference : Reference<bool>
    {
        public BoolVariable VariableReference;

        public override Variable<bool> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}