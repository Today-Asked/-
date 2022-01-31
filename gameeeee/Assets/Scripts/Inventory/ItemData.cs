using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name;
    public string Title;
    public string Description;
    public Sprite Icon;

    public Item(string Name, string Title, string Description){
        this.Name = Name;
        this.Title = Title;
        this.Description = Description;
        this.Icon = Resources.Load<Sprite>("Inventory/Props/" + Name);
    }
}
