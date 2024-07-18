using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public class ProjectileTag : MonoBehaviour
{
    private class Baker : Baker<ProjectileTag>
    {
        public override void Bake(ProjectileTag authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Projectile());
        }
    }
}

public struct Projectile : IComponentData { }
