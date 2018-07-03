using System;

namespace WolarGames.Variables
{
    [Serializable]
    public class StringReference : Reference<string>
    {
        public StringVariable VariableReference;

        public override Variable<string> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}