using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;
    public float smoothRotationSpeed = 5f;
    public float maxCameraDistance = 10f;

    private Vector3 cameraOffset;

    private void Start()
    {
        if (cameraTransform != null)
        {
            // Calculate the initial camera offset from the player
            cameraOffset = cameraTransform.position - transform.position;
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        movement.Normalize();

        transform.position += movement * moveSpeed * Time.deltaTime;

        SmoothFollowPlayer();
    }

    private void SmoothFollowPlayer()
    {
        if (cameraTransform != null)
        {
            Vector3 targetCameraPosition = transform.position + cameraOffset;

            targetCameraPosition = LimitCameraDistance(targetCameraPosition);

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, smoothRotationSpeed * Time.deltaTime);

            cameraTransform.LookAt(transform);
        }
    }

    private Vector3 LimitCameraDistance(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance > maxCameraDistance)
        {
            Vector3 direction = targetPosition - transform.position;

            targetPosition = transform.position + direction.normalized * maxCameraDistance;
        }

        return targetPosition;
    }
}
