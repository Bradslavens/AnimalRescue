using UnityEngine;
using System.Collections;


public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    private int currentWaypointIndex;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            currentWaypointIndex = 0;
            StartCoroutine(Patrol());
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            // Move to the next waypoint
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for the specified time
            yield return new WaitForSeconds(waitTime);

            // Update the waypoint index
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
