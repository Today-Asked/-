using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [Header("水平速度")]
    public float Speed;//是可以改的變數!
    
    public Animator PlayerAni; //設定一個animator供控制
    public SpriteRenderer walk;//用來控制左右翻轉
    public Rigidbody2D PlayerRigid;
    public GameObject talk;
    public GameApplication gameApplication;
    public Inventory inventory;

    string ho = "Horizontal";
    string ve = "Vertical";
    //有點懶 而且怕拼錯:D
    bool treasure = false;
    void Awake(){
        gameApplication = FindObjectOfType<GameApplication>();
        if(gameApplication.scene == "2") treasure = true;
    }
    void Start()//設定初始狀態
    {
        PlayerRigid = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()//每次畫面刷新時更新一次
    {

        walking(); //動畫
        
        float h = Input.GetAxisRaw(ho);//得知水平方向，值會介於1~-1之間
        float v = Input.GetAxisRaw(ve);//垂直方向

        LiliMoving(h, v); //移動

    }

    //使用rigidbody的velocity控制，與單純改變物件的transform.position比起來，較不會有不自然的抖動行為。
    void LiliMoving(float h, float v){
        PlayerRigid.velocity = new Vector2(Speed * h, Speed * v);
        //不要用Transform 他會一直抖
    }
    /*public void Moving(GameObject gameObject, float h, float v)//舊的函式
    {
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * h, Speed * v);
        float moveX = h * Speed * 0.005f; //移動距離
        float moveY = v * Speed * 0.005f;
        Vector2 newposition = new Vector2(gameObject.transform.position.x + moveX, gameObject.transform.position.y + moveY); 
        gameObject.transform.position = newposition;
    }*/

    //得知玩家按下的鍵，並使用變數status改變動畫的狀態。
    void walking(){

        int IsWalking = 0; //C#不能允許變數未賦予值的存在:(
        
        if(Input.GetKey(KeyCode.DownArrow)){ 
            IsWalking = 3;
            PlayerAni.SetInteger("Status", 3);
            
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            IsWalking = 2;
            PlayerAni.SetInteger("Status", 2);
            
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            if(walk.flipX == true)
                walk.flipX = false;
            IsWalking = 1;
            PlayerAni.SetInteger("Status", 1);
            
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            if(walk.flipX == false)
                walk.flipX = true;
            IsWalking = 1;
            PlayerAni.SetInteger("Status", 1);
            
        }
        
        if(IsWalking > 0){
            if(IsWalking == 1){
                if (PlayerAni.GetInteger("Status") < 0)
                    PlayerAni.SetInteger("Status", 1);
            }
            if (IsWalking == 2){
                if (PlayerAni.GetInteger("Status") < 0)
                PlayerAni.SetInteger("Status", 2);
            }
            if (IsWalking == 3){
                if (PlayerAni.GetInteger("Status") < 0)
                    PlayerAni.SetInteger("Status", 3);
            }
        }else{
            if(PlayerAni.GetInteger("Status") == 1)
                PlayerAni.SetInteger("Status", -1); 
            if(PlayerAni.GetInteger("Status") == 2)
                PlayerAni.SetInteger("Status", -2);
            if(PlayerAni.GetInteger("Status") == 3 )
                PlayerAni.SetInteger("Status", -3);
        }
    }

    public static bool collided = false;
    public static string words = "n";
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.transform.parent.name);
        //說明
            if(collision.gameObject.transform.parent.name == "message" || collision.gameObject.transform.parent.name == "cantGo"){
                if(!treasure){
                    talk.GetComponent<talk>().Talking(collision.gameObject); 
                    collided = true;
                }
                
            }
        
        //尋寶
        if(collision.gameObject.tag == "treasure"){
            words = collision.gameObject.name;
            inventory.AddItem(gameObject.name);
            Destroy(collision.gameObject);
            collided = true;
        }
        
        PlayerRigid.velocity = new Vector2(0, 0);
        //Debug.Log("撞到" +  collision.gameObject.name + "了");
        /*if (collision.gameObject.tag == "CantGo"){
            words = '1';
            }
        if (collision.gameObject.tag == "room"){
            words = '1';
            }
        if (collision.gameObject.tag == "list1"){
            words = 'a';
            }
        if (collision.gameObject.tag == "list2"){
            words = 'b';
            }
        if (collision.gameObject.tag == "list3"){
            words = 'c';
            }
        if (collision.gameObject.tag == "list4"){
            words = 'd';
            }
            */
    }

    
}