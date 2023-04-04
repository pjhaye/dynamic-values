using UnityEngine;

namespace DynamicValues.DataSources
{
    [CreateAssetMenu(menuName = "Dynamic Values/Data Sources/Vector3 Data Source Asset")]
    public class Vector3DataSourceAsset : DataSourceAsset<Vector3>
    {
        [SerializeField]
        private Vector3 _initialValue;
        
        protected override Vector3 InitialValue => _initialValue;
    }
}