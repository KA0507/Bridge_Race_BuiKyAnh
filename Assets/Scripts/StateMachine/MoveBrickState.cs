using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;

public class MoveBrickState : IState
{
    Vector3 closestBrick;
    int randomBrick;
    public void OnEnter(Enemy enemy)
    {
        randomBrick = Random.Range(5, 15);
    }

    // Tìm đủ gạch chuyển sang đi lên bridge
    public void OnExecute(Enemy enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_RUN);
        if (enemy.StackBrick.CountBrick < randomBrick)
        {
            closestBrick = enemy.CurrentStage.FindClosestBrick(enemy.CharacterColor, enemy.transform.position);
            if (closestBrick != null)
            {
                enemy.navMeshAgent.SetDestination(closestBrick);
                enemy.ChangeAnim(Constant.ANIM_RUN);
            }
        } else
        {
            enemy.ChangeState(new MoveBridgeState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
