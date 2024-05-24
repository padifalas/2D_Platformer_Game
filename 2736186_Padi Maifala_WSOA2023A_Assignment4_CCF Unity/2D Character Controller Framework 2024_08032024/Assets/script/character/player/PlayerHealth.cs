using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class manages the player's health and also has a public field that allows you to control
 * how long the player will be invincible for after being hit by an enemy.
*/
#endregion

public class PlayerHealth : MonoBehaviour
{
    #region PUBLIC FEILDS:
    [Header("HEALTH:")]
    public int health = 10;
    public string currentHealth;
    public float invincibilityTime = 0.5f;
    [Header("DEATH:")]
    public KeyCode autoDeathButton;
    #endregion

    [System.NonSerialized]
    public bool invincible;

    private LevelManager _lvlMngr;
    private GameObject _playerSprite;
    private int _currentHealth;

    void Awake()
    {
        _lvlMngr = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        _currentHealth = health;
        currentHealth = _currentHealth.ToString();
        _playerSprite = transform.Find("Player_GFX/Player_sprite").gameObject;
    }

    void Update()
    {
        // Allows you to kill the player using a key for debugging and testing purposes.
        if (Input.GetKeyDown(autoDeathButton))
            Die();
    }

    //Controls how the player looses health.
    public void LooseHealth(int healthToDeduct)
    {
        if (!invincible)
        {
            // Sets health based on a value passed from another script (usuallu the enemy).
            _currentHealth -= healthToDeduct;
            currentHealth = _currentHealth.ToString();

            // Makes us invincible for a short period after being hit.
            if (_currentHealth > 0)
            {
                StartCoroutine(Invincibility());
                Debug.Log("Invincible :)");
            }
            // Kills the player if their health reaches zero.
            else 
            {
                Die();
            }
        }        
    }

    // This function allows us to gain health from pickups.
    public void GainHealth(int healthToGain)
    {
        _currentHealth += healthToGain;

        if (_currentHealth > health)
            _currentHealth = health;

        currentHealth = _currentHealth.ToString();
    }

    // Kills the player and reloads the current scene.
    public void Die()
    {
        _lvlMngr.ReloadScene(_lvlMngr.currentScene);
    }

    // The timer for the invincibility period. You could play around with this and the PowerUp script
    // to create a powerup that makes you invincible...
    IEnumerator Invincibility()
    {
        invincible = true;

        Color col = _playerSprite.GetComponent<SpriteRenderer>().color;
        col.a = 0.5f;
        _playerSprite.GetComponent<SpriteRenderer>().color = col;

        yield return new WaitForSeconds(invincibilityTime);
        
        invincible = false;
        col.a = 1f;
        _playerSprite.GetComponent<SpriteRenderer>().color = col;
        Debug.Log("Not Invincible :(");
    }
}
