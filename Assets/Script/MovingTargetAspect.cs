using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MovingTargetAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<MoveSpeed> moveSpeed;
    public readonly RefRW<Direction> direction;
    public readonly RefRO<ArenaLimit> arenaLimit;

    public void MoveToward(float deltaTime)
    {
        float3 moveDelta = direction.ValueRO.direction * moveSpeed.ValueRO.speed * deltaTime;
        localTransform.ValueRW.Position += moveDelta;
        if (localTransform.ValueRO.Position.x < arenaLimit.ValueRO.xLimit.x || localTransform.ValueRO.Position.x > arenaLimit.ValueRO.xLimit.y)
        {
            if (localTransform.ValueRO.Position.x < arenaLimit.ValueRO.xLimit.x)
            {
                localTransform.ValueRW.Position.x = arenaLimit.ValueRO.xLimit.x;
            }
            if (localTransform.ValueRO.Position.x > arenaLimit.ValueRO.xLimit.y)
            {
                localTransform.ValueRW.Position.x = arenaLimit.ValueRO.xLimit.y;
            }
            direction.ValueRW.direction.x = -direction.ValueRO.direction.x;
        }
        if (localTransform.ValueRO.Position.y < arenaLimit.ValueRO.yLimit.x || localTransform.ValueRO.Position.y > arenaLimit.ValueRO.yLimit.y)
        {
            if (localTransform.ValueRO.Position.y < arenaLimit.ValueRO.yLimit.x)
            {
                localTransform.ValueRW.Position.y = arenaLimit.ValueRO.yLimit.x;
            }
            if (localTransform.ValueRO.Position.y > arenaLimit.ValueRO.yLimit.y)
            {
                localTransform.ValueRW.Position.y = arenaLimit.ValueRO.yLimit.y;
            }
            direction.ValueRW.direction.y = -direction.ValueRO.direction.y;
        }
    }
}
