using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;  //物品预制体

    [SerializeField]
    private RectTransform contentPanel;  //内容面板

    [SerializeField]
    private UIInventoryDescription itemDescription;  //物品描述面板

    public List<UIInventoryItem> ListOfUIItems = new List<UIInventoryItem>();  //物品列表

    public static List<PickedUpItem> pickedupItem = new List<PickedUpItem>();  //物品列表 设置为静态 全局皆可显示

    private int selectedIndex = -1;

    private int index = 0;

    private static UIInventoryPage instance;  //单例模式

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //itemDescription.ResetDescription();
    }

    public static UIInventoryPage Instance
    {
        get { return instance; }
    }

    public void InitializeInventoryUI(int inventorysize)  //初始化物品页面
    {
        Debug.Log("InitializeInventoryUI");
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);  //设置物品的父物体
            ListOfUIItems.Add(uiItem);  //将物品添加到物品列表中
            uiItem.OnItemClicked += HandleItemSelection;  //绑定物品点击事件

            // 添加对应的 PickedUpItem 对象
            pickedupItem.Add(null);
        }
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        Debug.Log("HandleItemSelection called");
        selectedIndex = ListOfUIItems.IndexOf(obj);
        PickedUpItem selectedPickedUpItem = pickedupItem[selectedIndex];

        itemDescription.setDescription(selectedPickedUpItem.image, selectedPickedUpItem.title, selectedPickedUpItem.description);  //设置物品描述

        foreach (UIInventoryItem item in ListOfUIItems)
        {
            if (item != obj)
                item.Deselect();
        }
        obj.Select();  //选中一个物品
    }


    public void Show(int inventorysize)
    {
        gameObject.SetActive(true);  //显示物品页面
        itemDescription.ResetDescription();  //重置物品描述面板
    }

    public void Hide()
    {
        gameObject.SetActive(false);  //隐藏物品页面
    }

    public void Add(PickedUpItem item)
    {
        ListOfUIItems[index].SetData(item.image, item.quantity);
        pickedupItem[index] = item;
        index++;
        itemDescription.setDescription(item.image, item.title, item.description);
    }
}
