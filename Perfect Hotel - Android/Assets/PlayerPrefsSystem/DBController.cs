using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DBController : NauxUtils.Singleton<DBController>
{
    #region Default

    protected override void CustomAwake()
    {
        Initializing();
    }

    void CheckDependency(string key, UnityAction<string> onComplete)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            onComplete?.Invoke(key);
        }
    }

    void Save<T>(string key, T values)
    {
        if (typeof(T) == typeof(int) ||
            typeof(T) == typeof(bool) ||
            typeof(T) == typeof(string) ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(Quaternion) ||
            typeof(T) == typeof(Vector2) ||
            typeof(T) == typeof(Vector3) ||
            typeof(T) == typeof(Vector2Int) ||
            typeof(T) == typeof(Vector3Int))
        {
            PlayerPrefs.SetString(key, $"{values}");
        }
        else
        {
            try
            {
                string json = JsonUtility.ToJson(values);
                PlayerPrefs.SetString(key, json);
            }
            catch (UnityException e)
            {
                throw new UnityException(e.Message);
            }
        }
    }

    T LoadDataByKey<T>(string key)
    {
        if (typeof(T) == typeof(int) ||
            typeof(T) == typeof(bool) ||
            typeof(T) == typeof(string) ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(Quaternion) ||
            typeof(T) == typeof(Vector2) ||
            typeof(T) == typeof(Vector3) ||
            typeof(T) == typeof(Vector2Int) ||
            typeof(T) == typeof(Vector3Int))
        {
            var value = PlayerPrefs.GetString(key);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        else
        {
            var json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(json);
        }
    }

    #endregion

    private int money;
    public int MONEY
    {
        get => money;
        set
        {
            money = value;
            Save(DBKey.MONEY, value);
        }
    }

    private FloorData floorData;
    public FloorData FLOOR_DATA
    {
        get => floorData;
        set
        {
            floorData = value;
            Save(DBKey.FLOOR_DATA, value);
        }
    }

    void Initializing()
    {

        CheckDependency(DBKey.MONEY, key => MONEY = 50);

        CheckDependency(DBKey.FLOOR_DATA, key =>
        {
        var _temp = new FloorData();
            _temp.lstRoomState = new List<RoomInfo>();
            for (int i = 0; i < 4; i++)
            {
                _temp.lstRoomState.Add(new RoomInfo());
                _temp.lstRoomState[i].roomId = i;
                _temp.lstRoomState[i].isUnlock = false;
                _temp.lstRoomState[i].level = 1;
            }

            _temp.lstRoomState[0].isUnlock = true;

            FLOOR_DATA = _temp;
        });
        Load();
    }

    void Load()
    {
        money = LoadDataByKey<int>(DBKey.MONEY);
        floorData = LoadDataByKey<FloorData>(DBKey.FLOOR_DATA);
    }
}

public class DBKey
{
    public static readonly string MONEY = "MONEY";
    public static readonly string FLOOR_DATA = "FLOOR_DATA";
}