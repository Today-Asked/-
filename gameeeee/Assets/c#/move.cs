using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [Header("水平速度")]
    public float SpeedX;//是可以改的變數!

    [Header("垂直速度")]
    public float SpeedY;
    
    public Animator PlayerAni; //設定一個animator供控制
    public SpriteRenderer walk;//用來控制左右翻轉

    public float minX, maxX, minY, maxY;
    //房間：minX = -11, maxX = 11.5, minY = 4.25, maxY = -5.53

    string ho = "Horizontal";
    string ve = "Vertical";
    //有點懶 而且怕拼錯:D

    
    void Start()//設定初始狀態
    {
        
    }
    
    void Update()//每次畫面刷新時更新一次
    {
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
        /*else{
            switch (IsWalking)
            {
                case 1: 
                    IsWalking = -1; break;
                case 2: 
                    IsWalking = -2; break;
                case 3: 
                    IsWalking = -3; break;
            }
        }*/
        if(IsWalking > 0){
            if(IsWalking == 1){
                if (PlayerAni.GetInteger("Status") < 0){
                    PlayerAni.SetInteger("Status", 1);
                }
            }
            if (IsWalking == 2){
                if (PlayerAni.GetInteger("Status") < 0){
                    PlayerAni.SetInteger("Status", 2);
                }
            }
            if (IsWalking == 3){
                if (PlayerAni.GetInteger("Status") < 0){
                    PlayerAni.SetInteger("Status", 3);
                }
            }
        }else{
            if(PlayerAni.GetInteger("Status") == 1 || PlayerAni.GetInteger("Status") == -1)
                PlayerAni.SetInteger("Status", -1); 

            if(PlayerAni.GetInteger("Status") == 2 || PlayerAni.GetInteger("Status") == -2)
                PlayerAni.SetInteger("Status", -2);

            if(PlayerAni.GetInteger("Status") == 3 || PlayerAni.GetInteger("Status") == -3)
                PlayerAni.SetInteger("Status", -3);
            
            /*if(IsWalking == -1){
                if (PlayerAni.GetInteger("Status") > 0){
                    PlayerAni.SetInteger("Status", -1);
                }
            }
            if(IsWalking == -2){
                if (PlayerAni.GetInteger("Status") > 0){
                    PlayerAni.SetInteger("Status", -2);
                }
            }
            if(IsWalking == -3){
                if (PlayerAni.GetInteger("Status") > 0){
                    PlayerAni.SetInteger("Status", -3);
                }
            }*/
            
        }
        float h = Input.GetAxisRaw(ho);//得知水平方向，值會介於1~-1之間
        float v = Input.GetAxisRaw(ve);//垂直方向
        /*PlayerAni.SetFloat("MoveX", h);
        PlayerAni.SetFloat("MoveY", v);*/
        Move(h, v);
        
    }

    void Move(float h, float v)
    {
        float moveX = h * SpeedX * Time.deltaTime; //移動距離
        float moveY = v * SpeedY * Time.deltaTime;

        Vector2 newposition = new Vector2(Mathf.Clamp(transform.position.x + moveX, minX, maxX), Mathf.Clamp(transform.position.y + moveY, minY, maxY)); 
        //vector: 一個二維向量單位， 儲存角色的新位置

        transform.position = newposition;

    }
}
