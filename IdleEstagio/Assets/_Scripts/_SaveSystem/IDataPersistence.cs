using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface usada por todos os objetos passivos de save / load
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
