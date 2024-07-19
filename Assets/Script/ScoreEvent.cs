using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ScoreEvent : IComponentData
{
    public Entity Target;
}