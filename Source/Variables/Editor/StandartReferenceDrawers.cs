using UnityEditor;

namespace WolarGames.Variables
{
    // Concrete implementations to work with Unity editor
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferenceDrawer : ReferenceDrawer
    { }

    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : ReferenceDrawer
    { }

    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : ReferenceDrawer
    { }

    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : ReferenceDrawer
    { }
}