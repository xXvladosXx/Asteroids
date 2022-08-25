using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class EnemyHealthUI : StaticUIElement
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Color _low;
        [SerializeField] private Color _high;
        [SerializeField] private Vector3 _offset;

        public void Init(float maxValue, float currentValue)
        {
            _slider.value = currentValue;
            _slider.maxValue = maxValue;
            
            UpdateHealth(maxValue, currentValue);
        }
        
        public void UpdateHealth(float maxValue, float currentValue)
        {
            print("HealthUpdated");
            
            _slider.gameObject.SetActive(currentValue < maxValue);
            _slider.value = currentValue;
            _slider.maxValue = maxValue;

            _slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_low, _high, _slider.normalizedValue);
        }
       
    }
}