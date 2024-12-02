using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public enum RandomEvents{
        Obstacle,
        EnemyShip,
        Cannonball,
        WindsChanged,
        // None
    }
    private RandomEvents nextEvent;
    GameObject eventChildren;
    public float eventCooldown = 5f;
    bool gameStarted = false;
    private AudioSource audioSource;

    //Set to false in real game. True for testing
    bool sailsUp = true;

    [NonSerialized] public bool windDirectionWest = false;
    [Category("Enemy Ship")] 
    [SerializeField] GameObject[] shipSpawnLocations;
    [SerializeField] GameObject EnemyShipPrefab;
    [Category("Obstacle")]
    [SerializeField] GameObject[] ObstacleSpawnLocations;
    [SerializeField] GameObject[] ObstaclePrefabs;
    public float obstacleSpeed = 1f;
    [SerializeField] private GameObject obstacleParent;

    private FireScript[] fireLocations;



    void Start()
    {
        eventChildren = transform.Find("EventChildren").gameObject;
        fireLocations = transform.Find("FireLocations").GetComponentsInChildren<FireScript>();
        audioSource = GetComponent<AudioSource>();
    }

    //Call this function to start the events.
    public void RaiseSails(){
        sailsUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted && sailsUp){
            gameStarted = true;
            StartCoroutine(HandleEvents());
        }    
    }

    IEnumerator HandleEvents(){
        while(gameStarted){
            GetNextEvent();
            yield return new WaitForSeconds(eventCooldown);
            beginNextEvent();
        }
    }



    private void beginNextEvent(){
        switch(nextEvent){
            case RandomEvents.Obstacle:
                //spawn obstacle
                spawnObstacle();
                //Player must steer ship to avoid obstacle
                break;
            case RandomEvents.EnemyShip:
                //spawn enemy ship
                spawnEnemyShip();
                //Player must shoot enemy ship
                break;
            case RandomEvents.Cannonball:
                //spawn cannonball
                spawnCannonball();
                //Ship gets damaged and player must fix
                break;
            case RandomEvents.WindsChanged:
                //change wind direction
                changeWindDirection();
                //Player must adjust sails
                break;
            // case RandomEvents.None:
            //     //Player gets a break
            //     Debug.Log("The seas are calm for now...");
            //     break;
        }
    }


    //Random Event Generator
    public void GetNextEvent(){
        nextEvent = (RandomEvents)UnityEngine.Random.Range(0, RandomEvents.GetNames(typeof(RandomEvents)).Length);
    }



    void spawnObstacle(){
        audioSource.Play();
        //Spawn obstacle
        GameObject obstacleSpawnLocation = ObstacleSpawnLocations[UnityEngine.Random.Range(0, ObstacleSpawnLocations.Length)];
        GameObject obstacle = Instantiate(ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Length)], obstacleSpawnLocation.transform.position, obstacleSpawnLocation.transform.rotation);
        obstacle.transform.parent = eventChildren.transform;
        obstacle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        obstacle.transform.parent = obstacleParent.transform;
        StartCoroutine(moveObstacle(obstacle));
        //Alert the player of the obstacle
        Debug.Log("Obstacle has been spotted. Steer the ship to avoid it");
    }
    IEnumerator moveObstacle(GameObject obstacle){
        while(obstacle != null){
            //Move the obstacle towards the ship
            //Move obstacle in the positive X direction
            obstacle.transform.position += new Vector3(obstacleSpeed, 0, 0) * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            if (obstacle.transform.localPosition.x > 30){
                Destroy(obstacle);
                yield break;
            }
        }
    }

    public GameObject[] enemyShips = new GameObject[4];

    void spawnEnemyShip(){
        int shipIndex = UnityEngine.Random.Range(0, enemyShips.Length);
        if (enemyShips[shipIndex] != null){
            Destroy(enemyShips[shipIndex]);
            //Deal damage to player or end condition here?
        }
        //Spawn enemy ship
        GameObject shipSpawnLocation = shipSpawnLocations[shipIndex];
        GameObject enemyShip = Instantiate(EnemyShipPrefab, shipSpawnLocation.transform.position, shipSpawnLocation.transform.rotation);
        enemyShip.transform.parent = eventChildren.transform;
        enemyShips[shipIndex] = enemyShip;
        //Alert the player of the enemy ship
        Debug.Log("Enemy ship has been spotted. Defeat it quickly");
    }

    //Call this function to destroy a ship
    public void destroyShip(int shipIndex){
        Destroy(enemyShips[shipIndex]);
        enemyShips[shipIndex] = null;
    }

    void spawnCannonball(){
        //Spawn cannonball that flys towards the ship

        //Pick a random fire script and play it
        FireScript fire = fireLocations[UnityEngine.Random.Range(0, fireLocations.Length)];
        while (fire.fireEnabled){
            fire = fireLocations[UnityEngine.Random.Range(0, fireLocations.Length)];
        }
        fire.enableFire();
        //Alert the player of the damage
        Debug.Log("Cannonball has hit the ship");
        //Player must fix the damage
    }

    void changeWindDirection(){
        //Change wind direction
        windDirectionWest = !windDirectionWest;

        //Alert the player of the change in wind direction
        Debug.Log("Wind direction has changed to " + (windDirectionWest ? "West" : "East"));
    }


}
