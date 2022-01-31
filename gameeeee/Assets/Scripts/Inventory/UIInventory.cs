using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject prop;
    public int AmountOfSlots = 10;
    public string SelectedItem = null;

    private void Awake(){
        for(int i = 0; i < AmountOfSlots; i++){ 
            prop = GameObject.Find("Canvas/Tool Column/Scroll View/Viewport/Content/Item (" + i + ")");
            //prop.transform.SetParent(Content);
            uiItems.Add(prop.GetComponentInChildren<UIItem>());
        }
        /*for(int i = 0; i < AmountOfSlots; i++){
            GameObject prop = GameObject.Find("Canvas/Tool Column/Scroll View/Viewport/Content/Item (" + i + ")");
            if(uiItems[i].item == null) Debug.Log("uiItem " + i + "'s item is null");
        }*/
    }
    public void UpdateSlot(int slot, Item item){
        if(slot != -1) uiItems[slot].UpdateItem(item);
            
    }
    public void AddNewItem(Item item){
        //List<T>.FindIndex:搜尋符合條件的項目，並傳回 List<T> 內第一次出現或為其一部分之以零為起始的索引。如果找不到則傳回 -1。
        int UIitemToAdd = uiItems.FindIndex(i => i.item == null);
        if(UIitemToAdd != -1){
            UpdateSlot(UIitemToAdd, item);
            Debug.Log("Added item " + item.Title + " in UI Inventory");
        }
        else Debug.Log("null item not found");
        //if (UIitemToAdd != -1) 
    }
    public void Remove(Item item){
        int itemIndex = uiItems.FindIndex(i => i.item == item); //要被移除的道具是幾號
        int amount = uiItems.FindIndex(i => i.item == null); //現在道具欄中的道具數量
        for(int i = itemIndex; i < amount; i++){
            Item itemToUpdate = uiItems[i+1].item;
            UpdateSlot(i, itemToUpdate);
        }
    }
    public void Select(Item item){        
        SelectedItem = item.Name;
        int amount = uiItems.FindIndex(i => i.item == null);
        Debug.Log(SelectedItem + " is being chosen!");
        for(int i = 0; i < amount; i++){
            uiItems[i].Refresh(uiItems[i].item);
        }
    }
    public void Reset(){
        SelectedItem = null;
        int amount = uiItems.FindIndex(i => i.item == null);
        for(int i = 0; i < amount; i++){
            uiItems[i].Reset();
        }
    }
}
