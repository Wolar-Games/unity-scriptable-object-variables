using UniRx;
using UnityEngine.Assertions;

namespace WolarGames.Variables
{
    /// Generic base variable reference that all other variable references inherit
    /// Can (should) only be used for getting values, not setting them
    public abstract class Reference<T>
    {
        public bool UseConstant = true;
        public T ConstantValue;
        public virtual Variable<T> Variable {
            get {
                return null;
            }
        }

        public Reference() { }

        public Reference(T value): this() {
            UseConstant = true;
            ConstantValue = value;
        }

        public IObservable<T> AsObservable
        {
            get {
                Assert.IsTrue(UseConstant || Variable != null, "Using variable value with no variable assigned");
                return UseConstant ? Observable.Return(ConstantValue) : Variable.AsObservable(); 
            }
        }

        public T Value
        {
            get
            {
                return UseConstant ? ConstantValue : Variable.CurrentValue;;                
            }
        }

        public static implicit operator T(Reference<T> reference)
        {
            return reference.Value;
        }
    }
}