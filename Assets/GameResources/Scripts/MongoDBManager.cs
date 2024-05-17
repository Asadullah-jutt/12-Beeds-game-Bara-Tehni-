using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Collections;
using System;
// using Random = UnityEngine.Random;

public class MongoDBManager : MonoBehaviour
{
    private static MongoDBManager _instance;
    public static MongoDBManager Instance { get { return _instance; } }
    // private MongoClient client;
    // private IMongoDatabase database;
    // private IMongoCollection<BsonDocument> ingamedataCollection;

    string keytobechecked = "key1";

    bool found = false ;
    

    // Define your 2D array
    // public static int[,] boardValues = new int[5, 5];

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

    private void Start()
    {
        // Replace connection string and database name with your MongoDB configuration
        // string connectionString = "mongodb+srv://Asad:QUUtkhndShz8jlhl@cluster0.brrfoeg.mongodb.net/";
        // string databaseName = "mydatabase";
        // string collectionName = "ingamedata";

        // // Connect to MongoDB
        // client = new MongoClient(connectionString);
        // database = client.GetDatabase(databaseName);
        // ingamedataCollection = database.GetCollection<BsonDocument>(collectionName);


        // init();

        // Create and insert data into MongoDB
        // InsertInGameData(2);
    }


      
    private BsonArray ConvertToBsonArray(List<(int, int)> path)
    {
        BsonArray bsonArray = new BsonArray();

        for(int l = 0 ; l < path.Count  ; l++)
        {
            // Debug.Log(path[l].Item1+" , "+path[l].Item2+" ==> "+path[l+1].Item1+" , "+path[l+1].Item2);
            bsonArray.Add(path[l].Item1);
            bsonArray.Add(path[l].Item2);

            // MoveBeadFromTo(path[l].Item1,path[l].Item2,path[l+1].Item1,path[l+1].Item2);
        }
       
        // for (int i = 0; i < array.GetLength(0); i++)
        // {
           
        //     for (int j = 0; j < array.GetLength(1); j++)
        //     {
        //         bsonArray.Add(array[i, j]);
        //     }
           
        // }

        return bsonArray;
    }

    public void InsertInGameData(int count, List<(int, int)> path)
    {
        BsonArray newBsonArray = ConvertToBsonArray(path);

        // Find the document with the specified key
        var filter = Builders<BsonDocument>.Filter.Eq("key", UIMongodb.Instance.keytobechecked);
        var existingDocument = UIMongodb.Instance.ingamedataCollection.Find(filter).FirstOrDefault();

        if (existingDocument != null)
        {
            // Add the new array next to the existing one
            existingDocument.Add("key"+count, newBsonArray);

            // Replace the existing document with the updated one
            UIMongodb.Instance.ingamedataCollection.ReplaceOne(filter, existingDocument);

            keytobechecked = "key" + (count+1);

            Invoke("checknextturn" , 2f);
            

        }

        Debug.Log("Data inserted successfully into ingamedata collection.");
    }

    public void checknextturn()
    {
        // Debug.Log("sdsdsdsdvs");
        if(found == false)
        {
            Invoke("FetchLastInsertedArray" , 2f);
            Debug.Log("NOT STOP.");

        }
    }


 
    void FetchLastInsertedArray()
    {

        // Query to retrieve the document
        var filter = Builders<BsonDocument>.Filter.Eq("key", UIMongodb.Instance.keytobechecked);
        var document = UIMongodb.Instance.ingamedataCollection.Find(filter).FirstOrDefault();

          string jsonDocument = document.ToJson();
           Debug.Log(UIMongodb.Instance.keytobechecked);
        //    Debug.Log(jsonDocument);

        if (document != null)
        {
            // Attempt to extract the array from "key10" if it exists
            if (document.Contains(keytobechecked))
            {
                BsonArray array = document[keytobechecked].AsBsonArray;
                Debug.Log("document found in the collection.");

                // found = true ;
                GameManager.SetBoardValues(array);
                found = false ;
                return ;
            
            }
            else
            {
                // Fallback: Choose another key or handle the absence of key10
                Debug.Log("The key "+ keytobechecked + " not found");
               
                // Debug.LogWarning("The key 'key10' does not exist in the document.");
                // checknextturn();
            
            }
            Debug.LogError("document ....... in the collection.");

        }
        else
        {
            Debug.Log("The key "+ keytobechecked + " not found");
            // checknextturn();
        }
        checknextturn();
    
}
 

}
