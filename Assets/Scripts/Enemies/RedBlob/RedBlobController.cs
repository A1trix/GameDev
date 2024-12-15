using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RedBlobController : MonoBehaviour
{
    private RedBlobAnimation redBlobAnimation;
    private EnemyBaseMovement enemyBaseMovement;

    private void Start()
    {
      redBlobAnimation = GetComponent<RedBlobAnimation>();
    }

    private void Update()
    {
      redBlobAnimation.UpdateAnimation();
    }
}
