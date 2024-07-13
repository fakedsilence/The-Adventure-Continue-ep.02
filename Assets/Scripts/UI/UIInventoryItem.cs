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
    private Image itemImage;  //��Ʒͼ�����
    [SerializeField]
    private TMP_Text quatityTxt;  //��Ʒ�����ı����

    [SerializeField]
    private Image borderImage;  //��Ʒѡ�б߿����

    //��Ʒ����¼�
    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick;

    private bool empty = true;  //��Ʒ�Ƿ�Ϊ��

    private void Awake()
    {
        ResetData();  //������Ʒ���� 
        Deselect();  //ȡ��ѡ����Ʒ
    }
    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);  //������Ʒͼ��
        empty = true;  //����Ʒ���Ϊ��
    }
    public void Deselect()
    {
        borderImage.enabled = false;  //ȡ����Ʒѡ�б߿�
    }
    public void SetData(Sprite sprite,int quatity)
    {
        this.itemImage.gameObject.SetActive(true);  //��ʾ��Ʒͼ��
        this.itemImage.sprite = sprite;  //������Ʒͼ��
        this.quatityTxt.text = quatity + "";  //������Ʒ�����ı�
        empty = false;  //�����Ʒ�ǿ�
    }

    public void Select()
    {
        borderImage.enabled = true;  //ѡ����Ʒ
    }

    //��Ʒ���ָ���¼�
    public void OnPointerClick(BaseEventData data)
    {
        if (empty) //�����ƷΪ���򷵻�
            return; 
        PointerEventData pointerData = (PointerEventData)data; //��ȡָ���¼�����
        if(pointerData.button == PointerEventData.InputButton.Right)  //���������Ҽ����
        {
            OnRightMouseBtnClick?.Invoke(this);  //������Ʒ�Ҽ����
        }
        else
        {
            OnItemClicked?.Invoke(this);  //������Ʒ����¼�
        }
    }
}
