using System;
using UnityEngine;

public class TrackSet : MonoBehaviour
{
    public TrackData[] trackData;

    public TrackData FindByName(string name)
    {
        return Array.Find(trackData, (v)=> v._name == name);
    }

    public TrackData FindByIndex(int index)
    {
        if (index < 0) return null;
        if (index >= trackData.Length) return null;

        return trackData[index];
    }

    public int IndexByName(string name)
    {
        return Array.IndexOf(trackData, FindByName(name));
    }
}
