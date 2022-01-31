using UnityEngine;
public class GameApplication : MonoBehaviour
{
    /*快捷鍵
        a => 跳到第二幕
        b => check the value of Gameapplication.scene
    */
   
   static GameApplication instance;
   private void Awake()
   {
      if (instance == null)
      {
          instance = this;
          DontDestroyOnLoad(this);
      }else if(this != instance){
          Destroy(gameObject);
      }
   }
   public string scene = "";
   void Update()
   {
       if(Input.GetKeyDown("escape")) Application.Quit();
        /*string PresentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
       if(PresentScene == "scene0") scene = "0";
       if(PresentScene == "scene1") scene = "1";
       if(PresentScene == "scene2") scene = "2";*/
       if(Input.GetKeyDown("a")) OnStartGame("scene2");
       if(Input.GetKeyDown("b")) Debug.Log(scene);
   }
   public void OnStartGame(string SceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
   

}