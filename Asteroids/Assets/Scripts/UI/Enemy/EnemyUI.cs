using EnemyShipZenject;
using TMPro;
using UI.Core;
using UI.Score;
using UI.Stats;
using UnityEngine;

namespace UI.Enemy
{
    public class EnemyUI : StaticUIElement
    {
        [SerializeField] private ScoreCounterUI _scoreCounterUI;
        [SerializeField] private TextMeshProUGUI _name;

        public void Refresh(EnemyData enemyData)
        {
            _scoreCounterUI.ChangeScore(enemyData.Points);   
        }
    }
}