using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour
{
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent< Animation >();
    }
    public int CanChangeScene = 0;
    
    void Update()
    {
        if (CanChangeScene == 1)
            anim.Play("淡出");
    }
    string map;
    void Loadmap(string map){
        UnityEngine.SceneManagement.SceneManager.LoadScene(map);//轉場
    }
    
}


//GameObject.GetComponent<Animation>().Play("淡出");
//   Application.LoadLevel( "black" );//轉換場景Application.LoadLevel( "場景名稱 )
/*  if (Input.GetKeyDown(KeyCode.R)){
Application.LoadLevel( Application.loadedLevel );
}
if (Input.GetKeyDown(KeyCode.M)){
if (Application.loadedLevel==0){Application.LoadLevel(1);}
else {Application.LoadLevel(0);}
}*/