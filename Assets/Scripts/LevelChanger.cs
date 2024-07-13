using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        FadetoLevel(1);
    //    }    
    //}

    public void FadetoLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    //public void OnFadeComplete()
    //{
    //    SceneManager.LoadScene(levelToLoad + 1);
    //}
}
