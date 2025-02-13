using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnClickDownloadScene : MonoBehaviour
{
    [SerializeField] string nameScene = "MainMenu";


    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }


}
