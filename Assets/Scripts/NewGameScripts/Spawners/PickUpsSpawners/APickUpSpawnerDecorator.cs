using System;
using UnityEngine;

public abstract class APickUpSpawnerDecorator : APickUpSpawner
{
    private APickUpSpawner pickUpSpawner { get; set; }

    public APickUpSpawnerDecorator(APickUpSpawner pickUpSpawner, Transform parent, Action onPickUp)
        : base(parent, onPickUp)
    {
        this.pickUpSpawner = pickUpSpawner;
    }

    public override Vector2 GetSpawnPosition()
    {
        return pickUpSpawner.GetSpawnPosition();
    }

    public override GameObject CreateSpawnObject()
    {
        return pickUpSpawner.CreateSpawnObject();
    }
}
