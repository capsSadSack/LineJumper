using UnityEngine;

public class BottomSpawnDecorator : APickUpSpawnerDecorator
{
    public BottomSpawnDecorator(APickUpSpawner spawner, Transform parent)
        : base(spawner, parent)
    { }

    public override Vector2 GetSpawnPosition()
    {
        float xMagniude = 375.0f;
        float x = UnityEngine.Random.Range(-xMagniude, xMagniude);

        return new Vector2(x, -1000.0f);
    }
}
