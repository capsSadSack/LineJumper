using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpsSpawner
{
    public GameController gameController;

    private System.Random rand = new System.Random();
    private Transform parentForSpawners;
    private Dictionary<PickUp, APickUpSpawner> pickUpSpawners;


    public PickUpsSpawner(GameObject transformParent, GameController gameController)
    {
        this.parentForSpawners = transformParent.transform;
        this.gameController = gameController;

        InitializePickUpSpawners(gameController.DestroyAllEnemies);
    }

    public void SpawnPickUp()
    {
        PickUp pickUpToSpawn = PickUp.NuclearBomb;//GetRandomPickUp();
        APickUpSpawner spawner = pickUpSpawners[pickUpToSpawn];
        spawner.Spawn();
    }

    private PickUp GetRandomPickUp()
    {
        IEnumerable<PickUp> pickUps = EnumsProcessor.GetAllValues(PickUp.NuclearBomb);
        int index = rand.Next(0, pickUps.Count() - 1);
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

    }
}
