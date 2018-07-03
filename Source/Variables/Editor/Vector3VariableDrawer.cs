using UnityEditor;
using UnityEngine;

namespace WolarGames.Variables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableDrawer : VariableDrawer<Vector3>
    { }
}