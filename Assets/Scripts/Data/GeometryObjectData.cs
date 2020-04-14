using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeometryObjectData", menuName = "ScriptableObjects/GeometryObjectData", order = 1)]
public class GeometryObjectData : ScriptableObject
{
    public List<ClickColorData> ClicksData;
}

[Serializable]
public struct ClickColorData
{
    public string ObjectType;
    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
}
