using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnCubeConfigAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public int numbertoSpawn;

    private class Baker : Baker<SpawnCubeConfigAuthoring>
    {
        public override void Bake(SpawnCubeConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnCubeConfig
            {
                amountToSpawn = authoring.numbertoSpawn,
                cubePrefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
            });
        }
    }


}

//Component
public struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}