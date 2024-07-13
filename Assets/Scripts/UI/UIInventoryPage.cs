using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;  //��ƷԤ����

    [SerializeField]
    private RectTransform contentPanel;  //�������

    [SerializeField]
    private UIInventoryDescription itemDescription;  //��Ʒ�������

    public List<UIInventoryItem> ListOfUIItems = new List<UIInventoryItem>();  //��Ʒ�б�

    public static List<PickedUpItem> pickedupItem = new List<PickedUpItem>();  //��Ʒ�б� ����Ϊ��̬ ȫ�ֽԿ���ʾ

    private int selectedIndex = -1;

    private int index = 0;

    private static UIInventoryPage instance;  //����ģʽ

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

    public void InitializeInventoryUI(int inventorysize)  //��ʼ����Ʒҳ��
    {
        Debug.Log("InitializeInventoryUI");
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);  //������Ʒ�ĸ�����
            ListOfUIItems.Add(uiItem);  //����Ʒ��ӵ���Ʒ�б���
            uiItem.OnItemClicked += HandleItemSelection;  //����Ʒ����¼�

            // ��Ӷ�Ӧ�� PickedUpItem ����
            pickedupItem.Add(null);
        }
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        Debug.Log("HandleItemSelection called");
        selectedIndex = ListOfUIItems.IndexOf(obj);
        PickedUpItem selectedPickedUpItem = pickedupItem[selectedIndex];

        itemDescription.setDescription(selectedPickedUpItem.image, selectedPickedUpItem.title, selectedPickedUpItem.description);  //������Ʒ����

        foreach (UIInventoryItem item in ListOfUIItems)
        {
            if (item != obj)
                item.Deselect();
        }
        obj.Select();  //ѡ��һ����Ʒ
    }


    public void Show(int inventorysize)
    {
        gameObject.SetActive(true);  //��ʾ��Ʒҳ��
        itemDescription.ResetDescription();  //������Ʒ�������
    }

    public void Hide()
    {
        gameObject.SetActive(false);  //������Ʒҳ��
    }

    public void Add(PickedUpItem item)
    {
        ListOfUIItems[index].SetData(item.image, item.quantity);
        pickedupItem[index] = item;
        index++;
        itemDescription.setDescription(item.image, item.title, item.description);
    }
}
