using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region CLASS DESCRIPTION:
/* 
 * This class stores level related variables and the player's score. It also stores data and  
 * runs functions related to checkpoints.
*/
#endregion

// USE FOR OBJECT POOLING!!!
public class LevelManager : MonoBehaviour
{
    #region INITIALIZATIONS:
    [Header("INITIALIZATIONS:")]
    public AudioSource pointSFX;
    #endregion

    #region PUBLIC FIELDS:
    [Header("TRACKING:")]
    public string currentScene;
    public int score;
    [Header("ENEMY STATS:")]
    public int walkingEnemyDamage = 1;
    public int walkingEnemyPointsValue;
    public int jumpingEnemyDamage = 1;
    public int jumpingEnemyPointsValue;
    public int shootingEnemyDamage = 1;
    public int shootingEnemyPointsValue;
    #endregion

    #region CHECKPOINTS:
    [Header("PREFAB REFERENCES:")]
    [Tooltip("Array of Prefab types for the items you want to respawn "
                + "when the player dies after having reached a checkpoint. "
                + "Note that this field only reads the object's tag, "
                + "so if two objects have the same tag, you don't "
                + "need to drag both of them into the array.")]
    public GameObject[] checkpointRespawnTags;
    #endregion

    #region NON-SERIALIZED PUBLIC FIELDS:
    [System.NonSerialized]
    public GameObject currentCheckPoint;
    [System.NonSerialized]
    public int scoreAtLastCheckPoint;
    [System.NonSerialized]
    public Transform transformAtLastCheckPoint;
    #endregion

    #region PRIVATE:
    private GameObject _player;
    private Dictionary<int, GameObject[]> _respawns;
    #endregion

    void Awake()
    {
        // Get references.
        _player = GameObject.FindGameObjectWithTag("Player");
        transformAtLastCheckPoint = _player.transform;
        scoreAtLastCheckPoint = score;
        currentScene = SceneManager.GetActiveScene().name;

        // Update the checkpoint system.
        _respawns = new Dictionary<int, GameObject[]>();
        UpdateRespawns();
    }

    // Called by other scripts, adds points to the player's score.
    public void AddPointsToScore(int points)
    {
        score += points;

        if(pointSFX != null)
            Instantiate(pointSFX, _player.transform.position, Quaternion.identity);
    }

    // Reloads the current scene.
    public void ReloadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Updates the checkpoit system.
    public void UpdateRespawns()
    {
        // This loop finds all the gameObjects of a specific type (coins, powerups, etc. Defined by tag)
        // and stores their positions in a dictionary so that they can be respawned when the player dies.
        for (var x = 0; x < checkpointRespawnTags.Length; x++)
        {
            string tag = checkpointRespawnTags[x].tag;
            _respawns[x] = GameObject.FindGameObjectsWithTag(tag);
        }
        //Debug.Log("1st Array of respawn's length: "  + _respawns[0].Length);
    }
}

// Stuff to do: manager now records relevant values whenever the player passes a checkpoint.
// Need to do respawning stuff, re-instantiate all the objects in the collectibles arrays.
// Consider changing the arrays from GameObject to pos transforms to save RAM
