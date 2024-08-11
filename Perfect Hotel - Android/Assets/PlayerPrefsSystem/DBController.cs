using System;
using UnityEngine;
using UnityEngine.Events;

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
        get => money; set
        {
            money = value;
            Save(DBKey.MONEY, value);
        }
    }

    //    private ListMonthData _calendarData;
    //
    //    public ListMonthData CALENDAR_DATA
    //    {
    //        get => _calendarData;
    //        set
    //        {
    //            _calendarData = value;
    //            Save(DBKey.CALENDAR_DATA, value);
    //        }
    //    }


    void Initializing()
    {

        CheckDependency(DBKey.MONEY, key =>
        {
            MONEY = 50;
        });

        //        CheckDependency(DBKey.CALENDAR_DATA, key =>
        //        {
        //            var temp = new ListMonthData();
        //            temp.lstDataMonth = new List<MonthData>();
        //
        //            for (int i = 0; i < 3; i++)
        //            {
        //                var dayNow = DateTime.Today.AddMonths(-i);
        //                temp.lstDataMonth.Add(new MonthData());
        //                temp.lstDataMonth[i].year = dayNow.Year;
        //                temp.lstDataMonth[i].month = dayNow.Month;
        //                var daysInMonth = DateTime.DaysInMonth(dayNow.Year, dayNow.Month);
        //                temp.lstDataMonth[i].lstStatusDay = new List<bool>();
        //                for (int j = 0; j < daysInMonth; j++)
        //                {
        //                    temp.lstDataMonth[i].lstStatusDay.Add(false);
        //                }
        //            }
        //
        //            CALENDAR_DATA = temp;
        //        });


        Load();
    }

    void Load()
    {
        money = LoadDataByKey<int>(DBKey.MONEY);
        //_calendarData = LoadDataByKey<ListMonthData>(DBKey.CALENDAR_DATA);
    }
}

public class DBKey
{
    public static readonly string MONEY = "MONEY";
    //public static readonly string CALENDAR_DATA = "CALENDAR_DATA";
}