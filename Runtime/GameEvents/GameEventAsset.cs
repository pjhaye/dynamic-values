using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DynamicValues.GameEvents
{
    [CreateAssetMenu(menuName = "Dynamic Values/Game Events/Game Event")]
    public class GameEventAsset : ScriptableObject
    {
        private List<GameEventListener> _listeners;

        public void Dispatch()
        {
            var numListeners = _listeners.Count;
            for (var i = numListeners - 1; i >= 0; i--)
            {
                var listener = _listeners[i];
                listener.OnInvoke();
            }
        }

        public void AddListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                return;
            }
            
            _listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                return;
            }

            _listeners.Remove(listener);
        }
        
        [Button]
        public void ForceDispatch()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            
            Dispatch();
        }
    }
}