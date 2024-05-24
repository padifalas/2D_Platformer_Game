using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class destroys a game object that it's attached after a set
 * length of time.
*/
#endregion

public class WaitDestroy : MonoBehaviour
{
    [Header("TIMING:")]
    public bool autoStart;
    public float waitTime = 1.0f;

    void Start()
    {
        if(autoStart)
            StartCoroutine(WaitTimer());
    }

    // Since we can't start co-routines directly from external scripts,
    // this function allows us to do so, since public functions can be
    // called from other scripts. 
    public void StartWaitTimer()
    {
        StartCoroutine(WaitTimer());
    }

    // Waits some time and then destroys the gameObject.
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
