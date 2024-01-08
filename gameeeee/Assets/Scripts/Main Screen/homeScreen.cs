using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class homeScreen : MonoBehaviour
{
    private GameObject Menu;
    public GameObject finish;
    public GameApplication application;
    public changeScene changeScene;
    public chooseNo chooseNo;
    // Start is called before the first frame update
    void Start()
    {
        application = FindObjectOfType<GameApplication>();
        Menu = GameObject.Find("announcement");
        Menu.SetActive(false);
        if(application.scene == "2") finish.SetActive(true);
        else finish.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void end(){
        finish.SetActive(true);
    }
    public void CloseEnd(){
        finish.SetActive(false);
    }
    public void gameStart(){
        changeScene.CanChangeScene = 1; //切換到scene 0
        chooseNo.panalNo = 1; //

    }
    public void announcement(){
        Menu.SetActive(true);
    }
    public void closeAnnouncement(){
        Menu.SetActive(false);
    }
    public void site(){
        Application.OpenURL("https://forms.gle/Rcyn2o2eMoqLR4H4A");
    }
}
