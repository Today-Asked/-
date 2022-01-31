using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class chooseYes : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    [Header("UI")]
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public Image mImage;
    public Image mImagecg;

    [Header("文本")]
    public TextAsset textAsset;

    [Header("立繪&CG")]
    public Sprite image0;
    public Sprite image1;
    public Sprite cg1;

    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話

    public GameApplication gameApplication;
    bool game;
    public changeScene changeSceneExp;

    
    void Awake(){
        gameApplication = FindObjectOfType<GameApplication>();
        if(gameApplication.scene == "explore"){
            game = true; 
        }
        else game = false;
    }
    void Start()
    {
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

        msgSys.AddSpecialCharToFuncMap("B", B);
        msgSys.AddSpecialCharToFuncMap("C", C);
        msgSys.AddSpecialCharToFuncMap("CE", changeExplore);
        
    }

    private void B(){mImage.sprite = image1;}
    private void C(){mImagecg.sprite = cg1;}
    
    private void changeExplore(){
        changeSceneExp.CanChangeScene = 1;    
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
        if (test2.panalclose == 2){
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
        if(game){ //已經尋寶完回來
            test2.panalclose = 0;
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false; //把自己關掉
        }
    }
}
