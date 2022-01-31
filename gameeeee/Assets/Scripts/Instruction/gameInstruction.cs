using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class gameInstruction : MonoBehaviour
{
    private ES_MessageSystem msgSys; //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText; //uiText:更新文字時使用的物件
    public Image mImage;

    [Header("文本")]
    public TextAsset textAsset;

    public Sprite image0;
    public Sprite image1;
    public Sprite image2;

    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //储存說到哪一句話
    
/*
  [r] 換行
  [l] 按一下才會繼續，不換行
  [lr]按一下才會繼續，換行
  [w] 按一下才會繼續，清空畫面(字)
*/

    void Start()
    {
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else ReadTextDataFromAsset(textAsset);

        GameObject.Find("player").GetComponent<move>().enabled = false;

        //add special chars(關鍵字)and functions in other component.
        msgSys.AddSpecialCharToFuncMap("close", closePanel);
        msgSys.AddSpecialCharToFuncMap("A", input1);
        msgSys.AddSpecialCharToFuncMap("B", input2);
        msgSys.AddSpecialCharToFuncMap("C", input0);
    }

    //可自己定義(放音樂、圖片)
    private void input1(){mImage.sprite = image1;}
    private void input2(){mImage.sprite = image2;}
    private void input0(){mImage.sprite = image0;}
    private void closePanel(){
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("player").GetComponent<move>().enabled = true;
    }

    private void ReadTextDataFromAsset(TextAsset _textAsset) //把文字傳進 list
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');//用 \n 來切文字
        foreach (string line in lineTextData)//把字一行一行加到 textList 裡
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

        if (msgSys.IsCompleted == false)//IsCompleted(回傳boolean) 判斷執行完了沒(執行完:false 還沒:true)
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