using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerShooterAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerShooterAuthoring>
    {
        public override void Bake(PlayerShooterAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerShooter());
        }
    }
}

public struct PlayerShooter : IComponentData //Tag
{

}