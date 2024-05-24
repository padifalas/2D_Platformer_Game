using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region CLASS DESCRIPTION:
/* 
 * This script allows the enemy to do damage to the player 
 * and reduce their health by a set amount.
*/
#endregion

public class DamagePlayer : MonoBehaviour
{
    public int damageToPlayer = 1;

    private LayerMask _playerLayer;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private PlayerMovement _playerMovement;
    private EnemyMovement _enemyMovement;

    void Awake()
    {
        // Get references.
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerLayer = LayerMask.GetMask("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }


    // Check to see if we're colliding with the player and if so, call the 
    // 'KnockBack()' and 'LooseHealth()' functions on the player object.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            //Debug.Log("Player Contact");
            // If the Player hits the Enemy from the side:
            if (Physics2D.Raycast
                (_enemyMovement._leftMid, Vector3.left, Mathf.Infinity, _playerLayer) ||
                Physics2D.Raycast
                (_enemyMovement._rightMid, Vector3.right, Mathf.Infinity, _playerLayer))
            {
                _playerMovement.KnockBack(transform);
                _playerHealth.LooseHealth(damageToPlayer);
            }
            // If the Player hits the Enemy from the bottom:
            else if (_enemyMovement.bouncingOn && (Physics2D.Raycast
                    (_enemyMovement._bottomLeft, Vector3.down, Mathf.Infinity, _playerLayer) ||
                    Physics2D.Raycast
                    (_enemyMovement._bottomMid, Vector3.down, Mathf.Infinity, _playerLayer) ||
                    Physics2D.Raycast
                    (_enemyMovement._bottomRight, Vector3.down, Mathf.Infinity, _playerLayer)))
            {
                //Debug.Log("Player Contact bottom");
                _playerMovement.KnockBack(transform);
                _playerHealth.LooseHealth(damageToPlayer);
                _enemyMovement.Bounce();
            }
        }
    }
}
