using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class MovingTargetSystem : SystemBase
{
    protected override void OnCreate()
    {
    }

    protected override void OnUpdate()
    {
        foreach (MovingTargetAspect movingTarget in SystemAPI.Query<MovingTargetAspect>())
        {
            movingTarget.MoveToward(SystemAPI.Time.DeltaTime);
        }
    }
}
