using System;
using DynamicValues.DataReferences;
using UnityEngine;

namespace DynamicValues.GameEvents.ValueConditions
{
    [CreateAssetMenu(menuName = "Dynamic Values/Conditions/Bool Value Condition")]
    public class BoolValueCondition : ValueCondition<bool>
    {
        [SerializeField]
        private bool _areEqual = true;
        [SerializeField]
        private BoolReference _targetValue;
        
        public override bool Evaluate(bool value)
        {
            if (_areEqual)
            {
                return value == _targetValue.Value;
            }
            else
            {
                return value != _targetValue.Value;
            }
        }
    }
}