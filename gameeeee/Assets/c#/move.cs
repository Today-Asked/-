using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [Header("水平速度")]
    public float SpeedX;//是可以改的變數!

    [Header("垂直速度")]
    public float SpeedY;
    
    string ho = "Horizontal";
    string ve = "Vertical";
    //有點懶 而且怕拼錯:D

    void Start()//設定初始狀態
    {
        
    }
    

    void Update()//每次畫面刷新時更新一次
    {
        float h = Input.GetAxisRaw(ho);//得知水平方向，值會介於1~-1之間
        float v = Input.GetAxisRaw(ve);//垂直方向
        Move(h, v);
    }

    void Move(float h, float v)
    {
        float moveX = h * SpeedX * Time.deltaTime; //移動距離
        float moveY = v * SpeedY * Time.deltaTime;

        Vector2 newposition = new Vector2(transform.position.x + moveX, transform.position.y + moveY); //vector: 一個二維向量單位， 儲存角色的新位置

        transform.position = newposition;

    }
}
