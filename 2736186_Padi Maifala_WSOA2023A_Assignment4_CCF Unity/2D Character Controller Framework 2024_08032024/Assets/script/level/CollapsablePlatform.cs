using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class, when placed on a GameObject with a 'Rigidbody2D', and a 'BoxCollider2D' 
 * component, makes it collapse, or fall after the player stands on it.
*/
#endregion

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class CollapsablePlatform : MonoBehaviour
{
    #region PUBLIC FIELDS
    [Header("PLATFORM:")]
    public float timeTillCollapse = 1.0f;
    public float platformMass = 1.0f;
    public float platformGravityScale = 2.5f;
    // 'platformGravityScale' should be set to about 10% of the 
    // 'PlayerMovement' script's 'gravity' variable if you want
    // the platforms to fall at the same speed as the player.
    public float timeTillDestruction = 3.0f;
    [Header("EFFECTS:")]
    public GameObject vfx; 
    public AudioSource sfx;
    #endregion

    [HideInInspector]
    public bool playerOnPlatform;

    #region PRIVATE
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    #endregion

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;

    }
    public void CollapsePlatform()
    {
        StartCoroutine(PlatformWaitTime());
    }

    // This co-routine, once started, waits a set amount of time, 
    // then activates the object's 'Rigidbody2D' component, causing
    // it to fall. It then waits another set amount of time before
    // destroying the object.
    IEnumerator PlatformWaitTime()
    {
        yield return new WaitForSeconds(timeTillCollapse);

        if (vfx != null)
            Instantiate(vfx, this.transform.position, Quaternion.identity);
        if (sfx != null)
            Instantiate(sfx, this.transform.position, Quaternion.identity);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.mass = platformMass;
        _rb.gravityScale = platformGravityScale;
        _collider.enabled = false;

        yield return new WaitForSeconds(timeTillDestruction);
        Destroy(gameObject);
    }
}