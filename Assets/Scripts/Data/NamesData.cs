using System;

[Serializable]
public class NamesData
{
    public NameData[] names;
}

[Serializable]
public class NameData
{
    public string ObjectType;
    public string Name;
}