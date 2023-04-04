using UnityEngine;

namespace DynamicValues.DataSources
{
    [CreateAssetMenu(menuName = "Dynamic Values/Data Sources/String Data Source Asset")]
    public class StringDataSourceAsset : DataSourceAsset<string>
    {
        [SerializeField]
        private string _initialValue;
        
        protected override string InitialValue => _initialValue;
    }
}