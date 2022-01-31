using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTree : MonoBehaviour
{
    
    public bool under = false;
    public SpriteRenderer playerLayer;
    private Transform playerTran;
    void Start()
    {
        playerLayer = GetComponentInParent<SpriteRenderer>();
        playerTran = GetComponent<Transform>();
    }
    void Update(){
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.transform.parent.name == "through") Layer(collider.gameObject.GetComponent<Transform>()); 
    }
    
    void Layer(Transform behind){ //判斷目前所在圖層
        if(behind.position.y >= (playerTran.position.y - 0.506f)) playerLayer.sortingOrder = 2;//under = true;
        else playerLayer.sortingOrder = 0;//under = false;
    }
}
