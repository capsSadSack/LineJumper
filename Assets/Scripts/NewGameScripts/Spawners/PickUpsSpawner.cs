using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpsSpawner
{
    private System.Random rand = new System.Random();
    private Transform parentForSpawners;
    private Dictionary<PickUp, APickUpSpawner> pickUpSpawners;

    public PickUpsSpawner(GameObject transformParent)
    {
        this.parentForSpawners = transformParent.transform;

        InitializePickUpSpawners();
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

    private void InitializePickUpSpawners()
    {
        pickUpSpawners = new Dictionary<PickUp, APickUpSpawner>();

        // Nuclear Bomb
        APickUpSpawner simpleNuclearBombSpawner = new SimplePickUpSpawner(parentForSpawners);
        APickUpSpawner nuclearBombSpawner = new BombSpawnDecorator(simpleNuclearBombSpawner, parentForSpawners);
        APickUpSpawner nuclearBombTopSpawner = new TopSpawnDecorator(nuclearBombSpawner, parentForSpawners);

        pickUpSpawners.Add(PickUp.NuclearBomb, nuclearBombTopSpawner);

        // Shield

    }
}
