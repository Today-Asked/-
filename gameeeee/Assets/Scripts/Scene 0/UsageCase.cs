using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class UsageCase : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage;
    public Image mImageName;
    public Image mImagecg;
    public Image mImageground;
    public Image mImageroom;

    [Header("文本")]
    public TextAsset textAsset;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite imageName1;
    public Sprite imageName2;
    public Sprite imageName3;
    public Sprite cg1;
    public Sprite cg2;
    public Sprite cg3;
    public Sprite cg4;
    public Sprite cg5;
    public Sprite cg6;
    public Sprite cg7;
    public Sprite cg10;

    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    public changeScene changeScene;
    private bool animationDone = true;
    public GameApplication application;
    
/*
  [r] 換行
  [l] 按一下才會繼續，不換行
  [lr]按一下才會繼續，換行
  [w] 按一下才會繼續，清空畫面(字)
*/

    void Start()
    {
        application = FindObjectOfType<GameApplication>();
        application.scene = "0";
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(textAsset);


        //add special chars(關鍵字)and functions in other component.
        msgSys.AddSpecialCharToFuncMap("B", B);
        msgSys.AddSpecialCharToFuncMap("C", input2);
        msgSys.AddSpecialCharToFuncMap("D", input3);
        msgSys.AddSpecialCharToFuncMap("E", inputName1);
        msgSys.AddSpecialCharToFuncMap("F", inputName2);
        msgSys.AddSpecialCharToFuncMap("G", inputName3);
        msgSys.AddSpecialCharToFuncMap("1", inputCg1);
        msgSys.AddSpecialCharToFuncMap("2", inputCg2);
        msgSys.AddSpecialCharToFuncMap("3", inputCg3);
        msgSys.AddSpecialCharToFuncMap("4", inputCg4);
        msgSys.AddSpecialCharToFuncMap("5", inputCg5);
        msgSys.AddSpecialCharToFuncMap("6", inputCg6);
        msgSys.AddSpecialCharToFuncMap("CS", changescene);
    }

    //可自己定義(放音樂、圖片)
    private void B(){mImagecg.sprite = image0;
                     mImage.sprite = image1;}
    private void input2(){mImage.sprite = image2;}
    private void input3(){mImage.sprite = image3;}
    private void inputName1(){mImageName.sprite = imageName1;}
    private void inputName2(){mImageName.sprite = imageName2;}
    private void inputName3(){mImageName.sprite = imageName3;}
    private void inputCg1(){mImagecg.sprite = cg1;}
    private void inputCg2(){mImagecg.sprite = cg2;}
    private void inputCg3(){mImagecg.sprite = cg3;}
    private void inputCg4(){animationDone = false;
                            mImagecg.sprite = cg4;
                            Invoke( "changeCg" , 0.7f);
                            Invoke( "tocg6" , 1.1f);
                            InvokeRepeating( "toblack" , 1.4f,0);
                            Invoke( "stopAnimation" , 1.5f);}
    private void inputCg5(){mImagecg.sprite = cg5;}
    private void inputCg6(){mImageground.sprite = image0;
                            mImageroom.sprite = cg10;
                            mImage.sprite = image1;}
    private void changescene(){changeScene.CanChangeScene = 1;}

    private void changeCg(){mImagecg.sprite = cg5;}
    private void tocg6(){mImageground.sprite = cg6;}
    private void toblack(){mImageground.sprite = cg7;}
    private void stopAnimation(){animationDone = true;}


    private void ReadTextDataFromAsset(TextAsset _textAsset) //把文字傳進list
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }

    void Update()
    {
        if(animationDone){// == true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }
        }
        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }

        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count) //__.Count :算長度
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }
        
    }
}

/*      if (Input.GetKeyDown(KeyCode.S))
        {
            //You can sending the messages from strings or text-based files.
            if (msgSys.IsCompleted) //IsCompleted(回傳boolean) 判斷執行完了沒(執行完:false 還沒:true)
            {
                msgSys.SetText("Send the messages![lr] HelloWorld![w]"); //(按空白鍵之前)會停在[lr]前一個字
            }
        }
*/