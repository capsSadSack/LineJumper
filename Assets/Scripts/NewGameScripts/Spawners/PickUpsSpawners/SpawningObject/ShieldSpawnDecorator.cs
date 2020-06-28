using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ShieldSpawnDecorator : APickUpSpawnerDecorator
{
    private System.Random rand;
    private PlayerController player;

    public ShieldSpawnDecorator(APickUpSpawner spawner, Transform parent, Action onPickUp, PlayerController player)
        : base(spawner, parent, onPickUp)
    {
        this.player = player;
        this.rand = new System.Random();
    }

    public override GameObject CreateSpawnObject()
    {
        var source = Resources.Load("Prefabs/ShieldPickUp");
        GameObject objSource = GameObject.Instantiate(source, new Vector3(), new Quaternion()) as GameObject;

        UnityAction<int> setPlayerShield = player.transform.GetChild(2).GetComponent<ShieldBehaviour>().IncrementShield;
        var shieldBehaviour = objSource.GetComponent<ShieldPickUpBehaviour>();
        shieldBehaviour.OnShieldPickedUp.AddListener(setPlayerShield);
        shieldBehaviour.SetShieldLevel(GetShieldLevel());
        return objSource;
    }

    private int GetShieldLevel()
    {
        return rand.Next(1, 4);
    }
}
