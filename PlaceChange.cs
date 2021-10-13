using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChange : MonoBehaviour
{        
    public GameObject Here, ChangePlace, Lili, Map, underTree;
    public SceneControl sceneControl;
    public List<GameObject> scene = new List<GameObject>();
    public Transform ParentTransform;
    public Animation FadeAni;
    public move move;
    public bool OnTrigger = false;
    public void Awake(){
        foreach(Transform go in ParentTransform){
            scene.Add(go.gameObject);
            go.gameObject.SetActive(false);
        }
    }
    public void Start(){
        Here.SetActive(true);
        if(Here.name != "gate") Lili.transform.position = new Vector2(0, -2);
        else Lili.transform.position = new Vector2(2, 0);
    }
    public void Update(){
        if(OnTrigger == true){
                        
            OnTrigger = false;
            FadeAni.Play("淡出");
            FadeAni.Play("淡入");
            Invoke("enableMove", 1.5f);
        }
    }
    public void enableMove(){
        move.enabled = true;
    }
    
    public void OnTriggerEnter2D(Collider2D collider)
    {        
        
        if(collider.gameObject.transform.parent.name == "transform") ChangeScene(collider.gameObject);
        if(collider.gameObject.name == "scene1"){
            sceneControl.OnStartGame("scene1");
        }
        if(Here.name == "banyan" || Here.name == "garden")  underTree.SetActive(true);
        else underTree.SetActive(false);
    }
    public void ChangeScene(GameObject gameObject){
        
        ChangePlace = scene.Find(x => x.name == gameObject.name);
        Debug.Log(ChangePlace); 
        Vector2 position = new Vector2(0, 0);       
        if(ChangePlace != Here){
            move.enabled = false;
            move.PlayerRigid.velocity = new Vector2(0, 0);            
            OnTrigger = true;
            move.Speed = 5;
            
            if(ChangePlace.name == "1F" || ChangePlace.name == "2F"){
                move.Speed = 2.5f;
                if(gameObject.tag == "front") position = new Vector2(23.65f, -0.8f); //前面的右邊
                if(gameObject.tag == "back") position = new Vector2(23.65f, 4.71f);
                if(ChangePlace.name == "1F")
                    if(gameObject.tag == "aisle") position = new Vector2(0, 0); //玄關 我只是不想再設一個標籤
                if(gameObject.tag == "left") position = new Vector2(6.2f, -0.7f); //前面的左邊
            }
            if(ChangePlace.name == "center") position = new Vector2(0, -2);
            if(ChangePlace.name == "banyan") position = new Vector2(0, -2.5f);
            if(ChangePlace.name == "gate"){
                if(gameObject.tag == "right") position = new Vector2(11, 0);
                else position = new Vector2(2, 0);
            }
            if(ChangePlace.name == "VolleyballCourt") position = new Vector2(-4, -6);
            if(ChangePlace.name == "garden") position = new Vector2(0, -4);
            Lili.transform.position = position;          
            Here.SetActive(false);
            ChangePlace.SetActive(true);
            Here = ChangePlace;
            if(Map.activeSelf) Map.SetActive(false);
        }
    }
}
