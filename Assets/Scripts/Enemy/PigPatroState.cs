using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPatroState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        //∑¢œ÷player«–ªªµΩchase
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (!currentEnemy.physicCheck.isGround || (currentEnemy.physicCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
            currentEnemy.anim.SetBool("walk", true);
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        Debug.Log("Exit");
    }
}