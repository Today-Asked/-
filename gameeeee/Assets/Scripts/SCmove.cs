using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCmove : MonoBehaviour
{
    public Animator Ani;
    public SpriteRenderer Sprite;
    public move move;
    bool walk = false;
    float distanceX, distanceY, moveX, moveY;
    Vector2 OriPos;
    void Start(){
        Ani = gameObject.GetComponent<Animator>();
        Sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Update(){
        if(Input.GetKeyDown("x")){
            Walk(1, -5);
            
        }
        if(Input.GetKeyDown("y")){
            Walk(-1, 5);
        }
        if(walk == true){
            if(abs(gameObject.transform.position.x - OriPos.x) < abs(distanceX)){
                Ani.SetInteger("Status", 1);
                if(distanceX > 0){ 
                    moveX = move.Speed * Time.deltaTime; 
                    if(Sprite.flipX == false)
                    Sprite.flipX = true;
                }
                else if(distanceX < 0){
                    moveX = -1 * move.Speed * Time.deltaTime;
                    if(Sprite.flipX == true)
                    Sprite.flipX = false;
                }
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + moveX, gameObject.transform.position.y);
            }
            else if(abs(gameObject.transform.position.y - OriPos.y) < abs(distanceY)){
                if(distanceY > 0){
                    moveY = move.Speed * Time.deltaTime;
                    Ani.SetInteger("Status", 2); 
                }
                else if(distanceY < 0){
                    moveY = -1 * move.Speed * Time.deltaTime;
                    Ani.SetInteger("Status", 3);
                }
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + moveY);
            }
            else {
                Ani.SetInteger("Status", -3);
                walk = false;
            }
        }

        
    }

    //這個檔案裝在要走路的物件身上
    public void Walk(float x, float y){
        distanceX = x; //欲移動的x長度
        distanceY = y; //欲移動的y長度
        OriPos = gameObject.transform.position;
        walk = true;
    }
    public float abs(float x){
        return x > 0 ? (x) : (-x) ;
    }
    /*float StartTime, deltaTimeX, deltaTimeY;
    bool finish = true;
    public IEnumerator OtherWalk(GameObject gameObject, float x, float y){
        while(true){
            if(finish){
                StartTime = Time.time;
                OtherAni = gameObject.GetComponentInChildren<Animator>();
                OtherSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
                deltaTimeX = abs(x)/move.Speed;
                deltaTimeY = abs(y)/move.Speed;       
                Debug.Log(x + " " + y + " " + StartTime);
                finish = false;
                StartCoroutine(Xwalk(gameObject, x, y));
                yield break;
            }else yield return new WaitForSeconds(0.02f);
        }
    }
    
    IEnumerator Xwalk(GameObject gameObject, float x, float y){
        while(true){
            if(Time.time - StartTime < deltaTimeX && x != 0){
                OtherAni.SetInteger("Status", 1);
                if(x > 0){
                    move.Moving(gameObject, 1, 0);
                    if(OtherSprite.flipX == false)
                    OtherSprite.flipX = true;
                }else if(x < 0){
                    move.Moving(gameObject, -1, 0);
                    if(OtherSprite.flipX == true)
                    OtherSprite.flipX = false;
                }
                yield return new WaitForSeconds(0.02f);
            }else{
                StartCoroutine(Ywalk(gameObject, y));
                StartTime = Time.time;
                Debug.Log("X finish"+Time.time);
                yield break;
            }
        }        
    }
    public IEnumerator Ywalk(GameObject gameObject, float y){
        while(true){
            if(Time.time - StartTime < deltaTimeY && y != 0){
                if(y > 0){
                    move.Moving(gameObject, 0, 1);
                    OtherAni.SetInteger("Status", 2);
                }else if(y < 0){
                    move.Moving(gameObject, 0, -1);
                    OtherAni.SetInteger("Status", 3);
                }
                yield return new WaitForSeconds(0.02f);
            }else{
                OtherAni.SetInteger("Status", 0);
                Debug.Log("Y finish "+ Time.time); 
                finish = true;               
                yield break;
            }
        }
    }*/
}
