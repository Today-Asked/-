using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    void Start(){
    }
    public void AddItem(string Name){
        Item itemToAdd = itemDatabase.GetItem(Name);
        if(itemToAdd != null){
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item " + itemToAdd.Title + " in Inventory");
        }else{
            Debug.Log("failed");
        }
    }
    public void RemoveItem(string Name){
        Item itemToRemove = itemDatabase.GetItem(Name);
        if(itemToRemove != null){
            characterItems.Remove(itemToRemove);
            Debug.Log("Removed " + itemToRemove.Name);
            inventoryUI.Remove(itemToRemove);
            inventoryUI.Reset();
        }
        else Debug.Log("Item not exist");
    }

}
