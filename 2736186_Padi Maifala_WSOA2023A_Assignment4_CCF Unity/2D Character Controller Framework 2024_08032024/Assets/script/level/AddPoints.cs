using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class can be attached to an object to make it give 
 * the player points when they collide with it. You can use it  
 * to make simple pickups like coins and stuff. 
*/
#endregion

public class AddPoints : MonoBehaviour
{
    public int scoreToAdd = 1;
    public GameObject vfx;
    public AudioSource sfx;
    public bool destroyGameObject = false;
    
    private LevelManager _lvlMngr;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lvlMngr = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    // When another object collides with this one, 
    // check to see if it's the player, and if it is, 
    // add a set number of points to the total score 
    // (which is stored in the 'LevelManager' class).
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            _lvlMngr.AddPointsToScore(scoreToAdd);

            if (!destroyGameObject)
                return;

            // Instantiate effects if there are any.
            if (vfx != null)
                Instantiate(vfx, transform.position, Quaternion.identity);
            if (sfx != null)
                Instantiate(sfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
