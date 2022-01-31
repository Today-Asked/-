using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class LeafCreater : MonoBehaviour

{

    //要被生成的怪物物件
    public GameObject leaf;

    void Start()

    {

        //執行生成怪物程式碼(每秒一次)

        InvokeRepeating("CreateLeaf", 1, 1);

    }

    public void CreateLeaf()

    {

        int leafNum;

        //隨機決定要生成幾個怪物(0-2個隨機)

        leafNum = Random.Range(0, 3);

        //開始生成怪物

        for (int i = 0; i < leafNum; i++)

        {

            //宣告生成的X座標

            float x;

            //產生隨機的X座標(-6到5之間)

            x = Random.Range(-6, 6);

            //生成怪物

            Instantiate(leaf, new Vector3(x, 2.8f, 0), Quaternion.identity);

        }

    }

}