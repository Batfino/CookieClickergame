using UnityEngine;

public class RandomWalkerAI2D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    public LayerMask obstacleLayer;
    public Vector2 gridSize = new Vector2(10, 10);

    private Vector2 targetPosition;

    void Start()
    {
        GetNewRandomPosition();
    }

    void Update()
    {
        // Hvis vi er nået til målet, find et nyt
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            GetNewRandomPosition();
        }

        // Undgå hindringer (2D version)
        if (Physics2D.Raycast(transform.position, transform.up, 1f, obstacleLayer))
        {
            GetNewRandomPosition();
        }

        // Bevægelse logik
        MoveTowardsTarget();
    }

    void GetNewRandomPosition()
    {
        // Generer en tilfældig position inden for griddet
        float x = Random.Range(-gridSize.x / 2, gridSize.x / 2);
        float y = Random.Range(-gridSize.y / 2, gridSize.y / 2);
        targetPosition = new Vector2(x, y);

        // Sørg for positionen er inden for griddet
        targetPosition = new Vector2(
            Mathf.Clamp(targetPosition.x, -gridSize.x / 2, gridSize.x / 2),
            Mathf.Clamp(targetPosition.y, -gridSize.y / 2, gridSize.y / 2)
        );
    }

    void MoveTowardsTarget()
    {
        // Drej mod målet (2D version)
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Bevæg fremad (2D version)
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Vis grid i editoren (valgfrit)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(gridSize.x, gridSize.y, 0.1f));
    }
}
