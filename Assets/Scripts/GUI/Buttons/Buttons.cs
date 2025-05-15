using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void LoadScene(int ID)
    {
        SceneManager.LoadScene(ID);
    }

    public void GameobjectsClose(GameObject close)
    {
        close.SetActive(false);     
    }

    public void GameobjectsOpen(GameObject open)
    {
        open.SetActive(true);
    }


    public void AudioPlay(AudioSource source)
    {
        source.Play();
    }
}
