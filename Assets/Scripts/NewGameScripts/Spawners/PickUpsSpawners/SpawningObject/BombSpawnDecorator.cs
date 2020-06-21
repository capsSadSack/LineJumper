using UnityEngine;

public class BombSpawnDecorator : APickUpSpawnerDecorator
{
    public BombSpawnDecorator(APickUpSpawner spawner, Transform parent)
        : base(spawner, parent)
    { }

    public override GameObject CreateSpawnObject()
    {
        var source = Resources.Load("Prefabs/NuclearBomb");
        GameObject objSource = GameObject.Instantiate(source, new Vector3(), new Quaternion()) as GameObject;

        return objSource;
    }
}
