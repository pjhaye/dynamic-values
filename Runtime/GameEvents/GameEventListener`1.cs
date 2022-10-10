using System.Collections.Generic;
using DynamicValues.GameEvents.ValueConditions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DynamicValues.GameEvents
{
    public class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField]
        [TextArea]
        private string _notes;
        [SerializeField]
        public UnityEvent<T> _response;
        [SerializeField]
        private List<ValueCondition<T>> _conditions;
        [SerializeField]
        private ValueConditionOperator _operator;
        [SerializeField]
        private GameEventAsset<T> _event;
        [SerializeField]
        private bool _replayBufferedEventsOnEnable;

        public GameEventAsset<T> Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
            }
        }

        public void OnInvoke(T value)
        {
            if (!ValueConditionEvaluator.EvaluateValueConditions(value, _conditions, _operator))
            {
                return;
            }
            
            _response?.Invoke(value);
        }

        private void OnEnable()
        {
            if (_event == null)
            {
                return;
            }
            
            _event.AddListener(this);

            if (_replayBufferedEventsOnEnable)
            {
                var numBufferedEvents = _event.BufferedEvents.Count;
                for (var i = numBufferedEvents - 1; i >= 0; i--)
                {
                    var bufferedEvent = _event.BufferedEvents[i];
                    _response?.Invoke(bufferedEvent);
                }
            }
        }

        private void OnDisable()
        {
            if (_event == null)
            {
                return;
            }

            _event.RemoveListener(this);
        }

        [Button(DisplayParameters = true, Style = ButtonStyle.Box)]
        public void ForceInvoke(T value)
        {
            _event.Dispatch(value);
        }
    }
}