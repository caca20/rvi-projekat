using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLoop : MonoBehaviour
{
    [SerializeField] public GameObject[] roadPrefabs;   
    [SerializeField] public GameObject[] boosterPrefabs;    
    private Vector3 spawnPoint = new Vector3(0f,0f,340f);

    [SerializeField] public GameObject palmPrefab;

    public int spawnTime;
    public int timeToSpeedUp;
    private int moveSpeed;

    public RoadMovement lastSpawned;
    private GameObject endGround;
    private GameObject startGround;
    private List<GameObject> middleGround;
    private List<RoadMovement> currentRoad;

    private List<GameObject> currentBoosters;
    private List<RoadMovement> currentObstacles;

    private int side = 8;

    public bool isPaused;

    public GameObject obstaclePrefab;
    private System.Random randomIndex = new System.Random();

    private void Awake() {
        isPaused = false;
        currentRoad = new List<RoadMovement>();
        currentBoosters = new List<GameObject>();
        currentObstacles = new List<RoadMovement>();

        moveSpeed = 4;

        timeToSpeedUp = 35;

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
    }

    void Start()
    {
        StartCoroutine(SpawnCoroutine(spawnTime));
        StartCoroutine(SpeedUpRoad(timeToSpeedUp));
      //  StartCoroutine(Boosters(spawnTime*3));
       // StartCoroutine(Obstacles(spawnTime*3));
    }
/*
    public IEnumerator Obstacles(int showTime){
        while (true)
        {
            while(isPaused){
                yield return null;
            }
            if(randomIndex.NextDouble()>=0.3){
                Vector3 pos = new Vector3(0f,0.25f,80f);
                pos.x = (float)randomIndex.NextDouble() * 4f - 2f;
                RoadMovement created = InstantiateObject(obstaclePrefab,pos);
                currentObstacles.Add(created);
                yield return new WaitForSecondsRealtime(showTime);
            }
        }
    }
    */
/*
    public IEnumerator Boosters(int boosterSpawnTime){
        while (true)
        {
            while(isPaused){
                yield return null;
            }

            if(randomIndex.NextDouble()>=0.3){
                yield return new WaitForSecondsRealtime(boosterSpawnTime);
                GameObject createPrefab = boosterPrefabs[randomIndex.Next(0,boosterPrefabs.Length)];
                float x = (float)randomIndex.NextDouble() * 5f- 2.5f;
                float z = 101f;
                //TODO: treba proveriti da li ispod ima staza, da ne bi lebdelo u vazduhu

                Vector3 positon = new Vector3(x,0.1f,z);
                GameObject created = Instantiate(createPrefab,positon,Quaternion.identity);
                created.transform.Rotate(0f,180f,0f,Space.Self);
                RoadMovement booster = created.GetComponent<RoadMovement>();
                booster.SpeedUp(moveSpeed);
                currentBoosters.Add(created);
            }
        }
    }
*/
    public IEnumerator SpeedUpRoad(int speedUpTime){
       while (true)
       {
            while(isPaused){
                yield return null;
            }

            //max speed
            if(moveSpeed == 15){
                break;
            }

            yield return new WaitForSecondsRealtime(speedUpTime);
            moveSpeed = moveSpeed + 1;
            if(spawnTime>1){
                spawnTime = spawnTime-1;
            }
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
            while(isPaused){
                yield return null;
            }
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

            if(randomIndex.NextDouble()<=0.2){
                //+-8,0,150
                Vector3 pos = new Vector3(side,0f,150f);
                side = -side;
                InstantiateObject(palmPrefab,pos);

            }

            instantiatedRoad = InstantiateObject(roadPrefab, spawnPoint);
            spawnPoint = instantiatedRoad.transform.position;
            lastSpawned = instantiatedRoad;
            SetBoostersAndObstacles(instantiatedRoad);
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

    private void SetBoostersAndObstacles(RoadMovement createdRoad ){
        Vector3 position = new Vector3(0f,0f,0f);
        position.x = createdRoad.transform.position.x; 
        position.z = createdRoad.transform.position.z+2f; 
        
        if(createdRoad.tag!="SmallBridge"){
            float rand = (float)randomIndex.NextDouble()*5f-2.5f;
            position.x = createdRoad.transform.position.x+rand; 
        }
        //boosters
        if(randomIndex.NextDouble()>=0.2){
            if(createdRoad.tag!="SmallBridge"){
                float rand = (float)randomIndex.NextDouble()*5f-2.5f;
                position.x = createdRoad.transform.position.x+rand; 
            }
            GameObject createPrefab = boosterPrefabs[randomIndex.Next(0,boosterPrefabs.Length)];
            GameObject created = Instantiate(createPrefab,position,Quaternion.identity);
            created.transform.Rotate(0f,180f,0f,Space.Self);
            RoadMovement booster = created.GetComponent<RoadMovement>();
            booster.SpeedUp(moveSpeed);
            currentBoosters.Add(created);
        }

        //obstacles
        if(randomIndex.NextDouble()>=0.2){
            position.y = 0.2f;
            if(createdRoad.tag!="SmallBridge"){
                float rand = (float)randomIndex.NextDouble()*5f-2.5f;
                position.x = createdRoad.transform.position.x+rand; 
            }
            position.z+=3f;
            RoadMovement created = InstantiateObject(obstaclePrefab,position);
            currentObstacles.Add(created);
        }
    }
}
