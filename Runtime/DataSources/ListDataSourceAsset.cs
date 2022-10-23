using System.Collections.Generic;
using Codice.Client.GameUI.Checkin;
using DynamicValues.GameEvents;
using UnityEngine;

namespace DynamicValues.DataSources
{
    public abstract class ListDataSourceAsset<T> : DataSourceAsset<List<T>>
    {
        [SerializeField]
        private List<T> _initialValue;
        [SerializeField]
        private GameEventAsset<T> _addedItem;
        [SerializeField]
        private GameEventAsset<T> _removedItem;
        [SerializeField]
        private GameEventAsset<List<T>> _listChanged;

        protected override List<T> InitialValue
        {
            get
            {
                _value = new List<T>();
                _value.AddRange(_initialValue);
                return _value;
            }
        }

        public void AddItem(T item)
        {
            _value.Add(item);

            OnItemAdd(item);
        }

        public void RemoveItem(T item)
        {
            _value.Remove(item);
            
            OnItemRemove(item);
        }

        public void AddRange(List<T> items)
        {
            foreach (var item in items)
            {
                _value.Add(item);
                if (_addedItem != null)
                {
                    _addedItem.Dispatch(item);
                }
            }
            
            OnChange();
        }

        public void Insert(int index, T item)
        {
            _value.Insert(index, item);
            
            OnItemAdd(item);
        }

        public void Clear()
        {
            var removedItems = new List<T>();
            removedItems.AddRange(_value);
            
            _value.Clear();

            foreach (var removedItem in removedItems)
            {
                if (_removedItem != null)
                {
                    _removedItem.Dispatch(removedItem);
                }
            }
        }

        public bool Contains(T item)
        {
            return _value.Contains(item);
        }

        public void Reverse()
        {
            _value.Reverse();
            
            OnChange();
        }

        public void RemoveAt(int index)
        {
            var item = _value[index];
            _value.RemoveAt(index);
            OnItemRemove(item);
        }
        
        private void OnItemAdd(T item)
        {
            if (_addedItem != null)
            {
                _addedItem.Dispatch(item);
            }
            
            OnChange();
        }

        private void OnItemRemove(T item)
        {
            if (_removedItem != null)
            {
                _removedItem.Dispatch(item);
            }
            
            OnChange();
        }

        private void OnChange()
        {
            if (_listChanged != null)
            {
                _listChanged.Dispatch(_value);
            }
        }
    }
}
