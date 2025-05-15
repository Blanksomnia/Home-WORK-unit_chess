using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndLevel : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int levelEnd;
    [SerializeField] Level level;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player.transform)
        {
            if(level.levelCompleted < levelEnd)
                level.levelCompleted = levelEnd;       
            
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
