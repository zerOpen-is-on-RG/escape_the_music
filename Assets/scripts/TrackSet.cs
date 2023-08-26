using System;
using UnityEngine;

public class TrackSet : MonoBehaviour
{
    public TrackData[] trackData;

    public TrackData FindByName(string name)
    {
        return Array.Find(trackData, (v)=> v._name == name);
    }

    public int IndexByName(string name)
    {
        return Array.IndexOf(trackData, FindByName(name));
    }
}
