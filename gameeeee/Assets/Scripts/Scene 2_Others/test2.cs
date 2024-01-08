using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class test2 : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage;
    public Image mImageName;
    public Image mImagecg;

    [Header("文本")]
    public TextAsset textAsset;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite image6;
    public Sprite image7;
    public Sprite image8;
    public Sprite image9;
    public Sprite image10;
    public Sprite imageName1;
    public Sprite imageName2;
    public Sprite cg1;

    public SCmove SC, Lili;
    public GameObject DustPan;

    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    public static int panalclose = 0;
    /* 
    1 => open "no"
    2 => open "yes"
    */
    public GameObject Menu; //[要尋寶嗎?]選擇

/*
  [r] 換行
  [l] 按一下才會繼續，不換行
  [lr]按一下才會繼續，換行
  [w] 按一下才會繼續，清空畫面(字)
*/
    public GameApplication gameApplication;
    bool game;

    void Awake(){
        gameApplication = FindObjectOfType<GameApplication>();
        if(gameApplication.scene == "explore"){
            game = true; //剛剛探險完回來
        }
        else game = false;
    }
    void Start()
    {
        //anim = GetComponent< Animator >();
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(textAsset);

        Menu.SetActive(false);

        //add special chars(關鍵字)and functions in other component.
        msgSys.AddSpecialCharToFuncMap("B", B);
        msgSys.AddSpecialCharToFuncMap("C", C);
        msgSys.AddSpecialCharToFuncMap("D", D);
        msgSys.AddSpecialCharToFuncMap("E", E);
        msgSys.AddSpecialCharToFuncMap("F", F);
        msgSys.AddSpecialCharToFuncMap("G", G);
        msgSys.AddSpecialCharToFuncMap("H", H);
        msgSys.AddSpecialCharToFuncMap("I", I);
        msgSys.AddSpecialCharToFuncMap("J", J);
        msgSys.AddSpecialCharToFuncMap("K", K);
        msgSys.AddSpecialCharToFuncMap("N1", inputName1);
        msgSys.AddSpecialCharToFuncMap("N2", inputName2);
        msgSys.AddSpecialCharToFuncMap("Choose", Choose);   
        msgSys.AddSpecialCharToFuncMap("moveYSCGo", MoveYSCGo);
        msgSys.AddSpecialCharToFuncMap("moveXSCGo", MoveXSCGo);
        msgSys.AddSpecialCharToFuncMap("moveSCBack", MoveSCBack);
        //msgSys.AddSpecialCharToFuncMap("moveXSCBack", MoveXSCBack);
        msgSys.AddSpecialCharToFuncMap("moveYGo", MoveYGo);
        msgSys.AddSpecialCharToFuncMap("WalkAndTalk", WalkAndTalk);
        msgSys.AddSpecialCharToFuncMap("ByeSC", ByeSC);
    }

    private void B(){mImage.sprite = image1;}
    private void C(){mImage.sprite = image2;}
    private void D(){mImage.sprite = image3;}
    private void E(){mImage.sprite = image4;}
    private void F(){mImage.sprite = image5;}
    private void G(){mImage.sprite = image6;}
    private void H(){mImage.sprite = image7;}
    private void I(){mImage.sprite = image8;}
    private void J(){mImage.sprite = image9;
                     mImagecg.sprite = cg1;}
    private void K(){mImage.sprite = image10;
                     mImagecg.sprite = cg1;}
    private void inputName1(){mImageName.sprite = imageName1;}
    private void inputName2(){mImageName.sprite = imageName2;}
    private void Choose(){Menu.SetActive(true);}
    private void MoveYSCGo(){SC.Walk(0, -1);}
    private void MoveXSCGo(){SC.Walk(1.3f, 0);}
    private void MoveSCBack(){Destroy(DustPan); SC.Walk(-1.3f, 1);}
    //private void MoveXSCBack(){SC.Walk(-8, 0);}
    private void MoveYGo(){Lili.Walk(0, -1);}
    private void WalkAndTalk(){
        Lili.Walk(0, -1.35f);
        SC.Walk(0, -1.38f);
    }
    private void ByeSC(){SC.Walk(0, -5.65f);}

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
        /*if(Input.GetKeyDown("k")){ //受不了 我需要跳過的功能QQ
            gameApplication.scene = "explore";
            gameApplication.OnStartGame("scene2");
        }*/
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
        if(game)
        {
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }
    public void clickYes()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        panalclose = 2; //打開選「是」的panel
    }
    public void clickNo()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        panalclose = 1; //打開選「否」的panel
    }
}