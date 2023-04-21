using UnityEngine;

public class AnimalCollision : MonoBehaviour
{
    public GameObject target;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            Destroy(gameObject);
            scoreManager.IncreaseScore();
        }
    }
}
