using UnityEngine;

// Idea taken from: http://wiki.unity3d.com/index.php/ExposePropertiesInInspector_Generic
// Original author: Venryx (venryx) (variant from Mift (mift) )
// Variant author: Tiago Roldão(Tiago Roldão) 

namespace WolarGames.Variables.Utils
{
    /// Simplification that allows any scriptable object inheriting from this one to expose it's properties by using [ExposeProperty] tag
    public class ExposableScriptableObject : ScriptableObject
    { }
}