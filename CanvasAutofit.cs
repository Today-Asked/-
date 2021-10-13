using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAutofit : MonoBehaviour
{
    public GridLayoutGroup Content_layout;
    public RectTransform Content, Canvas;
    public GameObject PropTitle, PropDescription;
    public RectTransform parent;
    void Awake()
    {
        //Inventory autofit
        double CellSize = Canvas.rect.width * 0.102;
        float _CellSize = (float)CellSize; //對不起 我就毒
        Content_layout.cellSize = new Vector2(_CellSize, _CellSize);
        Content.sizeDelta = new Vector2(_CellSize * 10 + 100, (float)Content.rect.height);
        
        //Description
        //float TextSize = 0.15f * Canvas.rect.height;
        //PropTitle.GetComponent<RectTransform>().sizeDelta = new Vector2(0.79f,0.04f);
        //PropTitle.GetComponent<Text>().fontSize = Mathf.RoundToInt(TextSize);
    }
}
