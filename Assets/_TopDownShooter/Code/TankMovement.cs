using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] private Vector2 xyLimit;
    [SerializeField] protected Rigidbody2D rb;

    [Header("Tank parts")]
    [SerializeField] private GameObject canon;
    [SerializeField] private GameObject body;

    protected void Move(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xyLimit.x, xyLimit.x), Mathf.Clamp(transform.position.y, -xyLimit.y, xyLimit.y), 0);
        RotateBody(direction);
    }

    protected void RotateCanon(Vector3 targetPos)
    {
        
        float angleRad = Mathf.Atan2(targetPos.y - canon.transform.position.y, targetPos.x - canon.transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;

        canon.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }

    private void RotateBody(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
            body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
