using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public void Awake(){
        BuildDatabase();
    }
    public Item GetItem(string Name){
        return items.Find(item => item.Name == Name);
    }
    void BuildDatabase(){
        items = new List<Item>(){
            new Item("Clue1", "尋寶小紙條", "一張奇怪的紙條，上面寫著\n尋寶的提示：\n「風雨任他來，青春不留白。\n若要健身心，可把排球帶。」"),
            new Item("Clue2", "在排球場撿到的提示", "「憶當初，初入校園；\n而如今，三年飛逝。\n無數典禮在此舉行；\n無數回憶於此流淌。\n如今青春已逝，而君是否無悔？」"),
            new Item("Clue3", "在活動中心撿到的提示", "「老樹\n前人的餘蔭 後人的遮蔽\n樹下流傳著 屬於雄女人的傳說\n我在此駐足\n不為乘涼 不為尋奇\n只為了埋下\n一點平凡的樂趣」"),
            new Item("Clue4", "在榕園撿到的提示", "「夜合含笑開滿園，\n忽傳幽香誰家院？\n排憂解惱好去處，\n邀君於此園中見。」"),
            new Item("LuckyCharm", "御守", "早上尋寶找到的幸運符。\n總之先收著吧。"),
        };
    }
}
