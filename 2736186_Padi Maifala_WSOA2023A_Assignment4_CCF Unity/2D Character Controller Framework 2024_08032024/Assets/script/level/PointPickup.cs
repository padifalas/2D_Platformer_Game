using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class can be used to create a generic coin type pickup that
 * adds a set amount of points to the player's score when they 
 * collide with it.
*/
#endregion

public class PointPickup : MonoBehaviour
{
    [Header("SCORE:")]
    public int scoreToAdd = 1;
    [Header("EFFECTS:")]
    public GameObject vfx;
    public AudioSource sfx;

    private GameObject _player;
    private LevelManager _lvlMngr;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lvlMngr = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if it's the player, if it is add to the score,
        // and instatiate the effects for this pickup.
        if (col.gameObject == _player)
        {
            _lvlMngr.AddPointsToScore(scoreToAdd);

            // Instantiate effects if there are any.
            if (vfx != null)
                Instantiate(vfx, this.transform.position, Quaternion.identity);
            if (sfx != null)
                Instantiate(sfx, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
