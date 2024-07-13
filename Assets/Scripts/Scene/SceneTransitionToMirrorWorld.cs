using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionToMirrorWorld : MonoBehaviour
{
    private bool ontriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ontriggered = true;
    }

    private void Update()
    {
        if(BoxObject.isTransition && ontriggered)
        {
            SceneManager.LoadScene("MirrorWorld");
        }
    }
}
