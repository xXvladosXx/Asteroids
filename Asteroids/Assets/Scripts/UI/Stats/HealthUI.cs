using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class HealthUI : StaticUIElement
    {
        [SerializeField] private GameObject _heartContainerPrefab;

        private GameObject[] _heartContainers;
        private Image[] _heartFills;

        private float _currentHealth;
        private float _maxHealth;

        public void Init(float maxValue, float currentValue)
        {
            _currentHealth = currentValue;
            _maxHealth = maxValue;
            
            _heartContainers = new GameObject[(int) _maxHealth];
            _heartFills = new Image[(int) _maxHealth];

            InstantiateHeartContainers();
            UpdateHeartsHUD(maxValue, currentValue);
        }

        public void UpdateHeartsHUD(float maxValue, float currentValue)
        {
            _currentHealth = currentValue;
            _maxHealth = maxValue;
            
            SetHeartContainers();
            SetFilledHearts();
        }

        void SetHeartContainers()
        {
            for (int i = 0; i < _heartContainers.Length; i++)
            {
                _heartContainers[i].SetActive(i < _maxHealth);
            }
        }

        void SetFilledHearts()
        {
            for (int i = 0; i < _heartFills.Length; i++)
            {
                _heartFills[i].fillAmount = i < _currentHealth ? 1 : 0;
            }

            if (_currentHealth % 1 != 0)
            {
                int lastPos = Mathf.FloorToInt(_currentHealth);
                _heartFills[lastPos].fillAmount = _currentHealth % 1;
            }
        }

        void InstantiateHeartContainers()
        {
            for (int i = 0; i < _maxHealth; i++)
            {
                GameObject temp = Instantiate(_heartContainerPrefab, transform, false);
                _heartContainers[i] = temp;
                _heartFills[i] = temp.transform.Find("Filled").GetComponent<Image>();
            }
        }
    }
}