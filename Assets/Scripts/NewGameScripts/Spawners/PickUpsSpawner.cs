using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PickUpsSpawner
{
    private System.Random rand = new System.Random();
    private GameObject transformParent;

    public PickUpsSpawner(GameObject transformParent)
    {
        this.transformParent = transformParent;
    }

    public void SpawnPickUp()
    {
        PickUp pickUpToSpawn = GetRandomPickUp();
    }

    private PickUp GetRandomPickUp()
    {
        IEnumerable<PickUp> pickUps = EnumsProcessor.GetAllValues(PickUp.NuclearBomb);
        int index = rand.Next(0, pickUps.Count() - 1);
        return pickUps.ElementAt(index);
    }

}
