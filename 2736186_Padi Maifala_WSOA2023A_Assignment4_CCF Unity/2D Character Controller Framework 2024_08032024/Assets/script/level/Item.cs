using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class is placed on items that can be collected by the player, so that
 * when the player collides with them the item is added to the player's inventory.
 * 
 * NOTES: The object that this script is attached to should have a 2D collider on it
 * with the 'Is Trigger' flag on the component set to TRUE. 
*/
#endregion

public class Item : MonoBehaviour
{
    [Header("ITEM:")]
    public Inventory.ItemType itemToGive;
    [Header("EFFECTS:")]
    public GameObject vfx; 
    public AudioSource sfx;

    private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if the object we're colliding with is the player, 
        // and if we have remebered to set the type of item!
        if(col.gameObject == _player && itemToGive != Inventory.ItemType.Null)
        {
            //Get a reference to the player's inventory.
            Inventory inv = _player.GetComponent<Inventory>();

            // Run a loop for each slot in the inventory, if the slot is empty, 
            // fill it with this item, otherwise carry on going until we run out of slots.
            int count = 0;
            foreach(Inventory.ItemType item in inv.inventory)
            {
                if (item == Inventory.ItemType.Null)
                {
                    inv.inventory[count] = itemToGive;
                    if (_player.GetComponent<Inventory>().currentItem == Inventory.ItemType.Null)
                        _player.GetComponent<Inventory>().currentItem = itemToGive;
                    if (vfx != null)
                        Instantiate(vfx, transform.position, Quaternion.identity);
                    if (sfx != null)
                        Instantiate(sfx, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;
                }
                count++;
            }
        }
    }
}
