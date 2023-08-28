using Mono.Data.Sqlite;
using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public struct DataResult
{
    public string name;
    public int score;
    public int collectedStars;
}
public class StageDB : MonoBehaviour
{
    string dir = "/stageData.db";
    public IDbConnection db;

    string table = "StageData";

    private void Awake()
    {
        StartCoroutine(DBCreate());
    }
    private void Start()
    {
        try
        {
            db = new SqliteConnection(GetPath());
            db.Open();
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    
    IEnumerator DBCreate()
    {
        string filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android) // SOS
        {
            filepath = Application.persistentDataPath + dir;
            if (!File.Exists(filepath))
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets" + dir);
                unityWebRequest.downloadedBytes.ToString();
                yield return unityWebRequest.SendWebRequest().isDone;
                File.WriteAllBytes(filepath, unityWebRequest.downloadHandler.data);
            }
        } else
        {
            filepath = Application.dataPath + dir;
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + dir, filepath);
            }
        }
    }

    public string GetPath()
    {
        string str = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            str = "data source = " + Application.persistentDataPath + dir;
        } else
        {
            str = "data source = " + Application.dataPath + dir;
        }

        return str;
    }

public bool ResultIsNull(DataResult result)
    {
        if (result.name == null ||
            result.name.Length < 1) return false;

        return true;
    }

    public DataResult GetDataByName(string name)
    {
        var command = db.CreateCommand();
        command.CommandText = "SELECT * FROM " + table + " WHERE name = '" + name + "'";

        var data = command.ExecuteReader();

        DataResult result = new DataResult();

        while (data.Read())
        {
            result.name = data.GetString(0);
            result.score = data.GetInt32(1);
            result.collectedStars = data.GetInt32(2);
        }

        data.Close();

        return result;
    }

    public void UpdateObjectData(string name, int score, int collectedStars, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET name = '" + name + "', score = " + score + ", collectedStars = " + collectedStars + " WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void UpdateIntData(string key, int value, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET " + key + " = '" + value + "' WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void UpdateStrData(string key, string value, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET " + key + " = '" + value + "' WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void InsertData(string name, int score, int collectedStars)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "INSERT INTO " + table + " (name, score, collectedStars) VALUES ('" + name + "', " + score + ", " + collectedStars + ")";
        command.ExecuteNonQuery();
        command.Dispose();
    }

    public bool HasData(string name)
    {
        var command = db.CreateCommand();
        command.CommandText = "SELECT EXISTS (SELECT * FROM " + table + " WHERE name = '" + name + "')";

        var data = command.ExecuteReader();

        bool forReturn = false;

        while (data.Read())
        {
            forReturn = data.GetInt32(0) == 1;
        }

        data.Close();

        return forReturn;
    }

    public void Close()
    {
        if (db.State == ConnectionState.Open)
        {
            this.db.Close();
        }
    }
}
