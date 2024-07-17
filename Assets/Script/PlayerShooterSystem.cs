using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))] //This will allow for to process before the transform system, meaning the instantiate won't spwan at 0,0,0 for 1 frame
[BurstCompile]
public partial class PlayerShooterSystem : SystemBase
{
    [BurstCompile]
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerShooter>();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ProjectileSpawnerConfig projectileSpawnerConfig = SystemAPI.GetSingleton<ProjectileSpawnerConfig>();
            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            Camera playerCamera = Camera.main;
            float3 mousePosition = Input.mousePosition;
            Ray ray = playerCamera.ScreenPointToRay(mousePosition);
            float rayLength = 100f;
            float3 directionToMouse = math.normalize(ray.direction * rayLength);

            foreach (var localTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<PlayerShooter>())
            {
                Entity spawnedEntity = ecb.Instantiate(projectileSpawnerConfig.projectilePrefabEntity);
                ecb.SetComponent(spawnedEntity, new LocalTransform
                {
                    Position = localTransform.ValueRO.Position,
                    Rotation = quaternion.identity,
                    Scale = 1,
                });
                ecb.SetComponent(spawnedEntity, new Direction
                {
                    direction = directionToMouse
                });
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}
