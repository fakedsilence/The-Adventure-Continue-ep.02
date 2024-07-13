using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Vector3 playerPosition = new Vector3(0, 0, 0); // ���������λ��

    // �л�����ǰ����
    public void SwitchScene(string sceneName)
    {
        Debug.Log(1);
        // ���������λ����Ϣ�������ļ�ϵͳ��
        playerPosition = this.transform.position; // �������ǵ�ǰλ��
        Debug.Log(playerPosition);
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        // �л���ָ���ĳ���
        SceneManager.LoadScene(sceneName);
    }

    // �л���ԭ���ĳ���ʱ����
    public void SwitchBack()
    {
        float x = PlayerPrefs.GetFloat("PlayerX"); // �ӱ����ļ�ϵͳ�ж�ȡ�����λ����Ϣ
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");
        Debug.Log(new Vector3(x,y,z));  
        this.transform.position = new Vector3(x,y,z); // ������Ż�ԭ����λ��
    }
}
