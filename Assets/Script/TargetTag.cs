using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public class TargetTag : MonoBehaviour
{
    private class Baker : Baker<TargetTag>
    {
        public override void Bake(TargetTag authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Target());
        }
    }
}

public struct Target : IComponentData { }