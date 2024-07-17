using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotateSpeedAuthoring : MonoBehaviour
{
    public float rotationSpeed;

    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            //Dynamic = moving object for exemple
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            //Add the component runtime
            AddComponent(entity, new RotateSpeed {
                rotationSpeed = authoring.rotationSpeed,
            });
        }
    }
}
