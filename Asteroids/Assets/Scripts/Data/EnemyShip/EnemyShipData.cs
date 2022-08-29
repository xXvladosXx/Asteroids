using System;
using Data.EnemyShip.StatesSettings;
using UnityEngine;

namespace Data.EnemyShip
{
    [Serializable]
    public class EnemyShipData
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public StateSettings StateSettings { get; private set; }
    }
}