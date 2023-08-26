using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public struct DataResult
{
    public string name;
    public int score;
    public int collectedStars;
    public int difficulty;
}
public class StageDB : MonoBehaviour
{
    string dir = "./stageData.db";
    IDbConnection db;

    string table = "StageData";

    void Start()
    {
        string connection = "URL=file:" + Application.streamingAssetsPath + dir;
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
            result.difficulty = data.GetInt32(3);
        }

        data.Close();

        return result;
    }

    public void UpdateObjectData(string name, int score, int collectedStars, int difficulty, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET name = '" + name + "', score = " + score + ", collectedStars = " + collectedStars + ", difficulty = " + difficulty + " WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void UpdateIntData(string key, int value, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET " + key + " = '" + value + "', WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void UpdateStrData(string key, string value, string whereName)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "UPDATE " + table + " SET " + key + " = '" + value + "', WHERE name = '" + whereName + "'";

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void InsertData(string name, int score, int collectedStars, int difficulty)
    {
        var command = db.CreateCommand();
        command.CommandText =
            "INSERT INTO " + table + " (name, score, collectedStars, difficulty) VALUES ('" + name + "', " + score + ", " + collectedStars + ", " + difficulty + ")";
        command.ExecuteNonQuery();
        command.Dispose();
    }

    public bool HasData(string name)
    {
        var command = db.CreateCommand();
        command.CommandText = "SELECT EXISTS (SELECT * FROM " + table + " WHERE '" + name + "'";

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
        db.Close();
    }
}
