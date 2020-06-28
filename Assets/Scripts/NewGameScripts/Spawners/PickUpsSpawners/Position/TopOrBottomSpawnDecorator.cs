using System;
using UnityEngine;

public class TopOrBottomSpawnDecorator : APickUpSpawnerDecorator
{
    public TopOrBottomSpawnDecorator(APickUpSpawner spawner, Transform parent, Action onPickUp)
        : base(spawner, parent, onPickUp)
    { }

    public override Vector2 GetSpawnPosition()
    {
        float xMagniude = 300.0f;
        float x = UnityEngine.Random.Range(-xMagniude, xMagniude);

        float y = -1000.0f;
        if (SpawnOnTop())
        {
            y *= -1;
        }

        return new Vector2(x, y);
    }

    private bool SpawnOnTop()
    {
        return UnityEngine.Random.value > 0.5;
    }
}
