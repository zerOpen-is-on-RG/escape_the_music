using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public struct DataResult
{
    public string name;
    public int score;
    public int collectedStars;
}
public class StageDB : MonoBehaviour
{
    string dir = "/stageData.db";
    IDbConnection db;

    string table = "StageData";

    private void Awake()
    {
        int num = FindObjectsOfType<StageDB>().Length;
        if (num != 1)
        {
            Destroy(this.gameObject);
        } else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        string connection = "data source = " + Application.streamingAssetsPath + dir;
        Debug.Log("data source = " + Application.streamingAssetsPath + dir);
        db = new SqliteConnection(connection);

        db.Open();
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
        Debug.Log(command.CommandText);

        var data = command.ExecuteReader();

        bool forReturn = false;

        while (data.Read())
        {
            forReturn = data.GetInt32(0) == 1;
        }

        data.Close();

        return forReturn;
    }

    private void OnApplicationQuit()
    {
        if (db == null) return;
        db.Close();
    }
}
