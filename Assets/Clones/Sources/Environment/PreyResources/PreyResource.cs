using Clones.Character;
using Clones.GameLogic;
using Clones.Types;
using System;
using UnityEngine;

namespace Clones.Environment
{
    public class PreyResource : MonoBehaviour, IDroppable
    {
        [SerializeField] private PreyResourceDestroyTimer _destroyTimer;

        private bool _isAlive;
        private int _hitsCountToDie;

        public event Action<IDamageable> Died;
        public event Action Damaged;

        public bool IsAlive => _isAlive;
        public QuestItemType DroppedItem { get; private set; }

        public void Init(int hitsCountToDie, QuestItemType droppedItem)
        {
            _isAlive = true;

            _hitsCountToDie = hitsCountToDie;
            DroppedItem = droppedItem;
        }

        public void Accept(IDroppableVisitor visitor) =>
            visitor.Visit(this);

        public void TakeDamage(float damage)
        {
            _hitsCountToDie--;

            if (_hitsCountToDie <= 0)
            {
                _isAlive = false;
                Died?.Invoke(this);
                _destroyTimer.Destroy();
            }
            else
            {
                Damaged?.Invoke();
            }
        }
    }
}