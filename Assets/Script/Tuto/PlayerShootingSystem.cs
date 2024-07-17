using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//ISystem is for unmanaged type (int, float, etc...)
//System base is for managed type (Gameobject)
public partial class PlayerShootingSystem : SystemBase
{
    public event EventHandler OnShoot;

    protected override void OnCreate()
    {
        RequireForUpdate<Player>();
    }

    protected override void OnUpdate()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);
        }
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();

        //Unity.Collections.Allocator.Temp //Temp will be disposed when it's not needed anymore
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator); //Will be disposed at the end of the frame
        //It will store command in the foreach
        //With entity Access = if you want to get the entity (tuple)
        foreach ((RefRO<LocalTransform> localtransform, Entity entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>().WithEntityAccess())
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(spawnCubeConfig.cubePrefabEntity);
            //storing the command
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = localtransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1, //Scaled is defaulted to 0 
            });
            OnShoot?.Invoke(entity, EventArgs.Empty);
            //Accessing monobehaviour
            PlayerShootManager.Instance.PlayerShoot(localtransform.ValueRO.Position);
        }
        //Playing the command
        entityCommandBuffer.Playback(EntityManager);
    }
}
