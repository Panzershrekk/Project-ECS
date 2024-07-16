using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StunnedAuthoring : MonoBehaviour
{
    private class Baker : Baker<StunnedAuthoring>
    {
        public override void Bake(StunnedAuthoring authoring)
        {
            //Dynamic = moving object for exemple
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            //Add the component runtime
            AddComponent(entity, new Stunned());
            SetComponentEnabled<Stunned>(entity, false); //Set Component enabled or not when baking
        }
    }
}

public struct Stunned : IComponentData, IEnableableComponent
{

}