using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    public float moveTime = 0.1f;
    private Rigidbody2D rb;
    private Tilemap tilemap;
    private Vector2 targetPosition;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tilemap = FindObjectOfType<Tilemap>();
        targetPosition = rb.position;
        isMoving = false;
    }

    private void Update()
    {
        if (!isMoving)
        {
            int moveX = (int)Input.GetAxisRaw("Horizontal");
            int moveY = (int)Input.GetAxisRaw("Vertical");

            if (moveX != 0 || moveY != 0)
            {
                StartCoroutine(Move(new Vector2(moveX, moveY)));
            }
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector2 startPosition = rb.position;
        targetPosition = startPosition + direction;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            yield return null;
        }

        rb.position = targetPosition;
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TilemapCollider2D tilemapCollider = collision.collider.GetComponent<TilemapCollider2D>();

        if (tilemapCollider != null)
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }

            AstarPath.active.Scan();
        }
    }
}
