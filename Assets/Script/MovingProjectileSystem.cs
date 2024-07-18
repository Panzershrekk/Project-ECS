using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Physics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using Unity.VisualScripting;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
[BurstCompile]
public partial class MovingProjectileSystem : SystemBase
{
    private ComponentLookup<Projectile> projectile;
    private ComponentLookup<Target> target;

    protected override void OnCreate()
    {
        projectile = GetComponentLookup<Projectile>(false);
        target = GetComponentLookup<Target>(false);

    }

    protected override void OnUpdate()
    {
        projectile.Update(this);
        target.Update(this);

        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        foreach (MovingProjectileAspect movingProjectileAspect in SystemAPI.Query<MovingProjectileAspect>())
        {
            movingProjectileAspect.MoveToward(SystemAPI.Time.DeltaTime);
        }

        SimulationSingleton simulation = SystemAPI.GetSingleton<SimulationSingleton>();

        var jobHandle = new ProjectileHitJob
        {
            //ecb = ecb.CreateCommandBuffer(unman),
            projectile = this.projectile,
            target = this.target,
        }.Schedule(simulation, Dependency);

        Dependency = jobHandle;
    }

    [BurstCompile]
    struct ProjectileHitJob : ITriggerEventsJob
    {
        public EntityCommandBuffer ecb;
        [ReadOnly] public ComponentLookup<Projectile> projectile;
        [ReadOnly] public ComponentLookup<Target> target;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity p = Entity.Null;
            Entity t = Entity.Null;

            if (projectile.HasComponent(triggerEvent.EntityA))
            {
                p = triggerEvent.EntityA;
            }
            if (projectile.HasComponent(triggerEvent.EntityB))
            {
                p = triggerEvent.EntityB;
            }
            if (target.HasComponent(triggerEvent.EntityA))
            {
                t = triggerEvent.EntityA;
            }
            if (target.HasComponent(triggerEvent.EntityB))
            {
                t = triggerEvent.EntityB;
            }

            if (p == Entity.Null || t == Entity.Null) return;

            Debug.Log("SKia");
            //ecb.DestroyEntity(0, p);
        }
    }
}
