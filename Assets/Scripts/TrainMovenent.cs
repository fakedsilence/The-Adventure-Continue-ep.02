using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovenent : MonoBehaviour
{
    public float moveSpeed = 1f;
    private void Update()
    {
        this.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
