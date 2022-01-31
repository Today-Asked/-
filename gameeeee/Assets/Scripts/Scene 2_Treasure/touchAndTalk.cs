using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class touchAndTalk : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage;
    public Image mImagecg;

    [Header("文本")]
    public TextAsset textAsset;
    public TextAsset textAsset2;
    public TextAsset textAsset3;
    public TextAsset textAsset4;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite cg1;
    public Sprite cg2;
    public Sprite cg3;
    public Sprite cg4;
    [Header("紙條")]
    public GameObject clueThree, clueFour, LuckyCharm;
    public Inventory inventory;
    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    private int read;
    private int textcount;
    public changeScene changeScene;

/*
  [r] 換行
  [l] 按一下才會繼續，不換行
  [lr]按一下才會繼續，換行
  [w] 按一下才會繼續，清空畫面(字)
*/

    void Start()
    {
        msgSys = this.GetComponent<ES_MessageSystem>();
        read = 0;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        msgSys.AddSpecialCharToFuncMap("A", inputList1);
        msgSys.AddSpecialCharToFuncMap("B", inputList2);
        msgSys.AddSpecialCharToFuncMap("C", inputList3);
        msgSys.AddSpecialCharToFuncMap("D", inputList4);
        msgSys.AddSpecialCharToFuncMap("E", input1);
        msgSys.AddSpecialCharToFuncMap("F", input2);
        msgSys.AddSpecialCharToFuncMap("G", input3);
        msgSys.AddSpecialCharToFuncMap("H", input4);
        msgSys.AddSpecialCharToFuncMap("I", input5);
        msgSys.AddSpecialCharToFuncMap("NC", inputNothingCG);
        msgSys.AddSpecialCharToFuncMap("N", inputNothing);
        msgSys.AddSpecialCharToFuncMap("S3",showClueThree);
        msgSys.AddSpecialCharToFuncMap("S4",showClueFour);
        msgSys.AddSpecialCharToFuncMap("ST",showTreasure);
        msgSys.AddSpecialCharToFuncMap("Back", Back);
    }

    private void inputNothingCG(){mImagecg.sprite = image0;}
    private void inputNothing(){mImage.sprite = image0;}
    private void inputList1(){mImagecg.sprite = cg1;}
    private void inputList2(){mImagecg.sprite = cg2;}
    private void inputList3(){mImagecg.sprite = cg3;}
    private void inputList4(){mImagecg.sprite = cg4;}
    private void input1(){mImage.sprite = image1;}
    private void input2(){mImage.sprite = image2;}
    private void input3(){mImage.sprite = image3;}
    private void input4(){mImage.sprite = image4;}
    private void input5(){mImage.sprite = image5;}
    private void showClueThree(){
        clueThree.SetActive(true);
        inventory.AddItem("Clue2");    
    }
    private void showClueFour(){
        clueFour.SetActive(true);
        inventory.AddItem("Clue3");
    }
    private void showTreasure(){
        LuckyCharm.SetActive(true);
        inventory.AddItem("Clue4");
    }
    
    private void Back(){changeScene.CanChangeScene = 1;}
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
        if (move.collided == true){
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("player").GetComponent<move>().enabled = false;//停止 move.cs
            
            if(read == 0){ //判斷是否讀過文本
                switch(move.words){ //透過撞到的東西選擇要讀的文本
                    case "Clue2" :
                        ReadTextDataFromAsset(textAsset);
                        textcount = textList.Count;
                        read = 1;
                        break;

                    case "Clue3" :
                        ReadTextDataFromAsset(textAsset2);
                        textcount = textList.Count;
                        read = 1;
                        break;
                    
                    case "Clue4" :
                        ReadTextDataFromAsset(textAsset3);
                        textcount = textList.Count;
                        read = 1;
                        break;
                    
                    case "LuckyCharm" :
                        ReadTextDataFromAsset(textAsset4);
                        textcount = textList.Count;
                        read = 1;
                        break;
                }
            }

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
        if (msgSys.IsCompleted == true && textIndex < textcount) //__.Count :算長度
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }

        if(textIndex == textcount){//註：文本最後一行要甚麼都沒有 (不然會少一句話)
            read = 0;
            textIndex = 0;
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            move.collided = false;
            move.words = "n";
            GameObject.Find("player").GetComponent<move>().enabled = true;
        }
        }
    }
}