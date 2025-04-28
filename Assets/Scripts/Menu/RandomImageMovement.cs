using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImageMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Minimum movement speed")]
    public float minSpeed = 1.0f;

    [Tooltip("Maximum movement speed")]
    public float maxSpeed = 3.0f;

    [Tooltip("How often the direction changes (in seconds)")]
    public float directionChangeInterval = 2.0f;

    [Tooltip("Bounds for horizontal movement")]
    public float horizontalBounds = 10.0f;

    [Tooltip("Bounds for vertical movement")]
    public float verticalBounds = 6.0f;

    private Vector3 targetPosition;
    private float speed;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        PickNewTarget();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards target position
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Update timer and pick new target when needed
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTarget();
            timer = 0;
        }
    }

    void PickNewTarget()
    {
        // Generate random position within bounds
        float targetX = Random.Range(-horizontalBounds, horizontalBounds);
        float targetY = Random.Range(-verticalBounds, verticalBounds);
        float targetZ = transform.position.z; // Keep the same Z position

        targetPosition = new Vector3(targetX, targetY, targetZ);

        // Generate random speed
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
