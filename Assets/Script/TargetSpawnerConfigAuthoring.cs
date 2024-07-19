using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetSpawnerConfigAuthoring : MonoBehaviour
{
    [SerializeField] GameObject _targetPrefab;
    [SerializeField] int _numberToSpawn;
    [SerializeField] int _xLimit;
    [SerializeField] int _yLimit;
    [SerializeField] float2 _zLimit;

    private class Baker : Baker<TargetSpawnerConfigAuthoring>
    {
        public override void Bake(TargetSpawnerConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TargetSpawnerConfig
            {
                targetPrefabEntity = GetEntity(authoring._targetPrefab, TransformUsageFlags.Dynamic),
                numberToSpawn = authoring._numberToSpawn,
                xLimit = authoring._xLimit,
                yLimit = authoring._yLimit,
                zLimit = authoring._zLimit,
            });
        }
    }
}

public struct TargetSpawnerConfig : IComponentData
{
    public Entity targetPrefabEntity;
    public int numberToSpawn;
    public int xLimit;
    public int yLimit;
    public float2 zLimit;
}
