using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerMemories : MonoBehaviour
{
    [SerializeField] PlayerMemory player;
    private void Awake()
    {

        foreach (Bonus bonus in gameObject.GetComponentsInChildren<Bonus>())
        {
            bonus.player = player;
        }
    }
}
