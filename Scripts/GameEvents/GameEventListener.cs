using UnityEngine;
using UnityEngine.Events;

namespace DynamicValues.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        [TextArea]
        private string _notes;
        [SerializeField]
        public UnityEvent _response;
        [SerializeField]
        private GameEventAsset _event;
        
        public void OnInvoke()
        {
            _response?.Invoke();
        }

        private void OnEnable()
        {
            _event.AddListener(this);
        }

        private void OnDisable()
        {
            _event.RemoveListener(this);
        }
    }
}