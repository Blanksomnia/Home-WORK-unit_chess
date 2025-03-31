using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterAnimatorListener
{
    public void stay();
    public void move();
    public void collect();
    public void attack();
    public void StartDeathAnim();
}
