using UnityEngine;

public abstract class APickUpSpawnerDecorator : APickUpSpawner
{
    private APickUpSpawner pickUpSpawner { get; set; }

    public APickUpSpawnerDecorator(APickUpSpawner pickUpSpawner, Transform parent)
        : base(parent)
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
