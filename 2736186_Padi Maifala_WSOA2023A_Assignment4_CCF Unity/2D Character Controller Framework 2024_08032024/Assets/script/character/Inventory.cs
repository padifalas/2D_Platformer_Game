using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CLASS DESCRIPTION:
/* 
 * This class defines the types of item the player 
 * can pick up, manages the items already in their inventory,
 * and allows the player to cycle through the items they 
 * currently have.
*/
#endregion

public class Inventory : MonoBehaviour
{
    #region PUBLIC FIELDS:
    [Range(1, 10)]
    public int inventorySize = 4;   // Number of inventory slots.
    public enum ItemType            // Declares an enum containing the types of             
    {                               // item that are available for the player  
        Null,                       // to pick up, including an empty, or "Null" 
        RedKey,                     // type for slots that don't have anything 
        BlueKey,                    // in them currently.
        GreenKey,
        YellowKey,
    }
    
    public string currentInventoryItem;
    // Creates an array for the items to be stored in.
    public ItemType[] inventory;
    #endregion

    #region NON-SERIALIZED PUBLIC FIELDS:
    // Tracks the current item selected in the inventory.
    [System.NonSerialized]
    public ItemType currentItem;
    #endregion

    #region PRIVATE:
    private int _itemCount;
    private KeyCode _cycleInventoryButton;
    #endregion

    void Awake()
    {
        // Set inventory size.
        inventory = new ItemType[inventorySize];
        // Get a keycode from another script.
        _cycleInventoryButton = GetComponent<PlayerMovement>().cycleInventoryButton;
    }

    void Start()
    {
        // Set the current inventory item to the 1st item.
        currentItem = inventory[0];
        currentInventoryItem = currentItem.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(_cycleInventoryButton))
            CycleInventory();
    }

    // This block lets us cycle the current item in our inventory,
    // this could be used to set an active item, for example.
    void CycleInventory()
    {
        if (_itemCount < inventorySize - 1)
        {
            _itemCount++;
            currentItem = inventory[_itemCount];
            currentInventoryItem = currentItem.ToString();
        }
        else
        {
            _itemCount = 0;
            currentItem = inventory[_itemCount];
            currentInventoryItem = currentItem.ToString();
        }
    }
}
