using UnityEngine;

namespace DynamicValues.DataSources
{
    [CreateAssetMenu(menuName = "Dynamic Values/Data Sources/Float Data Source Asset")]
    public class FloatDataSourceAsset : DataSourceAsset<float>
    {
        [SerializeField]
        private float _initialValue;

        protected override float InitialValue => _initialValue;
    }
}