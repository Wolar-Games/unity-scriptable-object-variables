#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif
using System;
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

#if REACTIVE_VARIABLE_RX_ENABLED
        public IObservable<T> AsObservable()
        {
            Assert.IsTrue(UseConstant || Variable != null, "Using variable value with no variable assigned");
            return UseConstant ? Observable.Return(ConstantValue) : Variable.AsObservable(); 
        }
#else
        
        public Action<T> OnValueChanged
        {
            get
            {
                if (UseConstant)
                {
                    return new Action<T>(_ =>
                    {
                        
                    });
                }
                else
                {
                    return Variable.OnValueChanged;
                }
            }
            set
            {
                if (!UseConstant)
                {
                    Variable.OnValueChanged = value;
                }
            }
        }
#endif
    
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