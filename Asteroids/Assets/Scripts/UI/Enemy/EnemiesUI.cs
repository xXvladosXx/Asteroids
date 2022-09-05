using System.Collections.Generic;
using Combat.Core;
using UI.Core;
using UnityEngine;

namespace UI.Enemy
{
    public class EnemiesUI : StaticUIElement
    {
        [SerializeField] private EnemyUI _enemyUI;

        private List<EnemyUI> _enemyUis = new List<EnemyUI>();

        public int AddNewEnemyUI()
        {
            var enemyUI = Instantiate(_enemyUI, transform);
            enemyUI.Refresh(new EnemyData(0));
            
            _enemyUis.Add(enemyUI);
            
            return enemyUI.GetInstanceID();
        }

        public void RefreshData(int value, EnemyData enemyData)
        {
            foreach (var enemyUi in _enemyUis)
            {
                if(enemyUi.GetInstanceID() == value)
                    enemyUi.Refresh(enemyData);
            }
        }

        public void RemoveEnemyUI(int id)
        {
            foreach (var enemyUi in _enemyUis)
            {
                if (enemyUi.GetInstanceID() == id)
                {
                    Destroy(enemyUi.gameObject);
                }
            }
        }
    }

    public struct EnemyData
    {
        public readonly int Points;

        public EnemyData(int points)
        {
            Points = points;
        }
    }
}