using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //����UIInventoryPage�ű������ڹ���UI����
    [SerializeField]
    private UIInventoryPage inventoryUI;

    //������UI��С
    public int inventorySize = 20;

    public AudioSource audiosource;

    public static bool isInitialize = false;

    private void Awake()
    {
        //��ʼ������UI����
        Debug.Log(1);
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    private void Update()
    {
        //����I�� �л�����UI�����µ���ʾ״̬
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

