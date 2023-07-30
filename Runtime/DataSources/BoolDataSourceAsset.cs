using UnityEngine;

namespace DynamicValues.DataSources
{
    [CreateAssetMenu(menuName = "Dynamic Values/Data Sources/Bool Data Source Asset")]
    public class BoolDataSourceAsset : DataSourceAsset<bool>
    {
        [SerializeField]
        private bool _initialValue;

        protected override bool InitialValue => _initialValue;
    }
}