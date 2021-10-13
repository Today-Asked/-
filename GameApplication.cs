using UnityEngine;
public class GameApplication : MonoBehaviour
{
   
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
   }
   public void OnStartGame(string SceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
   

}