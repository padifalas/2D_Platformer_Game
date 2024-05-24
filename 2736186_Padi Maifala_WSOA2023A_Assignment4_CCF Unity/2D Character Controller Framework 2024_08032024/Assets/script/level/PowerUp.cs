using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class is attached to objects that act as powerups. It is a generic powerup which
 * allows you to select whether to activate a power on the Player such as double-jumping or gliding,
 * or to add health to the Player. 
 * 
 * NOTES: If you want to add your own powers, apart from having to code them in the 'PlayerMovement' class, 
 * you'll also need to add them to the PowerUpType enum below if you'd like to use the powerup to activate them.
 * 
 * Also note that powerups should have their BoxCollider2D component set to 'Is Trigger'.
*/
#endregion

public class PowerUp : MonoBehaviour
{
    // How much health will be added if this powerup is set to be a health powerup. 
    // If it's set to something else this field does nothing!
    public int healthToGive = 1;

    public enum PowerUpType
    { 
        DoubleJump,
        WallJump,
        Glide,
        Shoot,
        Health,
    }
    [Header("POWERUP:")]
    public PowerUpType powerUpType;

    [Header("EFFECTS:")]
    public GameObject vfx;
    public AudioSource sfx;

    private GameObject _player;

    void Awake()
    {
        // Get a reference to the player.
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if we're colliding with the player.
        if (col.gameObject == _player)
        {
            // Get a reference to the 'PlayerMovement' script.
            PlayerMovement _playerMovement = _player.GetComponent<PlayerMovement>();

            // Activate a power on the player based on what type of powerup this is.
            if (powerUpType == PowerUpType.DoubleJump)
            {
                _playerMovement.doubleJumpOn = true;
            }
            else if (powerUpType == PowerUpType.WallJump)
            {
                _playerMovement.wallJumpOn = true;
            }
            else if (powerUpType == PowerUpType.Glide)
            {
                _playerMovement.glideOn = true;
            }
            else if (powerUpType == PowerUpType.Shoot)
            {
                _playerMovement.shootOn = true;
            }
            else if (powerUpType == PowerUpType.Health)
            {
                PlayerHealth _playerHealth = _player.GetComponent<PlayerHealth>();
                _playerHealth.GainHealth(healthToGive);
            }

            // Run some effects if available.
            if (vfx != null)
                Instantiate(vfx, transform.position, Quaternion.identity);
            if (sfx != null)
                Instantiate(sfx, transform.position, Quaternion.identity);
            // Clean up.
            Destroy(gameObject);
        }
    }
}
