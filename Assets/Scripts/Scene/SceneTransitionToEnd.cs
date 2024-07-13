using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionToEnd : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }
}
