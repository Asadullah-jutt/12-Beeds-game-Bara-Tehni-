using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using TMPro;
public class UIMongodb : MonoBehaviour
{
    public GameObject Notification ;
    public TMP_Text tMP_Text ;
    private static UIMongodb _instance;
    public static UIMongodb Instance { get { return _instance; } }
    public MongoClient client;
    public IMongoDatabase database;
    public IMongoCollection<BsonDocument> ingamedataCollection;

    public string keytobechecked ;

    Boolean a = false ;

    // Start is called before the first frame update

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
        

        

        


        // init();

        // Create and insert data into MongoDB
        // InsertInGameData(2);
    }

    void Connect()
    {
        if(a == true)
            return ;

        a = true ;
        
        const string connectionString = "mongodb+srv://Asad:QUUtkhndShz8jlhl@cluster0.brrfoeg.mongodb.net/";
        const string databaseName = "mydatabase";
        const string collectionName = "ingamedata";

        // Check if the connection string is null or empty
        if (string.IsNullOrEmpty(connectionString))
        {
            Debug.Log("Error: Connection string is null or empty.");
            // return false;
        }

        // Attempt to resolve the DNS manually to check for issues
        // try
        // {
        //     var dnsCheck = Dns.GetHostAddresses("cluster0.brrfoeg.mongodb.net");
        //     Debug.Log("DNS Resolution successful.");
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log($"Error during DNS resolution: {ex.Message}");
        //     return false;
        // }

        // Try connecting to MongoDB and handle exceptions
        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            ingamedataCollection = database.GetCollection<BsonDocument>(collectionName);

            Debug.Log("Successfully connected to MongoDB.");
        }
        catch (TimeoutException ex)
        {
            Debug.Log($"Connection timeout: {ex.Message}");
             Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            // return false;
        }
        catch (MongoConfigurationException ex)
        {
            Debug.Log($"Configuration error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            // return false;
        }
        catch (MongoConnectionException ex)
        {
            Debug.Log($"Connection error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            // return false;
        }
        catch (Exception ex)
        {
            Debug.Log($"An unexpected error occurred: {ex.Message}");
            Notification.SetActive(true);
            tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            // return false;
        }

        
    }


    public bool init(string key)
    {
         // Constants for MongoDB connection
        const string connectionString = "mongodb+srv://Asad:QUUtkhndShz8jlhl@cluster0.brrfoeg.mongodb.net/";
        const string databaseName = "mydatabase";
        const string collectionName = "ingamedata";


        // Check if the connection string is null or empty
        if (string.IsNullOrEmpty(connectionString))
        {
            Debug.Log("Error: Connection string is null or empty.");
            return false;
        }

        // Attempt to resolve the DNS manually to check for issues
        // try
        // {
        //     var dnsCheck = Dns.GetHostAddresses("cluster0.brrfoeg.mongodb.net");
        //     Debug.Log("DNS Resolution successful.");
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log($"Error during DNS resolution: {ex.Message}");
        //     return false;
        // }

        // Try connecting to MongoDB and handle exceptions
        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            ingamedataCollection = database.GetCollection<BsonDocument>(collectionName);
            a = true;
            Debug.Log("Successfully connected to MongoDB.");
        }
        catch (TimeoutException ex)
        {
            Debug.Log($"Connection timeout: {ex.Message}");
             Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        catch (MongoConfigurationException ex)
        {
            Debug.Log($"Configuration error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        catch (MongoConnectionException ex)
        {
            Debug.Log($"Connection error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        // catch (Exception ex)
        // {
        //     Debug.Log($"An unexpected error occurred: {ex.Message}");
        //     Notification.SetActive(true);
        //     tMP_Text.text = ex.Message ;
        //     Invoke("DisableGameObject", 5f);
        //     return false;
        // }

        // Perform a test operation to ensure the connection works
        try
        {
            // Insert a test document to verify connection
        keytobechecked = key ;
        var filter = Builders<BsonDocument>.Filter.Eq("key", key);
        var existingDocument = ingamedataCollection.Find(filter).FirstOrDefault();

        if (existingDocument != null)
        {
           return false ;
        }
        else
        {
            // Construct the document to insert
            var document = new BsonDocument
            {
                { "key", key },
                { "2player", false } // Adding the new attribute with default value
            };


            ingamedataCollection.InsertOne(document);

            Debug.Log("New document inserted successfully into ingamedata collection. with key = " + key);
        }
        }
        catch (Exception ex)
        {
            Notification.SetActive(true);
            tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            Debug.Log("Error during test operation: {ex.Message}");
            return false;
        }
        // Replace connection string and database name with your MongoDB configuration
        

        // Connect to MongoDB
        
        

        return true;
    }

     bool is2PlayerReady = false; 

     bool Get2player()
    {
        return is2PlayerReady ;
    }



    public bool Check1PlayerStatus(string key)
    {
        if(a == false)
        {
        const string connectionString = "mongodb+srv://Asad:QUUtkhndShz8jlhl@cluster0.brrfoeg.mongodb.net/";
        const string databaseName = "mydatabase";
        const string collectionName = "ingamedata";

        // Check if the connection string is null or empty
        if (string.IsNullOrEmpty(connectionString))
        {
            Debug.Log("Error: Connection string is null or empty.");
            
            return false;
        }

        // Attempt to resolve the DNS manually to check for issues
        // try
        // {
        //     var dnsCheck = Dns.GetHostAddresses("cluster0.brrfoeg.mongodb.net");
        //     Debug.Log("DNS Resolution successful.");
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log($"Error during DNS resolution: {ex.Message}");
        //     return false;
        // }

        // Try connecting to MongoDB and handle exceptions
        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            ingamedataCollection = database.GetCollection<BsonDocument>(collectionName);
            a = true ;
            Debug.Log("Successfully connected to MongoDB.");
        }
        catch (TimeoutException ex)
        {
            Debug.Log($"Connection timeout: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        catch (MongoConfigurationException ex)
        {
            Debug.Log($"Configuration error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        catch (MongoConnectionException ex)
        {
            Debug.Log($"Connection error: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
        catch (Exception ex)
        {
            Debug.Log($"An unexpected error occurred: {ex.Message}");
            Notification.SetActive(true);
             tMP_Text.text = ex.Message ;
            Invoke("DisableGameObject", 5f);
            return false;
        }
            
        }
        
            keytobechecked = key ;
            var filter = Builders<BsonDocument>.Filter.Eq("key", key);
            var document = ingamedataCollection.Find(filter).FirstOrDefault();
            
            if (document != null)
            {
                if (document.GetValue("2player").AsBoolean== false)
                {
                    is2PlayerReady = true;
                    
                    document["2player"] = true; // Set the value of "2player" to true
                    var update = Builders<BsonDocument>.Update.Set("2player", true); // Define the update operation
                    ingamedataCollection.UpdateOne(filter, update); // Update the document in the collection
                    Debug.Log("2player attribute set to true in the document.");
                    
                    // (client as IDisposable).Dispose();
                    // client.Cluster.Dispose();
                    // Perform any actions you want when both players are ready
                      PlayerPrefs.SetInt("VS", 4);
                      PlayerPrefs.Save();
                      SceneManager.LoadScene(1);
                      return true;
                  
                    // return true;

                }
                else
                {
                    Debug.Log("Waiting for the other player to join...");
                }
            }
            else
            {
                Debug.LogError("Document not found for the given key.");
            }
            return false ;
    }

    private void DisableGameObject()
    {
        if (Notification != null)
        {
            // Deactivate the GameObject
            Notification.SetActive(false);
            Debug.Log("GameObject has been deactivated.");
        }
    }



    // Function to generate a random color



    public void Check2PlayerStatus()
    {
        Connect();

            // yield return new WaitForSeconds(5f); // Wait for 5 seconds
            var filter = Builders<BsonDocument>.Filter.Eq("key", keytobechecked);
            var document = ingamedataCollection.Find(filter).FirstOrDefault();
            
            if (document != null)
            {
                if (document.GetValue("2player").AsBoolean)
                {
                    is2PlayerReady = true;
                    Debug.Log("Both players are ready.");
                    // Perform any actions you want when both players are ready
                      PlayerPrefs.SetInt("VS", 5);
                      PlayerPrefs.Save();
                      SceneManager.LoadScene(1);
                      return ;
                  
                    // return true;

                }
                else
                {
                    Debug.Log("Waiting for the other player to join...");
                }
            }
            else
            {
                Debug.LogError("Document not found for the given key.");
            }
        // return false;
        Invoke("Check2PlayerStatus",5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
