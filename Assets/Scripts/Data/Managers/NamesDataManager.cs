using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesDataManager : Singleton<NamesDataManager>
{
    private NamesData namesData;

    public NamesData NamesData
    {
        get
        {
            if (namesData == null) {
                var json = Resources.Load<TextAsset>("NamesData.json").text;
                namesData = JsonUtility.FromJson<NamesData>(json);
            }
            
            return namesData;
        }
    }

    public string GetName(string objectType) {
        foreach (var item in NamesData.names) {
            if (item.ObjectType == objectType) {
                return item.Name;
            }
        }
        throw new Exception("Нет такого объекта в JSON!");
    }
}
