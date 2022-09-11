using System;
using DynamicValues.DataReferences;
using UnityEngine;

namespace DynamicValues.GameEvents.ValueConditions
{
    [CreateAssetMenu(menuName = "Dynamic Values/Conditions/Float Value Condition")]
    public class FloatValueCondition : ValueCondition<float>
    {
        [SerializeField]
        private ConditionComparisonOperator _comparisonOperator;
        [SerializeField]
        private FloatReference _targetValue;
        
        public override bool Evaluate(float value)
        {
            switch (_comparisonOperator)
            {
                case ConditionComparisonOperator.EqualTo:
                    return Math.Abs(value - _targetValue.Value) < float.Epsilon;
                case ConditionComparisonOperator.NotEqualTo:
                    return Math.Abs(value - _targetValue.Value) > float.Epsilon;
                case ConditionComparisonOperator.GreaterThan:
                    return value > _targetValue.Value;
                case ConditionComparisonOperator.GreaterThanOrEqualTo:
                    return value >= _targetValue.Value;
                case ConditionComparisonOperator.LessThan:
                    return value < _targetValue.Value;
                case ConditionComparisonOperator.LessThanOrEqualTo:
                    return value <= _targetValue.Value;
            }

            return false;
        }
    }
}