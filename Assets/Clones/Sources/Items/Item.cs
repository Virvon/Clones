using Clones.Data;
using Clones.StaticData;
using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemData Data { get; private set; }

    private Coroutine _move;

    public abstract void Accept(IItemVisitor visitor);

    public void Init(ItemData itemData) => Data = itemData;

    public void TakeMove(Vector3 targetPosition, float speed)
    {
        if (_move != null)
            StopCoroutine(_move);

        _move = StartCoroutine(Move(targetPosition, speed));
    }

    public void TakeMove(Transform targetTransform, float speed)
    {
        if (_move != null)
            StopCoroutine(_move);

        _move = StartCoroutine(Move(targetTransform, speed));
    }

    private IEnumerator Move(Vector3 targetPosition, float speed)
    {
        while(transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator Move(Transform targetTransform, float speed)
    {
        while (transform.position != targetTransform.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);

            yield return null;
        }
    }
}
