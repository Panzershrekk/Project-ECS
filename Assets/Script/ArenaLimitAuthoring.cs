using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ArenaLimitAuthoring : MonoBehaviour
{
    [SerializeField] float2 _xLimit;
    [SerializeField] float2 _yLimit;


    private class Baker : Baker<ArenaLimitAuthoring>
    {
        public override void Bake(ArenaLimitAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ArenaLimit
            {
                xLimit = authoring._xLimit,
                yLimit = authoring._yLimit,
            });
        }
    }
}

public struct ArenaLimit : IComponentData
{
    public float2 xLimit;
    public float2 yLimit;
}
