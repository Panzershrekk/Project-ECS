using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MovingProjectileAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<MoveSpeed> moveSpeed;
    public readonly RefRW<Direction> direction;
}
