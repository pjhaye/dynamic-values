using UnityEngine;

namespace DynamicValues.DataSources
{
    [CreateAssetMenu(menuName = "Dynamic Values/Data Sources/Int Data Source Asset")]
    public class IntDataSourceAsset : DataSourceAsset<int>
    {
        [SerializeField]
        private int _initialValue;
        
        protected override int InitialValue => _initialValue;
    }
}