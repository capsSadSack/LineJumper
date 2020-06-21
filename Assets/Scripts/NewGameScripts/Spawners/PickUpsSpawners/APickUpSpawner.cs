using UnityEngine;

public abstract class APickUpSpawner
{
    private Transform parent;

    public APickUpSpawner(Transform parent)
    {
        this.parent = parent;
    }

    public virtual void Spawn()
    {
        GameObject pickUpObject = CreateSpawnObject();
        pickUpObject.transform.SetParent(parent);
        pickUpObject.transform.localScale = new Vector3(25, 25, 1);
        pickUpObject.transform.localPosition = GetSpawnPosition();
    }

    public abstract Vector2 GetSpawnPosition();

    public abstract GameObject CreateSpawnObject();
}
