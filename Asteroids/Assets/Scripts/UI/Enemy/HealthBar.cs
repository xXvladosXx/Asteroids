using StatsSystem;
using UnityEngine;

namespace UI.Enemy
{
    public class HealthBar : MonoBehaviour
    {
        private Heath _heath;
        private MaterialPropertyBlock _matBlock;
        private MeshRenderer _meshRenderer;

        private static readonly int Fill = Shader.PropertyToID("_Fill");

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _matBlock = new MaterialPropertyBlock();
        }

        public void Init(Heath heath)
        {
            _heath = heath;
        
            UpdateParams(_heath.MaxValue, _heath.CurrentValue);
            _heath.OnHealthChanged += UpdateParams;
        }

        private void Update()
        {
            _meshRenderer.enabled = _heath.CurrentValue < _heath.MaxValue;
        }

        private void UpdateParams(float maxValue, float curValue)
        {
            _meshRenderer.GetPropertyBlock(_matBlock);
            _matBlock.SetFloat(Fill, _heath.CurrentValue / (float) _heath.MaxValue);
            _meshRenderer.SetPropertyBlock(_matBlock);
        }
    }
}