using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New ShootingEnemy", menuName = "Data/Create new shooting enemy", order = 51)]
    public class ShootingEnemyStaticData : EnemyStaticData
    {
        public BulletType BulletType;
    }
}
