using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class defines a projectile that the player can shoot, which will do damage to enemies.
*/
#endregion

public class Projectile : MonoBehaviour
{
    [Header("PROJECTILE:")]
    public float projectileSpeed = 2f;
    public bool flipSprite = true;
    public float cleanupDelay = 8f;
    [Header("EFFECTS:")]
    public GameObject vfx; 
    public AudioSource sfx;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private LayerMask _collisionLayers;
    private CircleCollider2D circCol;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        circCol = GetComponent<CircleCollider2D>();
        _collisionLayers = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().projectileCollision;

    }

    void Start()
    {
        // Makes sure that the sprite is facing the correct way, by checking the velocity of its RigidBody2D component.
        if (_rb.velocity.x < 0f && flipSprite)
            _sprite.flipX = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check to make sure that the projectile isn't hitting the player, 
        // and if not, call the DestroyProjectile function below.
        if (!col.CompareTag("Player") && circCol.IsTouchingLayers(_collisionLayers))
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        if (vfx != null)
            Instantiate(vfx, transform.position, Quaternion.identity);
        if (sfx != null)
            Instantiate(sfx, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    IEnumerator DestroyDelay()
    {
        // This co-routine waits for a set amount of time before destroying the projectile
        // so that we don't end up with millions of projectiles in the scene.
        yield return new WaitForSeconds(cleanupDelay);
        Destroy(gameObject);
    }
}
