using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/player.jvm";
        using (var stream = new FileStream(path, FileMode.Create)) {
            var data = new PlayerData(player);
            formatter.Serialize(stream, data);
        }
    }

    public static PlayerData LoadPlayer()
    {
        var path = Application.persistentDataPath + "/player.jvm";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            PlayerData data;
            using (var stream = new FileStream(path, FileMode.Open)) {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
