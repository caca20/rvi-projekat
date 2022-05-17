using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLoop : MonoBehaviour
{
    [SerializeField] public GameObject[] roadPrefabs;   
    [SerializeField] public GameObject[] boosterPrefabs;    
    private Vector3 spawnPoint = new Vector3(0f,0f,340f);

    public int spawnTime;
    public int timeToSpeedUp;
    private int moveSpeed = 3;

    public RoadMovement lastSpawned;
    private GameObject endGround;
    private GameObject startGround;
    private List<GameObject> middleGround;
    private List<RoadMovement> currentRoad;

    private List<GameObject> currentBoosters;
    private List<RoadMovement> currentObstacles;

    public GameObject obstaclePrefab;
    private System.Random randomIndex = new System.Random();
    
    private void Awake() {
        currentRoad = new List<RoadMovement>();
        currentBoosters = new List<GameObject>();
        currentObstacles = new List<RoadMovement>();


        timeToSpeedUp = 60;

        middleGround = new List<GameObject>();
        foreach (GameObject item in roadPrefabs)
        {
            if(item.tag == "GroundStart"){
                startGround = item;
            }
            if(item.tag == "GroundEnd"){
                endGround = item;
            }
            if(item.tag == "GroundMiddle" || item.tag == "LargeBridge" || item.tag == "SmallBridge"){
                middleGround?.Add(item);
            }   
        }
    }

    private void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player==null){
            StopAllCoroutines(); //TODO: ne radi iz nekog razloga
            //popup
        }
    }

    void Start()
    {
        StartCoroutine(SpawnCoroutine(spawnTime));
        StartCoroutine(SpeedUpRoad(timeToSpeedUp));
        StartCoroutine(Boosters(7));
        StartCoroutine(Obstacles(spawnTime*2));
    }

    public IEnumerator Obstacles(int showTime){
        while (true)
        {
            if(randomIndex.NextDouble()>=0.2){
                Vector3 pos = new Vector3(0f,0.25f,90f);
                pos.x = (float)randomIndex.NextDouble() * 4f - 2f;
                RoadMovement created = InstantiateObject(obstaclePrefab,pos);
                currentObstacles.Add(created);
                yield return new WaitForSecondsRealtime(showTime);
            }
        }
    }
    

    public IEnumerator Boosters(int boosterSpawnTime){
        //TODO:mozda staviti da se ne stavi svaki ovaj booster nego neki da se preskoce?
        while (true)
        {
            yield return new WaitForSecondsRealtime(boosterSpawnTime);
            GameObject createPrefab = boosterPrefabs[randomIndex.Next(0,boosterPrefabs.Length)];
            float x = (float)randomIndex.NextDouble() * 5f- 2.5f;
            float z = 101f;

        //TODO: treba proveriti da li ispod ima staza, da ne bi lebdelo u vazduhu

            Vector3 positon = new Vector3(x,0.1f,z);
            GameObject created = Instantiate(createPrefab,positon,Quaternion.identity);
            created.transform.Rotate(0f,180f,0f,Space.Self);
            currentBoosters.Add(created);
        }
    }

    public IEnumerator SpeedUpRoad(int speedUpTime){
       while (true)
       {
            yield return new WaitForSecondsRealtime(speedUpTime);
            moveSpeed = moveSpeed + 1;
            Debug.Log("Ubrzava");
            foreach (RoadMovement item in currentRoad)
            {
                item.SpeedUp(moveSpeed);
            }

            foreach (GameObject obj in currentBoosters)
            {
                if(obj){
                    RoadMovement booster = obj.GetComponent<RoadMovement>();
                    booster.SpeedUp(moveSpeed);
                }
            }

            foreach (RoadMovement obj in currentObstacles)
            {
                if(obj){
                    obj.SpeedUp(moveSpeed);
                }
            }
       }
    }

    //TODO:srediti kod ispod
    public IEnumerator SpawnCoroutine(int timeForSpawn)
    {
        
        while (true)
        {
            RoadMovement instantiatedRoad;
            GameObject roadPrefab;
            if (lastSpawned.tag == "SmallBridge"){
                roadPrefab = middleGround[randomIndex.Next(0,middleGround.Count)];
                if(roadPrefab.tag =="SmallBridge" ){
                    spawnPoint = lastSpawned.transform.position;
                    spawnPoint.z += 6.92f; 
                    roadPrefab.transform.position = spawnPoint;
                }else if(roadPrefab.tag == "GroundMiddle"){
                    //instanciraj prvo pocetak zemlje
                    spawnPoint = lastSpawned.transform.position;
                    spawnPoint.z += 7.41f; 
                    spawnPoint.x = 0f;
                    roadPrefab.transform.position = spawnPoint;
                    instantiatedRoad = InstantiateObject(endGround,spawnPoint);
                    lastSpawned = instantiatedRoad;
                    currentRoad.Add(instantiatedRoad);

                    spawnPoint = lastSpawned.transform.position;
                    spawnPoint.z += 3.80f; 
                    roadPrefab.transform.position = spawnPoint;    
            
                }else{
                    //Veliki most da se noramlno nadoveze
                    spawnPoint = lastSpawned.transform.position;
                    spawnPoint.z += 7.62f; 
                    spawnPoint.x = 0f;
                    roadPrefab.transform.position = spawnPoint;
                }
            }else if(lastSpawned.tag == "LargeBridge"){
                    roadPrefab = middleGround[randomIndex.Next(0,middleGround.Count)];
                    if(roadPrefab.tag =="SmallBridge" ){
                        //random pozicija
                        float random_pos = ((float)randomIndex.NextDouble() *4.8f - 2.4f );
                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 7.62f; 
                        spawnPoint.x += random_pos;
                        roadPrefab.transform.position = spawnPoint;
                
                    }else if(roadPrefab.tag == "GroundMiddle"){
                        //instanciraj prvo pocetak zemlje
                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 6.75f;
                        roadPrefab.transform.position = spawnPoint;
                        instantiatedRoad = InstantiateObject(endGround,spawnPoint);
                        lastSpawned = instantiatedRoad;
                        currentRoad.Add(instantiatedRoad);


                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 3.80f;
                        roadPrefab.transform.position = spawnPoint;    
            
                    }else{
                        //Veliki most da se noramlno nadoveze
                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 6.9f;
                        roadPrefab.transform.position = spawnPoint;    
                    }    
                }else{ //groundMiddle
                    roadPrefab = middleGround[randomIndex.Next(0,middleGround.Count)];
                    if(roadPrefab.tag =="GroundMiddle" ){
                        //nadoveze se normalno
                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 7.6f; 
                        roadPrefab.transform.position = spawnPoint;        
                    }else{
                        //doda se kraj zemlje pa neki od mostova
                        spawnPoint = lastSpawned.transform.position;
                        spawnPoint.z += 7.59f;
                        roadPrefab.transform.position = spawnPoint;
                        instantiatedRoad = InstantiateObject(startGround, spawnPoint);
                        lastSpawned = instantiatedRoad;
                        currentRoad.Add(instantiatedRoad);


                        if(roadPrefab.tag =="SmallBridge" ){
                            //random pozicija
                            float random_pos = ((float)randomIndex.NextDouble() *4.8f - 2.4f );
                            spawnPoint = lastSpawned.transform.position;
                            spawnPoint.z += 3.5f; 
                            spawnPoint.x += random_pos;
                            roadPrefab.transform.position = spawnPoint;    
                        }else{
                            //veliki most 
                            spawnPoint = lastSpawned.transform.position;
                            spawnPoint.z += 3.5f; 
                            roadPrefab.transform.position = spawnPoint;        
                    
                        }
                    }
                }
            instantiatedRoad = InstantiateObject(roadPrefab, spawnPoint);
            spawnPoint = instantiatedRoad.transform.position;
            lastSpawned = instantiatedRoad;
            currentRoad.Add(instantiatedRoad);
            yield return new WaitForSecondsRealtime(timeForSpawn);
        }
    }
    private RoadMovement InstantiateObject(GameObject prefab, Vector3 position){
        GameObject created = Instantiate(prefab, position, Quaternion.identity);
        RoadMovement road = created.GetComponent<RoadMovement>();
        road.SpeedUp(moveSpeed);
        road.transform.Rotate(0f,180f,0f,Space.Self);
        road.transform.localScale = new Vector3(2f,1f,1f);
        return road;    
    }  
}
