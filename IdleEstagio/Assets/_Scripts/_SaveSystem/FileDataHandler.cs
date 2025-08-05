using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Lê e escreve o arquivo no sistema operacional
public class FileDataHandler
{
    private string dataDirectoryPath = "";
    private string dataFileName = "";
    private bool useEncription = false;
    private readonly string encryptionCodeWord = "salamaleco";

    public FileDataHandler(string dataDirectoryPath, string dataFileName, bool useEncryption)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;
        this.useEncription = useEncryption;
    }

    public GameData Load()
    {
        string savePath = Path.Combine(dataDirectoryPath, dataFileName);
        GameData loadedData = null;

        if(File.Exists(savePath))
        {
            try
            {
                string dataToLoad = "";

                //Open the system file
                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    //Read the file
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //Put the string here
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if(useEncription)
                {
                    dataToLoad = EncriptDecript(dataToLoad);
                }

                // Json to C#:
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on loading save from FileDataHandler L32 : " + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        // C:/LocalLow/DefaulCompany/Unity + data.game
        string savePath = Path.Combine(dataDirectoryPath, dataFileName);
        try
        {
            //Create the actual file in the system
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            //Serialize the data to Json

            string dataToStore = JsonUtility.ToJson(data, true);

            if(useEncription)
            {
                dataToStore = EncriptDecript(dataToStore);
            }

            //Write the information to the system file
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error on save system FileDataHandler L62 : " + e);
        }
    }

    private string EncriptDecript(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
