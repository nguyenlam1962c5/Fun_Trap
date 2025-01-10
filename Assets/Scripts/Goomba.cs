using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    // Chỉ còn lại biến canBeKilledByJump
    public bool canBeKilledByJump = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            // Kiểm tra nếu Goomba có thể bị tiêu diệt bởi nhảy từ trên
            if (collision.transform.DotTest(transform, Vector2.down) && canBeKilledByJump)
            {
                Flatten();
            }
            else
            {
                player.Hit();  // Người chơi bị mất mạng nếu va chạm không tiêu diệt Goomba
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
