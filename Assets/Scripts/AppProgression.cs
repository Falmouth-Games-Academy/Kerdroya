using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class AppProgression : MonoBehaviour
{
    public bool openingwatched = false;
    public bool[] levelCompleted = new bool[12];
    public DisplayProgress display;

    private static AppProgression _instance;
    public static AppProgression Instance { get { return _instance; } }

    // Prevent this object from being destroyed between scenes loading.
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void UpdateLevelCompleted(int levelIndex)
    {
        levelCompleted[levelIndex] = true;
        SaveGame();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        foreach (bool CompletedLevel in levelCompleted)
        {
            save.completedSections.Add(CompletedLevel);
        }

        save.openingWatched = openingwatched;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public bool LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < save.completedSections.Count; i++)
            {
                levelCompleted[i] = save.completedSections[i];
            }

            openingwatched = save.openingWatched;

            Debug.Log("Game Loaded");

            display.Display();

            return true;
        }

        else
        {
            Debug.Log("No game saved!");
            return false;
        }
    }

}
