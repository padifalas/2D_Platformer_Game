using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class controls the enemy's health. It also defines what type of enemy this is,
 * destroys the enemy when it hits zero health and adds points to the player's score value.
*/
#endregion

public class EnemyHealth : MonoBehaviour
{
    #region PUBLIC FIELDS:
    public enum enemyType
    {
        walking,
        jumping,
        shooting,
    }
    public enemyType typeOfEnemy;
    [Header("HEALTH:")]
    public bool stompable = true;
    public int health = 1;
    public string currentHealth;
    [Header("EFFECTS:")]
    public GameObject vfx;
    public AudioSource sfx;
    #endregion

    #region NON-SERIALIZED PUBLIC FIELDS:
    [System.NonSerialized]
    public int _currentHealth;
    #endregion

    #region PRIVATE:
    private GameObject _player;
    private LayerMask _playerLayer;
    private int _playerProjectileDamage;
    private LevelManager _lvlMngr;
    private EnemyMovement _enemyMovement;
    private int _pointsValue;
    #endregion

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerLayer = LayerMask.GetMask("Player");
        _playerProjectileDamage = _player.GetComponent<PlayerShoot>().projectileDamage;

        // Sets the points value of the enemy based on values set in the LevelManager class.
        _lvlMngr = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        if (typeOfEnemy == enemyType.walking)
            _pointsValue = _lvlMngr.walkingEnemyPointsValue;
        else if (typeOfEnemy == enemyType.jumping)
            _pointsValue = _lvlMngr.jumpingEnemyPointsValue;
        else if (typeOfEnemy == enemyType.shooting)
            _pointsValue = _lvlMngr.shootingEnemyPointsValue;
        else
            _pointsValue = 0;

        _enemyMovement = GetComponent<EnemyMovement>();
        _currentHealth = health;
        currentHealth = _currentHealth.ToString();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // This is branch statement controls the player's stomp behaviour.
        if (col.gameObject == _player && stompable)
        {
            // Casts some rays to check if the player is above us. We could also use the 
            // CharacterController2D's 'contact_above' flag, but this gives us a bit more
            // control and potential extensability.
            if (Physics2D.Raycast
                (_enemyMovement._topLeft, Vector3.up, 0.2f, _playerLayer) ||
                Physics2D.Raycast
                (_enemyMovement._topMid, Vector3.up, 0.2f, _playerLayer) ||
                Physics2D.Raycast
                (_enemyMovement._topRight, Vector3.up, 0.2f, _playerLayer))
            {
                // Makes the player bounce after stomping the enemy.
                _player.GetComponent<PlayerMovement>().Bounce();
                
                // Deducts health from the stomp based on values set in the LevelManager class.
                if (typeOfEnemy == enemyType.walking)
                    LooseHealth(_lvlMngr.walkingEnemyDamage);
                else if (typeOfEnemy == enemyType.jumping)
                    LooseHealth(_lvlMngr.jumpingEnemyDamage);
                else if (typeOfEnemy == enemyType.shooting)
                    LooseHealth(_lvlMngr.shootingEnemyDamage);
                else
                    LooseHealth(1);
            }
        }

        // Calls the looseHealth function if the enemy is hit by one of the player's projectiles.
        if (col.gameObject.CompareTag("Projectile_player"))
        {
            LooseHealth( _playerProjectileDamage);
        }
    }

    // This one is pretty self explanatory.
    public void LooseHealth(int damage)
    {
        _currentHealth -= damage;
        currentHealth = _currentHealth.ToString();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // So's this one ;)
    void Die()
    {
        if(_pointsValue > 0)
            _lvlMngr.AddPointsToScore(_pointsValue);

        if (vfx != null)
            Instantiate(vfx, transform.position, Quaternion.identity);
        if (sfx != null)
            Instantiate(sfx, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
