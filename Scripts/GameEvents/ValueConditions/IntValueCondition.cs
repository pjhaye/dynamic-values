using DynamicValues.DataReferences;
using UnityEngine;

namespace DynamicValues.GameEvents.ValueConditions
{
    [CreateAssetMenu(menuName = "Dynamic Values/Conditions/Int Value Condition")]
    public class IntValueCondition : ValueCondition<int>
    {
        [SerializeField]
        private ConditionComparisonOperator _comparisonOperator;
        [SerializeField]
        private IntReference _targetValue;
        
        public override bool Evaluate(int value)
        {
            switch (_comparisonOperator)
            {
                case ConditionComparisonOperator.EqualTo:
                    return value == _targetValue.Value;
                case ConditionComparisonOperator.NotEqualTo:
                    return value != _targetValue.Value;
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