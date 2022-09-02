using UnityEngine;

namespace Data.Difficulties
{
    [CreateAssetMenu(menuName = "Data/DifficultiesData")]
    public class DifficultiesData : ScriptableObject
    {
        [field: SerializeField] public DifficultyData[] DifficultyDatas { get; private set; }
    }
}