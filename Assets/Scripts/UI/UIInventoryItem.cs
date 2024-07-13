using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;  //物品图像组件
    [SerializeField]
    private TMP_Text quatityTxt;  //物品数量文本组件

    [SerializeField]
    private Image borderImage;  //物品选中边框组件

    //物品点击事件
    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick;

    private bool empty = true;  //物品是否为空

    private void Awake()
    {
        ResetData();  //重置物品数据 
        Deselect();  //取消选中物品
    }
    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);  //隐藏物品图像
        empty = true;  //将物品标记为空
    }
    public void Deselect()
    {
        borderImage.enabled = false;  //取消物品选中边框
    }
    public void SetData(Sprite sprite,int quatity)
    {
        this.itemImage.gameObject.SetActive(true);  //显示物品图像
        this.itemImage.sprite = sprite;  //设置物品图像
        this.quatityTxt.text = quatity + "";  //设置物品数量文本
        empty = false;  //标记物品非空
    }

    public void Select()
    {
        borderImage.enabled = true;  //选中物品
    }

    //物品点击指针事件
    public void OnPointerClick(BaseEventData data)
    {
        if (empty) //如果物品为空则返回
            return; 
        PointerEventData pointerData = (PointerEventData)data; //获取指针事件数据
        if(pointerData.button == PointerEventData.InputButton.Right)  //如果是鼠标右键点击
        {
            OnRightMouseBtnClick?.Invoke(this);  //调用物品右键点击
        }
        else
        {
            OnItemClicked?.Invoke(this);  //调用物品点击事件
        }
    }
}
