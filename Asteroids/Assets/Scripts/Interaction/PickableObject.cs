using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class PickableObject : SerializedMonoBehaviour
    {
        [field: SerializeField] public IPickable Pickable { get; private set; }
        [field: SerializeField] public float PositionModifier { get; private set; }
        
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence().SetAutoKill(false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);;
            _sequence.Append(transform.DOLocalMoveY(transform.position.y + PositionModifier, 2f, false));
            _sequence.Append(transform.DOLocalMoveY(transform.position.y - PositionModifier, 2f, false));
        }
        
        private void OnDestroy()
        {
            _sequence.Kill(true);
        }
    }
}