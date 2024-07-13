using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionToStab : MonoBehaviour
{
    private bool istriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        istriggered = true;
    }

    private void Update()
    {
        if(istriggered)
        {
            SceneManager.LoadScene("Stab");
        }
    }
}
