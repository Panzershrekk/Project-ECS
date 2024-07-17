using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    public static PlayerShootManager Instance { get; private set;}
    public GameObject shootPopupprefab;

    private void Awake()
    {
        Instance = this;
    }
    /*
    //Logic to access data from system and use it in the "standard monobehaviour way"
        private void Start()
        {
            //GetExistingSystem -> For ISystem
            //GetExistingSystemManaged -> for SystemBase
            //Their is always one system in the world so it's kind of a singleton
            PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
            playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
        }

        private void PlayerShootingSystem_OnShoot(object sender, EventArgs e)
        {
            Entity playerEntity =  (Entity)sender;
            LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
            Instantiate(shootPopupprefab, localTransform.Position, Quaternion.identity);
        }
        */

    public void PlayerShoot(UnityEngine.Vector3 playerPosition)
    {
        Instantiate(shootPopupprefab, playerPosition, UnityEngine.Quaternion.identity);
    }
}
