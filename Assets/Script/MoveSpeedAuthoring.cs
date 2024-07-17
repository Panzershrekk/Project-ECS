using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    private class Baker : Baker<MoveSpeedAuthoring>
    {
        public override void Bake(MoveSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MoveSpeed
            {
                speed = authoring._moveSpeed,
            });
        }
    }
}

public struct MoveSpeed : IComponentData
{
    public float speed;
}