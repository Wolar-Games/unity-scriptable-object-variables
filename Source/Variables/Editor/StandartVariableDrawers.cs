using UnityEngine;
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

    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableDrawer : VariableDrawer<Vector3>
    { }

    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableDrawer : VariableDrawer<Vector2>
    { }
}