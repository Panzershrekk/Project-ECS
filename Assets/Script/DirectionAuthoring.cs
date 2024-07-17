using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DirectionAuthoring : MonoBehaviour
{
    private class Baker : Baker<DirectionAuthoring>
    {
        public override void Bake(DirectionAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Direction
            {
                direction = new float3(Vector3.zero),
            });
        }
    }
}

public struct Direction : IComponentData
{
    public float3 direction;
}