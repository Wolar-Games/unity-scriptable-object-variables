using UniRx;
using UnityEngine.Assertions;

namespace WolarGames.Variables
{
    /// Generic base variable reference that all other variable references inherit
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

        public IObservable<T> Value
        {
            get {
                Assert.IsTrue(UseConstant || Variable != null, "Using variable value with no variable assigned");
                return UseConstant ? Observable.Return(ConstantValue) : Variable.CurrentValue.AsObservable(); 
            }
        }

        public static implicit operator T(Reference<T> reference) {
            return reference.UseConstant ? reference.ConstantValue : reference.Variable.CurrentValue.Value;
        }
    }
}