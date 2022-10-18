using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void CreateUserData(UserInterface ui){
        string path = Application.persistentDataPath + "/user.data";
        if(File.Exists(path)){
            return;
        }else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            Data data = new Data(ui);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
    public static void saveUserData(UserInterface ui){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(ui);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static Data loadUserData(){
        
        string path = Application.persistentDataPath + "/user.data";
        if(File.Exists(path)){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        Data data = formatter.Deserialize(stream) as Data;
        stream.Close();
        return data;
        }else{
            return null;
        }
        
        
    }
}
