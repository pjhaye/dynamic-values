using System.Collections.Generic;

namespace DynamicValues.GameEvents.ValueConditions
{
    public static class ValueConditionEvaluator
    {
        public static bool EvaluateValueConditions<T>(
            T value,
            List<ValueCondition<T>> valueConditions,
            ValueConditionOperator valueConditionOperator)
        {
            foreach (var condition in valueConditions)
            {
                if (condition.Evaluate(value))
                {
                    if (valueConditionOperator == ValueConditionOperator.Or)
                    {
                        return true;
                    }
                }
                else if (valueConditionOperator == ValueConditionOperator.And)
                {
                    return false;
                }
            }

            return true;
        }
    }
}