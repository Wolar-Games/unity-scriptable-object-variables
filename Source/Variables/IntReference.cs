using System;

namespace WolarGames.Variables
{
    [Serializable]
    public class IntReference : Reference<int>
    {
        public IntVariable VariableReference;

        public override Variable<int> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}