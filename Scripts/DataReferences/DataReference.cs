using System;
using DynamicValues.DataSources;
using DynamicValues.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DynamicValues.DataReferences
{
    [Serializable]
    [InlineProperty]
    public abstract class DataReference<T>
    {
        [SerializeField]
        [ShowIf(nameof(IsValue))]
        [HorizontalGroup("Fields")]
        [HideLabel]
    
        private T _value;
        [SerializeField]
        [HorizontalGroup("Fields")]
        [HideLabel]
        [ShowIf(nameof(IsAsset))]
    
        private DataSourceAsset<T> dataSourceAsset;
        [SerializeField]
        [HorizontalGroup("Fields")]
        [HideLabel]
        private DataReferenceType _dataReferenceType;

        private bool IsValue => _dataReferenceType == DataReferenceType.Value;
        private bool IsAsset => _dataReferenceType == DataReferenceType.Asset;

        public GameEventAsset<T> GameEventAsset
        {
            get
            {
                switch (_dataReferenceType)
                {
                    case DataReferenceType.Value:
                        Debug.LogError($"{nameof(DataReferenceType)} of {nameof(DataReferenceType.Value)} cannot have events!");
                        return null;

                    case DataReferenceType.Asset:
                        return dataSourceAsset.ValueChangedEvent;
                
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [ShowInInspector]
        [HorizontalGroup("Fields")]
        [ReadOnly]
        [ShowIf(nameof(IsAsset))]
        [PropertyOrder(-1)]
        [HideLabel]
        public T Value
        {
            get
            {
                switch (_dataReferenceType)
                {
                    case DataReferenceType.Value:
                        return _value;

                    case DataReferenceType.Asset:
                        if (dataSourceAsset != null)
                        {
                            return dataSourceAsset.Value;
                        }

                        return default;
                    default:
                        throw new NotImplementedException();
                }
            }
            set
            {
                switch (_dataReferenceType)
                {
                    case DataReferenceType.Value:
                        _value = value;
                        break;
                
                    case DataReferenceType.Asset:
                        if (dataSourceAsset != null)
                        {
                            dataSourceAsset.Value = value;
                        }
                        else
                        {
                            Debug.LogError($"Data Reference value could not be assigned; Data Source is null!");
                            return;
                        }
                        break;
                }
            }
        }
    }
}
