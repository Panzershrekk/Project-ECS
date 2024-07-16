using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//BURST COMPILE ONLY WORK ON UNMANAGED TYPE (int, float, etc..)
//On profiler -> if it's green, it's burst compiler

public partial struct RotatingCubeSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        //This means we need a RotateSpeed GO in the scene before running update
        state.RequireForUpdate<RotateSpeed>();
    }

    /********** 
    Note: RefRO -> ReadOnly can be on multiple jobs concurently
          RefRW -> ReadWrite only one job can run at the same time
    **********/
    [BurstCompile] //Compile with burst
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false; //Disable the system
        return;
        foreach ((RefRW<LocalTransform>localTransform, RefRO<RotateSpeed> rotateSpeed) 
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>())
        {
            //Value RW -> value we want to change (ReadWrite)
            //Value RO -> value we want to read (ReadOnly)
            // !!!!!!! IMPORTANT: FOR TIME WE WANT TO USE SystemAPI.Time.DeltaTime AND NOT TIME.DELTATIME !!!!!! //
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.rotationSpeed * SystemAPI.Time.DeltaTime);
        }
        RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        //Schedule = run on one thread in the future depending on which one is busy, complete the entire job in 1 thread
        //Schedule parallel = Split job in multple chunks and create multiple job that run on multiple thread
        //rotatingCubeJob.Schedule();
        rotatingCubeJob.ScheduleParallel(); //Work better with a great amount of entity

    }

    [BurstCompile]
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;
        // Ref -> reference
        // In -> reference but lecture only
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            localTransform = localTransform.RotateY(rotateSpeed.rotationSpeed * deltaTime);
        }
    }
}
