using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class UsageCase1 : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage;
    public Image mImageName;

    [Header("文本")]
    public TextAsset textAsset;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite imageName1;

    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    public changeScene changeScene;
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
        application.scene = "1";
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(textAsset);

        //add special chars(關鍵字)and functions in other component.
        msgSys.AddSpecialCharToFuncMap("A", input0);
        msgSys.AddSpecialCharToFuncMap("B", input1);
        msgSys.AddSpecialCharToFuncMap("C", input2);
        msgSys.AddSpecialCharToFuncMap("D", input3);
        msgSys.AddSpecialCharToFuncMap("E", input4);
        msgSys.AddSpecialCharToFuncMap("N1", inputName1);
        msgSys.AddSpecialCharToFuncMap("CS", changescene);
    }

    //可自己定義(放音樂、圖片)
    private void input0(){mImage.sprite = image0;}
    private void input1(){mImage.sprite = image1;}
    private void input2(){mImage.sprite = image2;}
    private void input3(){mImage.sprite = image3;}
    private void input4(){mImage.sprite = image4;}
    private void inputName1(){mImageName.sprite = imageName1;}
    private void changescene(){changeScene.CanChangeScene = 1;}

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
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