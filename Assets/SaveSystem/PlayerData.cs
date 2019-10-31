using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class PlayerData
{
    public string level;
    public int Score;
    public float[] position;

    public PlayerData(Player player)
    {
        level = player.level;
        Score = player.Score;

        position = new float[3];
        var position1 = player.transform.position;
        position[0] = position1.x;
        position[1] = position1.y;
        position[2] = position1.z;
    }
}
