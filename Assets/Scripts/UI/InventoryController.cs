using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //引用UIInventoryPage脚本，用于管理UI界面
    [SerializeField]
    private UIInventoryPage inventoryUI;

    //背包的UI大小
    public int inventorySize = 20;

    public AudioSource audiosource;

    public static bool isInitialize = false;

    private void Awake()
    {
        //初始化背包UI界面
        Debug.Log(1);
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    private void Update()
    {
        //按下I键 切换背包UI界面下的显示状态
        if (Input.GetKeyDown(KeyCode.I))
        {   
            audiosource.Play();
            if (inventoryUI.isActiveAndEnabled == false)
            {
                isInitialize = false;
                inventoryUI.Show(inventorySize);
            }
            else
            {
                isInitialize = true;
                Debug.Log(11);
                inventoryUI.Hide();
            }
        }
    }
}

