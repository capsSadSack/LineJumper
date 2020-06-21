using UnityEngine;

public class SimplePickUpSpawner : APickUpSpawner
{
    public SimplePickUpSpawner(Transform parent)
        : base(parent)
    { }

    public override GameObject CreateSpawnObject()
    {
        var source = Resources.Load("Prefabs/NuclearBomb");
        GameObject objSource = GameObject.Instantiate(source, new Vector3(), new Quaternion()) as GameObject;

        return objSource;
    }

    public override Vector2 GetSpawnPosition()
    {
        float xMagniude = 3.2f;
        float x = UnityEngine.Random.Range(-xMagniude, xMagniude);

        return new Vector2(x, 10.0f);
    }
}
