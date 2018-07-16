using UnityEditor;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableDrawer : VariableDrawer<string>
    { }
}