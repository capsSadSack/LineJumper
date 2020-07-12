﻿using Assets.Scripts.NewGameScripts;
using Assets.Scripts.PickUps;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpsSpawner
{
    public GameController gameController;
    public PlayerController playerController;

    private System.Random rand = new System.Random();
    private Transform parentForSpawners;
    private Dictionary<PickUp, APickUpSpawner> pickUpSpawners;


    public PickUpsSpawner(GameObject transformParent, GameController gameController, PlayerController playerController)
    {
        this.parentForSpawners = transformParent.transform;
        this.gameController = gameController;
        this.playerController = playerController;

        InitializePickUpSpawners(OnNukePickedUp);
    }

    #region Переместить методы в др место!

    private void OnNukePickedUp()
    {
        GameObject nuke = CreateNukeExplosion();
       
        nuke.GetComponent<AudioFinish>().OnFinishSound.AddListener(gameController.DestroyAllEnemies);
    }

    private GameObject CreateNukeExplosion()
    {
        var source = Resources.Load("Prefabs/NukeExplosion");
        var obj = GameObject.Instantiate(source, parentForSpawners, false) as GameObject;
        obj.transform.localScale = new Vector3(192, 192, 1);

        return obj;
    }

    #endregion


    public void SpawnPickUp()
    {
        PickUp pickUpToSpawn = GetRandomPickUp();
        APickUpSpawner spawner = pickUpSpawners[pickUpToSpawn];
        spawner.Spawn();
    }

    private PickUp GetRandomPickUp()
    {
        IEnumerable<PickUp> pickUps = EnumsProcessor.GetAllValues(PickUp.NuclearBomb);
        int index = rand.Next(0, pickUps.Count());
        return pickUps.ElementAt(index);
    }

    private void InitializePickUpSpawners(Action onNuclearBombPickedUp)
    {
        pickUpSpawners = new Dictionary<PickUp, APickUpSpawner>();

        // Nuclear Bomb
        APickUpSpawner simpleNuclearBombSpawner = new SimplePickUpSpawner(parentForSpawners, onNuclearBombPickedUp);
        APickUpSpawner nuclearBombSpawner = new BombSpawnDecorator(simpleNuclearBombSpawner, parentForSpawners, onNuclearBombPickedUp);
        APickUpSpawner nuclearBombTopSpawner = new TopSpawnDecorator(nuclearBombSpawner, parentForSpawners, onNuclearBombPickedUp);

        pickUpSpawners.Add(PickUp.NuclearBomb, nuclearBombTopSpawner);

        // Shield
        APickUpSpawner simpleShieldSpawner = new SimplePickUpSpawner(parentForSpawners, () => { });
        APickUpSpawner shieldSpawner = new ShieldSpawnDecorator(simpleShieldSpawner, parentForSpawners, () => { }, playerController);
        APickUpSpawner shieldTopOrBottomSpawner = new TopOrBottomSpawnDecorator(shieldSpawner, parentForSpawners, () => { });

        pickUpSpawners.Add(PickUp.Shield, shieldTopOrBottomSpawner);

        // Superpower
    }
}
