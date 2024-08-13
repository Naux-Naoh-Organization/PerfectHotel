using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandle : NauxUtils.Singleton<SpawnHandle>
{

    [SerializeField] private List<ObjectPrefab> lstSpawn = new List<ObjectPrefab>();


    ObjectPrefab FindID(SpawnID id)
    {
        ObjectPrefab objectSpawn = null;
        for (int i = 0; i < lstSpawn.Count; i++)
        {
            if (lstSpawn[i].spawnID == id)
            {
                objectSpawn = lstSpawn[i];
            }
        }
        return objectSpawn;
    }

    public GameObject SpawnObj(SpawnID id, Vector3 posSpawn)
    {
        var find = FindID(id);
        GameObject obj = null;
        if (find != null)
        {
            obj = Instantiate(find.prefabSpawn, posSpawn, Quaternion.identity);
        }
        return obj;
    }
}

[Serializable]
public class ObjectPrefab
{
    public SpawnID spawnID;
    public GameObject prefabSpawn;
}

public enum SpawnID
{
    Money = 0,
    EfxClean = 1,
    BotAI = 2,
}
