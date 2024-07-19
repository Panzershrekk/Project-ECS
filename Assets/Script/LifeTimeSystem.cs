using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial class LifeTimeSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<LifeTime>();
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        foreach (var (time, entity) in SystemAPI.Query<RefRW<LifeTime>>().WithEntityAccess())
        {
            time.ValueRW.lifeTime -= deltaTime;
            if (time.ValueRO.lifeTime <= 0)
            {
                ecb.DestroyEntity(entity);
            }
        }
    }
}