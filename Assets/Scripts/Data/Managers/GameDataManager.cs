using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    private GameData gameData;

    public GameData GameData
    {
        get
        {
            if (gameData == null)
            {
                gameData = Resources.Load<GameData>("GameData");
            }
            return gameData;
        }
    }
}
