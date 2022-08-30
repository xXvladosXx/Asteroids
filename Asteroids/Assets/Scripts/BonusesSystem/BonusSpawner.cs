using System.Collections.Generic;
using UnityEngine;

namespace BonusesSystem
{
    public class BonusSpawner : MonoBehaviour
    {
        [field: SerializeField] public List<TimeableBonus> BonusListToSpawn { get; private set; }
        
        
    }
}