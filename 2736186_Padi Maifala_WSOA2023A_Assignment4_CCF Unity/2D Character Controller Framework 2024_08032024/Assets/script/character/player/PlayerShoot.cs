using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class controls the player's shooting behaviour. Most of the logic 
 * is contained in the Shoot function below.
*/
#endregion

public class PlayerShoot : MonoBehaviour
{
    #region PUBLIC FIELDS:
    [Header("INITIALIZATIONS:")]
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    [Header("PROJECTILE:")]
    public int projectileDamage = 1;
    public float projectileFireRate = 0.5f;
    public KeyCode playerShootButton;
    public LayerMask projectileCollision;
    #endregion

    #region PRIVATE:
    private float _projectileSpeed = 1;
    private PlayerMovement _playerMovement;
    private bool _canShoot = true;
    #endregion

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        
        // Just some reminders for us in case we forget to assign a projectile or spawn point.
        if (projectileSpawnPoint == null)
            Debug.Log("ERROR: You need to assign a transform for your projectiles to spawn at!");

        if (projectilePrefab == null)
            Debug.Log("ERROR: You need to assign a projectile prefab!");
        else
            _projectileSpeed = projectilePrefab.GetComponent<Projectile>().projectileSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(playerShootButton) && _playerMovement.shootOn && _canShoot)
        {
            Shoot();
            StartCoroutine(ProjectileDelay());
        }
    }

    void Shoot()
    {
        // Instantiate a projectile prefab.
        GameObject projectile;
        projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
 
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // If we're facing right:
        if (transform.position.x < projectileSpawnPoint.position.x)
            rb. velocity = transform.TransformDirection(Vector3.right * _projectileSpeed);
        //if we're facing left:
        else if (transform.position.x > projectileSpawnPoint.position.x)
            rb.velocity = transform.TransformDirection(Vector3.left * _projectileSpeed);
    }

    // This co-routine determines how often the player is able to shoot a projectile.
    IEnumerator ProjectileDelay()
    {
        _canShoot = false;
        yield return new WaitForSeconds(projectileFireRate);
        _canShoot = true;
    }
}
