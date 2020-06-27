using System;
using UnityEngine;
using UnityEngine.Events;

public class BombSpawnDecorator : APickUpSpawnerDecorator
{
    public BombSpawnDecorator(APickUpSpawner spawner, Transform parent, Action onPickUp)
        : base(spawner, parent, onPickUp)
    { }

    public override GameObject CreateSpawnObject()
    {
        var source = Resources.Load("Prefabs/NuclearBomb");
        GameObject objSource = GameObject.Instantiate(source, new Vector3(), new Quaternion()) as GameObject;
        objSource.GetComponent<NuclearBombBehaviour>().OnNuclearBombExplode.AddListener(new UnityAction(onPickUp));
        return objSource;
    }
}
