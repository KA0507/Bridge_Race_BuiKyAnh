using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBridgeState : IState
{
    bool isBridge = false;
    public void OnEnter(Enemy enemy)
    {

    }
    // Đi lên cầu đến khi hết gạch
    public void OnExecute(Enemy enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_RUN);
        enemy.navMeshAgent.SetDestination(enemy.TargetBridge);
        Vector3 positionEnemy = enemy.transform.position;
        //Debug.Log(Mathf.Sqrt((positionEnemy.x - enemy.TargetBridge.x) * (positionEnemy.x - enemy.TargetBridge.x) + (positionEnemy.z - enemy.TargetBridge.z) * (positionEnemy.z - enemy.TargetBridge.z)));
        if (Mathf.Sqrt((positionEnemy.x - enemy.TargetBridge.x)*(positionEnemy.x - enemy.TargetBridge.x) + (positionEnemy.z - enemy.TargetBridge.z)*(positionEnemy.z - enemy.TargetBridge.z)) < 0.2f)
        {

            if (enemy.CurrentStage.Targets.Length == 1)
            {
                enemy.ChangeState(new VictoryState());
            }else
            {
                enemy.ChangeStage();
                enemy.ChangeState(new MoveBrickState());
            }
            isBridge = true;

        } else if (enemy.StackBrick.CountBrick == 0)
        {
            enemy.navMeshAgent.velocity = Vector3.zero;
            enemy.ChangeState(new MoveBrickState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
