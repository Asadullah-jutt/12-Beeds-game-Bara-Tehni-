using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsGottiPlaces : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] team1Gottiplacestemp ;
    public GameObject[] team2Gottiplacestemp ;
    public GameObject team1GottiplacesPrefab;
    public GameObject team2GottiplacesPrefab;

    // Instances of team1Gottiplaces and team2Gottiplaces
    private GameObject team1Gottiplaces;
    private GameObject team2Gottiplaces;
    public int team1idx = 0 , team2idx = 0 ; 

    void Start()
    {
        // Instantiate team1Gottiplaces and team2Gottiplaces at runtime
      

        // Call GetPlaces to fill the arrays with the child objects
       
    }

    public void GetPlaces()
    {
           team1Gottiplaces = Resources.Load<GameObject>("Team1 beed Places");
           team2Gottiplaces = Resources.Load<GameObject>("Team2 beed Places");

        
        // Do any additional setup or initialization as needed

        // Initialize the arrays now that we have the team objects
        team1Gottiplacestemp = new GameObject[team1Gottiplaces.transform.childCount];
        team2Gottiplacestemp = new GameObject[team2Gottiplaces.transform.childCount];
        for (int i = 0; i < team1Gottiplacestemp.Length; i++)
        {
            Debug.Log("sfsfsf");
            team1Gottiplacestemp[i] = team1Gottiplaces.transform.GetChild(i).gameObject;
            team2Gottiplacestemp[i] = team2Gottiplaces.transform.GetChild(i).gameObject;
            
        }
    }


    public void AddDeadBeedOfTeam1(GameObject gameObject)
    {
        gameObject.transform.SetParent(team1Gottiplacestemp[team1idx].transform);
        team1idx++;
    }
    public void AddDeadBeedOfTeam2(GameObject gameObject)
    {
        gameObject.transform.SetParent(team2Gottiplacestemp[team2idx].transform);
        team2idx++;
    }

  
}
