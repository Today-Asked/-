using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class talk : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    //private List<string> textList = new List<string>(); 用list存文件裡的文字
    //private char words;
    public move move;

/*
  [r] 換行
  [l] 按一下才會繼續，不換行
  [lr]按一下才會繼續，換行
  [w] 按一下才會繼續，清空畫面(字)
*/
    
    void Start()
    {
        /*textList.Clear();
        textList.Add("現在不用去那裡[r]趕快移動到九班教室吧~[w]");//0
        textList.Add("這裡是警衛室[r]還是趕快移動到九班教室吧~[w]");//1*/
        msgSys = this.GetComponent<ES_MessageSystem>();
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Update()
    {
        
        if (move.collided){
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            move.enabled = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                msgSys.Next();
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                move.collided = false;
                move.enabled = true;
                //Continue the messages, stoping by [w] or [lr] keywords.
            }

            //If the message is complete, stop updating text.
            if (msgSys.IsCompleted == false)
            {
                uiText.text = msgSys.text;
            }

            //Auto update from textList.
            if (msgSys.IsCompleted == true ) //__.Count :算長度
            {
                Talking(gameObject);
            }
        }
    }
    string cantGo;
    char a;
    public void Talking(GameObject gameObject){
        Debug.Log(gameObject.transform.parent.name);
        Debug.Log(gameObject.transform.name);
        //if(gameObject != null){
            if(gameObject.transform.parent.name == "message"){
                switch(gameObject.name){
                    case "account":
                        msgSys.SetText("這是會計室。窗簾都關著，看不出什麼。[w]");
                        break;
                    case "studentAffair":
                        msgSys.SetText("這是學務處。老師們好像都還沒來。[w]");
                        break;
                    case "instructor":
                        msgSys.SetText("這是教官室。現在裡面沒有教官，可能都出去巡邏了吧。[w]");
                        break;
                    case "sink":
                        msgSys.SetText("這是洗手台。沒什麼特別的。[w]");
                        break;
                    case "103":
                        msgSys.SetText("這是一年三班。一年九班的教室在二樓。[w]");
                        break;
                    case "104":
                        msgSys.SetText("這是一年四班。一年九班的教室在二樓。[w]");
                        break;
                    case "hakka":
                        msgSys.SetText("這是客語教室。聽說下雨沒辦法升旗的時候，校長就會在這裡轉播。[w]");
                        break;
                    case "interactive":
                        msgSys.SetText("這是互動教室。每個大桌上都有一台電腦，感覺是分組討論會用到的教室。[w]");
                        break;
                    case "110":
                        msgSys.SetText("這是一年十班。一年九班的教室在右邊，趕快進去吧！[w]");
                        break;
                    case "107":
                        msgSys.SetText("這是一年七班。一年九班的教室在中間那一排。[w]");
                        break;
                    case "108":
                        msgSys.SetText("這是一年八班。一年九班的教室在中間那一排。[w]");
                        break;
                    case "guard room":
                        msgSys.SetText("這是警衛室。聽說早上會有可愛的警衛伯伯在門口和大家揮手打招呼，今天沒有看到。[w]");
                        break; 
                    case "sign":
                        msgSys.SetText("上面寫著「原鄉客香花園」。這應該就是植物園了吧。[w]");
                        break;  
                    case "gate109":
                        msgSys.SetText("有一個寫著「109」的牌子，指向玄關。[r]對喔，我記得我們班在這棟的二樓。[w]");
                        break;  
                    case "porch109":
                        msgSys.SetText("又看到另一個寫著「109」的牌子，指向右邊的走廊。往這邊走吧。[w]");
                        break;  
                }

            }else if(gameObject.transform.parent.name == "cantGo"){ //不能去的地方
                switch(gameObject.name){
                    case "CenterLeft":
                        cantGo = "早上在這裡舉辦開學典禮，現在鎖起來了。";
                        break;
                    case "CenterFront":
                        cantGo = "早上在這裡舉辦開學典禮，現在鎖起來了。";
                        break;
                    case "cantGo":
                        cantGo = "";
                        break;
                    case "left":
                        cantGo = "那邊是教務處跟總務處。";
                        break;
                    case "outside":
                        cantGo = "外面是籃球場跟操場，就是所謂的雄女湖。";
                        break;
                    case "B1":
                        cantGo = "聽說福利社在地下室，中午再去買午餐吧。";
                        break;
                    case "3F":
                        cantGo = "再上去就是三樓了，";
                        break;
                    case "2Fleft":
                        cantGo = "再過去就是11班了。";
                        break;
                }
                msgSys.SetText(cantGo + "現在沒有必要去那裏。[w]");
            }
        //}
    }
}