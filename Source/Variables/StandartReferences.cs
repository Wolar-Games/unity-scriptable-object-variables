using System;

namespace WolarGames.Variables
{
    // These concrete implementations are here because of Unity serialization mechanism, I didn't come up with better generic solution so far

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