using UnityEngine;

public class MiningFacility : MonoBehaviour, IDamageble
{
    public Vector3 Position => transform.position;

    public void TakeDamage(float damage)
    {

    }
}
