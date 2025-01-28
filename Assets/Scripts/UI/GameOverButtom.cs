using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtom : MonoBehaviour
{
    [SerializeField] Init saver;
    public void ClickOn()
    {
        saver.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
