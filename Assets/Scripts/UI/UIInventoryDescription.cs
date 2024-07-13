using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;  //��ƷͼƬ��Image���
    [SerializeField]
    private TMP_Text title;  //��Ʒ���Ƶ�Textmeshpro���
    [SerializeField]
    private TMP_Text description;  //��Ʒ������TextMeshPro���

    public void Awake()
    {
        //ResetDescription();  //�ڸýű�ʵ�����󣬵���ResetDescription������������Ʒ����
    }

    //������Ʒ����
    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);  //����ƷͼƬ����Ϊ���ɼ�
        this.title.text = "";  //����Ʒ��������Ϊ���ַ���
        this.description.text = "";  //����Ʒ�����ı�����Ϊ���ַ���
    }

    //������Ʒ����������
    public void setDescription(Sprite sprite, string itemName,string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);  //����ƷͼƬ��Ϊ�ɼ�
        this.itemImage.sprite = sprite;  //�������Sprite��ֵ����ƷͼƬ
        this.title.text = itemName;  //���������Ʒ���Ƹ�ֵ����Ʒ�����ı�
        this.description.text = itemDescription;  //���������Ʒ������ֵ����Ʒ�����ı�
    }
}
