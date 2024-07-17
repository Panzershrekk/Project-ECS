using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnCubeSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubeConfig>();
    }

    protected override void OnUpdate()
    {
        this.Enabled = false; //Will be disable after the first loop of the Update
        //GetSingleton will fire error if there is 0 or more than 1 component present
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();
        for (int i = 0; i < spawnCubeConfig.amountToSpawn; i++)
        {
           Entity spawnedEntity = EntityManager.Instantiate(spawnCubeConfig.cubePrefabEntity);
           //SystemAPI -> better performence
           SystemAPI.SetComponent(spawnedEntity, new LocalTransform {
            Position = new float3(UnityEngine.Random.Range(-10f, +5f), 1.5f, UnityEngine.Random.Range(-4f, +7f)),
            Rotation = quaternion.identity,
            Scale = 1, //Scaled is defaulted to 0 
           });
        }
    }
}
