using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class TargetSpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<TargetSpawnerConfig>();
    }

    protected override void OnUpdate()
    {
        this.Enabled = false; //Will be disable after the first loop of the Update
        TargetSpawnerConfig spawntargetConfig = SystemAPI.GetSingleton<TargetSpawnerConfig>();
        for (int i = 0; i < spawntargetConfig.numberToSpawn; i++)
        {
            Entity spawnedEntity = EntityManager.Instantiate(spawntargetConfig.targetPrefabEntity);
            //SystemAPI -> better performence
            SystemAPI.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-spawntargetConfig.xLimit, +spawntargetConfig.xLimit),
                                    UnityEngine.Random.Range(-spawntargetConfig.yLimit, +spawntargetConfig.yLimit),
                                    UnityEngine.Random.Range(spawntargetConfig.zLimit.x, spawntargetConfig.zLimit.y)),
                Rotation = quaternion.Euler(new float3(0, math.radians(180f), 0)),
                Scale = 1, //Scaled is defaulted to 0 
            });
            SystemAPI.SetComponent(spawnedEntity, new Direction
            {
                direction = new float3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0)
            });
        }
    }
}
