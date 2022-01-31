using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Image spriteImage, FrameImage;
    public Image PropImage; //說明欄內的圖片
    public GameObject Frame, Description;
    public Text PropTitle, PropDescription;
    public UIInventory uiinventory;
    private void Awake()
    {
        UpdateItem(null); //初始化為null
        spriteImage = GetComponent<Image>();
        FrameImage = Frame.GetComponent<Image>();
        FrameImage.color = Color.clear;
    }
    
    public void UpdateItem(Item item)
    {
        Color SpriteColor = spriteImage.color;
        this.item = item;
        if(item != null){ //設為不透明
            spriteImage.color = Color.white;
            spriteImage.sprite = item.Icon;
            spriteImage.preserveAspect = true;
        }else{
            spriteImage.color = Color.clear;
        }
    }
    public void OnPointerClick(PointerEventData eventData){
        if(this.item != null){
            if(uiinventory.SelectedItem != this.item.Name){
                uiinventory.SelectedItem = this.item.Name;
                uiinventory.Select(this.item);
                FrameImage.color = Color.white;
            }else{
                Description.SetActive(true);
                PropTitle.text = this.item.Title;
                PropDescription.text = this.item.Description;
                PropDescription.fontSize = 30;
                PropImage.sprite = spriteImage.sprite;
                PropImage.preserveAspect = true;
            }           
        }
    }
    public void Refresh(Item item){
        this.item = item;
        if (uiinventory.SelectedItem != this.item.Name) 
            FrameImage.color = Color.clear;
        else FrameImage.color = Color.white;
    }
    public void Reset(){
        FrameImage.color = Color.clear;
    }
}
