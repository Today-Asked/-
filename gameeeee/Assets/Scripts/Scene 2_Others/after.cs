using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class after : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage, mImageName, mImagecg;
    public Image background, backgroundStart;

    [Header("文本")]
    public TextAsset textAsset;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite imageName1;
    public Sprite imageName2;
    public Sprite cg1;
    public Sprite cg2;
    public changeScene changeSceneMain;
    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    public static int textcount;
    public GameApplication gameApplication;
    bool game;
    public GameObject Canvas, chooseYes;
    public GameObject Fade;

    void Awake(){
        gameApplication = FindObjectOfType<GameApplication>();
        if(gameApplication.scene == "explore"){
            game = true;
        }
        else game = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        gameApplication.scene = "2";

        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(textAsset);
            textcount = textList.Count;

        msgSys.AddSpecialCharToFuncMap("B", B);
        msgSys.AddSpecialCharToFuncMap("C", C);
        msgSys.AddSpecialCharToFuncMap("D", D);
        msgSys.AddSpecialCharToFuncMap("E", E);
        msgSys.AddSpecialCharToFuncMap("F", F);
        msgSys.AddSpecialCharToFuncMap("N1", inputName1);
        msgSys.AddSpecialCharToFuncMap("N2", inputName2);
        msgSys.AddSpecialCharToFuncMap("CM", changeMain);
    }

    private void B(){mImage.sprite = image1;}
    private void C(){mImage.sprite = image2;
                     mImagecg.sprite = cg2;}
    private void D(){mImage.sprite = image2;
                     mImagecg.sprite = image0;}
    private void E(){mImage.sprite = image3;
                     mImagecg.sprite = cg1;}
    private void F(){mImage.sprite = image2;}
    private void inputName1(){mImageName.sprite = imageName1;}
    private void inputName2(){mImageName.sprite = imageName2;}
    private void changeMain(){
        changeSceneMain.CanChangeScene = 1;
    }

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

    // Update is called once per frame
    void Update()
    {
        if (chooseNo.panalNo == 0 || game){
            if(Fade.activeSelf){
                Fade.GetComponent<Animation>().Play("淡入");
                Fade.SetActive(false);
            }
            Canvas.SetActive(false);
            chooseYes.SetActive(false);
            //backgroundStart.sprite = image0;
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

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
}
