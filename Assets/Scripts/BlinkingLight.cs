using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlinkingLight : MonoBehaviour
{
    public float minTime = 0.1f; // ��С���ʱ��
    public float maxTime = 0.5f; // �����ʱ��

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            this.GetComponent<Light2D>().enabled = !this.GetComponent<Light2D>().enabled;  // �л��ƹ�״̬
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); // �ȴ����ʱ��
        }
    }
}

