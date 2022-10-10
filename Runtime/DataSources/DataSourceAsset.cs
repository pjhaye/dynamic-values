using System;
using DynamicValues.GameEvents;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace DynamicValues.DataSources
{
    public abstract class DataSourceAsset<T> : ScriptableObject
    {
        public GameEventAsset<T> ValueChangedEvent;
        public event Action<DataSourceAsset<T>, T> ValueChanged;

        private bool _isFirstAccess = true;
        
        protected T _value;

        protected virtual T InitialValue
        {
            get
            {
                return default;
            }
        }
        
        public T Value
        {
            get
            {
                if (_isFirstAccess)
                {
                    _value = InitialValue;
                    _isFirstAccess = false;
                    OnValueChange(default, _value);
                }
                
                return _value;
            }
            set
            {
                if (_isFirstAccess)
                {
                    _value = InitialValue;
                    _isFirstAccess = false;
                }
                
                var prevValue = _value;
                if (prevValue != null && prevValue.Equals(value))
                {
                    return;
                }
                
                _value = value;
                
                ValueChanged?.Invoke(this, _value);
                if (ValueChangedEvent != null)
                {
                    ValueChangedEvent.Dispatch(_value);
                }
                
                OnValueChange(prevValue, _value);
            }
        }

        protected virtual void OnEnable()
        {
            Value = InitialValue;
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
            EditorApplication.playModeStateChanged += OnPlayModeStateChange;
#endif
        }

        protected virtual void OnValueChange(T prevValue,T value)
        {
            
        }
        
        private void OnPlayModeStateChange(PlayModeStateChange playModeStateChange)
        {
            if(playModeStateChange == PlayModeStateChange.ExitingEditMode)
            {
                Value = InitialValue;
            }
            else if (playModeStateChange == PlayModeStateChange.EnteredPlayMode)
            {
                if (ValueChangedEvent != null)
                {
                    ValueChangedEvent.Dispatch(Value);
                }
            }
            else if (playModeStateChange == PlayModeStateChange.ExitingPlayMode)
            {
                _isFirstAccess = true;
            }
        }

        [Button(DisplayParameters = true, Style = ButtonStyle.Box)]
        public void ForceChangeValue(T value)
        {
            Value = value;
        }
    }
}