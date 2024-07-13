using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Vector3 playerPosition = new Vector3(0, 0, 0); // 保存人物的位置

    // 切换场景前调用
    public void SwitchScene(string sceneName)
    {
        Debug.Log(1);
        // 保存人物的位置信息到本地文件系统中
        playerPosition = this.transform.position; // 保存主角当前位置
        Debug.Log(playerPosition);
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        // 切换到指定的场景
        SceneManager.LoadScene(sceneName);
    }

    // 切换回原来的场景时调用
    public void SwitchBack()
    {
        float x = PlayerPrefs.GetFloat("PlayerX"); // 从本地文件系统中读取保存的位置信息
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");
        Debug.Log(new Vector3(x,y,z));  
        this.transform.position = new Vector3(x,y,z); // 将人物放回原来的位置
    }
}
