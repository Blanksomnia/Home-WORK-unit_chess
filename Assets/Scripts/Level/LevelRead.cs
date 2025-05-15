using System.Collections.Generic;
using UnityEngine;


public class LevelRead : MonoBehaviour
{
    [SerializeField] Level level;
    [SerializeField] GameObject buttonPlay;
    [SerializeField] GameObject buttonNewGame;
    [SerializeField] List<GameObject> levelsButton = new List<GameObject>();
    [SerializeField] Color colorClosedLevel;

    private void Awake()
    {
        if(level.levelCompleted == 0)
            buttonPlay.SetActive(false);
        else
        {
            buttonNewGame.SetActive(false);
            for(int i = 0; i < levelsButton.Count; i++)
                if (i > level.levelCompleted)
                {
                    UnityEngine.UI.Button button = levelsButton[i].GetComponent<UnityEngine.UI.Button>();
                    button.targetGraphic.color = colorClosedLevel;
                    button.enabled = false;
                }

        }
    }

}
