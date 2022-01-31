using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class chooseNo : MonoBehaviour
{
    private ES_MessageSystem msgSys;  //引用 ES_MessageSystem 的物件
    public UnityEngine.UI.Text uiText;  //ui:更新文字時使用的物件
    public TextAsset textAsset;
    private List<string> textList = new List<string>(); //用list存文件裡的文字
    private int textIndex = 0; //存 到哪一句話
    private int textcount;
    private int textCounting;
    public static int panalNo = 1;
    public SCmove Lili;
    public Animation Fade;

    // Start is called before the first frame update
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
        textcount = textList.Count;
        msgSys.AddSpecialCharToFuncMap("B", B);
    }
    void B(){
        Lili.Walk(0, -5.65f);
        Fade.Play("淡出");
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
        //var panalclose2 = test2.panalclose;
        if (test2.panalclose == 1){
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Continue the messages, stoping by [w] or [lr] keywords.
                msgSys.Next();
                textCounting++;
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
            if (textCounting > textcount){
                panalNo = 0;
                test2.panalclose = 0;
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}
