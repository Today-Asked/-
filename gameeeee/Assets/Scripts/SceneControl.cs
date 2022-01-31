using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public GameObject Description, ToolBar, Map, Treasure, Talk;
    public UIInventory uiInventory;
    public Inventory inventory;
    public CanvasGroup ToolBarRender;
    public PlaceChange placeChange;
    public GameObject Clue, Confirm, Instruction; //排球場的提示
    public GameApplication gameApplication;
    
    public void Awake()
    {
        gameApplication = FindObjectOfType<GameApplication>();
        Debug.Log("gameApplication.scene = "+gameApplication.scene);
        CloseDescription();
        ToolBarRender.alpha = 0;
        Map.SetActive(false);
        Confirm.SetActive(false);
        if(gameApplication.scene == "0"){
            placeChange.Here = placeChange.scene.Find(n => n.name == "gate");
        }
        if(gameApplication.scene == "2"){
            Treasure.SetActive(true);
            Talk.SetActive(false);
            Instruction.SetActive(false);
            placeChange.Here = placeChange.scene.Find(n => n.name == "center");
            Map.SetActive(true);
            Clue.SetActive(true);
            
        }
    }
    void Start(){
        if(gameApplication.scene == "2"){
            inventory.AddItem("Clue1");
            gameApplication.scene = "explore";
        }
    }
    public void CloseDescription(){
        
        Description.SetActive(false);
        uiInventory.Reset();
    }
    public void ToolBarManipulation(){
        if(Map.activeSelf) MapManipulation();
        if(ToolBarRender.alpha == 1){
            ToolBarRender.alpha = 0;
            ToolBarRender.interactable = false;
        }else{
            ToolBarRender.alpha = 1;
            ToolBarRender.interactable = true;
        }
    }
    public void OnStartGame(string SceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
    public void MapManipulation(){
        if(ToolBarRender.alpha == 1) ToolBarManipulation();
        if(Map.activeSelf) Map.SetActive(false);
        else Map.SetActive(true);
    }
    public void ConfirmManipulation(){
        if(Confirm.activeSelf) Confirm.SetActive(false);
        else Confirm.SetActive(true);
    }
    void Update()
    {
        /*if(Input.GetKeyDown("k")){
            gameApplication.scene = "2";
            OnStartGame("explore");
        }*/    
    }
        
    
    
}
