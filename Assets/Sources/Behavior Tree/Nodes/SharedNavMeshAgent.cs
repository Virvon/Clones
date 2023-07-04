using System;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedNavMeshAgent : SharedVariable<NavMeshAgent>
    {
        public static implicit operator SharedNavMeshAgent(NavMeshAgent value) => new SharedNavMeshAgent { Value = value};
    }
}
