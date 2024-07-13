using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlinkingLight : MonoBehaviour
{
    public float minTime = 0.1f; // 最小间隔时间
    public float maxTime = 0.5f; // 最大间隔时间

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            this.GetComponent<Light2D>().enabled = !this.GetComponent<Light2D>().enabled;  // 切换灯光状态
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); // 等待随机时间
        }
    }
}

