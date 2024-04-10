using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        patrolState = new PigPatroState();
        chaseState = new PigChaseState();
    }
}
