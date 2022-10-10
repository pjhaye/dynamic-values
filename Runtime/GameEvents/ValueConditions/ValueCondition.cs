using System;
using UnityEngine;

namespace DynamicValues.GameEvents.ValueConditions
{
    [Serializable]
    public abstract class ValueCondition<T> : ScriptableObject
    {
        public abstract bool Evaluate(T value);
    }
}