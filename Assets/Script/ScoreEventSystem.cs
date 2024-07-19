using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class ScoreEventSystem : SystemBase
{
    protected override void OnUpdate()
    {

        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.ForEach((Entity entity, ref ScoreEvent scoreEvent) =>
        {
            ScoreManager.Instance.TargetHit();
            ecb.DestroyEntity(entity);
        }).WithoutBurst().Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}