using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Target { get; private set; }

    public void Init(GameObject target)
    {
        Target = target;
    }
}
