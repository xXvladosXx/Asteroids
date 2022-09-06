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

        public EnemyUI AddNewEnemyUI()
        {
            var enemyUI = Instantiate(_enemyUI, transform);
            enemyUI.Refresh(new EnemyData(0));
            
            _enemyUis.Add(enemyUI);
            
            return enemyUI;
        }

        public void RefreshData(EnemyUI value, EnemyData enemyData)
        {
            foreach (var enemyUi in _enemyUis)
            {
                if(enemyUi == value)
                    enemyUi.Refresh(enemyData);
            }
        }

        public void RemoveEnemyUI(EnemyUI id)
        {
            foreach (var enemyUi in _enemyUis)
            {
                if (enemyUi == id)
                {
                    Destroy(enemyUi.gameObject);
                }
            }

            _enemyUis.Remove(id);
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