using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propColliding : MonoBehaviour
{
    public talk talk;
    public Inventory inventory;
    
    //那道具身上都要裝tag: "prop"
    public void OnTriggerEnter2D(Collider2D other)
    {
        inventory.AddItem(gameObject.name);
        talk.Talking(this.gameObject);
        Destroy(this.gameObject);
    }
}
