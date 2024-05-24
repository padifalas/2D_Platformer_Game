using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class is meant to be attached to a big trigger volume placed
 * underneath the level. It makes sure that anything that falls 
 * off the level and touches it, gets destroyed. If you place the 
 * 'Player' object in the 'playerOverride' field, it will reload 
 * the current scene when the player touches the killbox, instead of
 * destroying the player.
*/
#endregion

public class KillBox : MonoBehaviour
{
    public GameObject playerOverride;

    private LevelManager _lvlMngr;

    void Awake()
    {
        // Getting a reference to the 'LevelManager' script that stores the name of the current scene.
        _lvlMngr = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the 'playerOverride' field has been set, reset the level 
        // when the player touches the killbox.
        if (playerOverride != null && col.tag == playerOverride.tag)
            _lvlMngr.ReloadScene(_lvlMngr.currentScene);
        // Otherwise, destroy the object.
        else
            Destroy(col.gameObject);
    }
}
