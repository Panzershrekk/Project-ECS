using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class LifeTimeAuthoring : MonoBehaviour
{
    [SerializeField] float _lifeTime;
    private class Baker : Baker<LifeTimeAuthoring>
    {
        public override void Bake(LifeTimeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new LifeTime
            {
                lifeTime = authoring._lifeTime,
            });
        }
    }
}

public struct LifeTime : IComponentData
{
    public float lifeTime;
}