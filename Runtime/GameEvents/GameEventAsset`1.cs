using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DynamicValues.GameEvents
{
    public abstract class GameEventAsset<T> : ScriptableObject
    {
        [SerializeField]
        [MinValue(0)]
        private int _replayBufferSize = 1;
        
        private List<GameEventListener<T>> _listeners = new List<GameEventListener<T>>();
        private List<Action<T>> _actions = new List<Action<T>>();

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(2)]
        private List<T> _bufferedEvents = new List<T>();

        public List<T> BufferedEvents => _bufferedEvents;

        public void Dispatch(T value)
        {
            var numListeners = _listeners.Count;
            for (var i = numListeners - 1; i >= 0; i--)
            {
                var listener = _listeners[i];
                listener.OnInvoke(value);
            }
            
            var numActions = _actions.Count;
            for (var i = numActions - 1; i >= 0; i--)
            {
                var action = _actions[i];
                action.Invoke(value);
            }

            if (_replayBufferSize > 0)
            {
                _bufferedEvents.Add(value);
                while (_bufferedEvents.Count > Mathf.Max(0, _replayBufferSize))
                {
                    _bufferedEvents.Remove(_bufferedEvents[0]);
                }
            }
        }

        public void AddListener(Action<T> listener)
        {
            if (_actions.Contains(listener))
            {
                return;
            }
            
            _actions.Add(listener);
        }
        
        public void AddListener(GameEventListener<T> listener)
        {
            if (_listeners.Contains(listener))
            {
                return;
            }
            
            _listeners.Add(listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            if (!_actions.Contains(listener))
            {
                return;
            }

            _actions.Remove(listener);
        }
        
        public void RemoveListener(GameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                return;
            }

            _listeners.Remove(listener);
        }

        [Button(DisplayParameters = true, Style = ButtonStyle.Box)]
        public void ForceDispatch(T value)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            
            Dispatch(value);
        }
    }
}