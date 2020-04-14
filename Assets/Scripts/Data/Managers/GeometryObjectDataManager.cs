using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryObjectDataManager : Singleton<GeometryObjectDataManager>
{
    private GeometryObjectData geometryObjectData;

    public GeometryObjectData GeometryObjectData
    {
        get
        {
            if (geometryObjectData == null) {
                geometryObjectData = Resources.Load<GeometryObjectData>("GeometryObjectData");
            }
            return geometryObjectData;
        }
    }

    public Color GetColor(string objectType, int clicks)
    {
        foreach (var item in GeometryObjectData.ClicksData)
        {
            if (item.ObjectType == objectType)
            {
                if (clicks >= item.MinClicksCount && clicks <= item.MaxClicksCount)
                {
                    return item.Color;
                }
            }
        }
        throw new Exception("Добавьте цветов в админке!");
    }
}
