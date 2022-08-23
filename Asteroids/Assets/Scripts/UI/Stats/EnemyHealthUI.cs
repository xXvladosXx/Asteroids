using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Color _low;
        [SerializeField] private Color _high;
        [SerializeField] private Vector3 _offset;

        public void SetHealth(float health, float maxHealth)
        {
            _slider.gameObject.SetActive(health < maxHealth);
            _slider.value = health;
            _slider.maxValue = maxHealth;

            _slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_low, _high, _slider.normalizedValue);
        }
        
        private void Update()
        {
            _slider.transform.position =
                UnityEngine.Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
        }
    }
}