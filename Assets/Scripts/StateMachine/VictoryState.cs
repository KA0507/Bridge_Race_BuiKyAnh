using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : IState
{
    public void OnEnter(Enemy enemy)
    {
        
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.Victory();
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
