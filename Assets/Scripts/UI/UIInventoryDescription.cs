using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;  //物品图片的Image组件
    [SerializeField]
    private TMP_Text title;  //物品名称的Textmeshpro组件
    [SerializeField]
    private TMP_Text description;  //物品描述的TextMeshPro组件

    public void Awake()
    {
        //ResetDescription();  //在该脚本实例化后，调用ResetDescription方法，重置物品描述
    }

    //重置物品描述
    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);  //将物品图片设置为不可见
        this.title.text = "";  //将物品名称设置为空字符串
        this.description.text = "";  //将物品描述文本设置为空字符串
    }

    //设置物品描述的内容
    public void setDescription(Sprite sprite, string itemName,string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);  //将物品图片设为可见
        this.itemImage.sprite = sprite;  //将传入的Sprite赋值给物品图片
        this.title.text = itemName;  //将传入的物品名称赋值给物品名称文本
        this.description.text = itemDescription;  //将传入的物品描述赋值给物品描述文本
    }
}
