using UnityEditor;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableDrawer : VariableDrawer<float>
    { }

    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableDrawer : VariableDrawer<bool>
    { }

    [CustomEditor(typeof(IntVariable))]
    public class IntVariableDrawer : VariableDrawer<int>
    { }

    [CustomEditor(typeof(FloatVariable))]
    public class StringVariableDrawer : VariableDrawer<string>
    { }
}