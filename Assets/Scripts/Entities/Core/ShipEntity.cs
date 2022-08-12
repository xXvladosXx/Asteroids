using Data.Combat;
using UnityEngine;

namespace Entities.Core
{
    public class ShipEntity : AliveEntity
    {
        [field: SerializeField] public CombatData CombatData { get; private set; }
    }
}