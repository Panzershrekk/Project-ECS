using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ProjectileSpawnerConfigAuthoring : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    private class Baker : Baker<ProjectileSpawnerConfigAuthoring>
    {
        public override void Bake(ProjectileSpawnerConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ProjectileSpawnerConfig
            {
                projectilePrefabEntity = GetEntity(authoring._projectilePrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct ProjectileSpawnerConfig : IComponentData
{
    public Entity projectilePrefabEntity;

}
