using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 20;
    public int rows = 20;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject beginZone;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject player;

    private Transform boardHolder;
    private List <Vector3> gridPositions = new List <Vector3> ();
    private Vector3 beginZonePosition = new Vector3();

    void Start() {
        SetupScene(1);
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for(int x = 1; x < columns - 1; ++x)
        {
            for(int y = 1; y < rows - 1; ++y)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for(int x = -1; x < columns + 1; x++)
        {
            for(int y = -1; y < rows + 1; y++)
            {
                GameObject toInstanciate = floorTiles[Random.Range(0, floorTiles.Length)];
                if(x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstanciate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate (toInstanciate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int ObjectCount = Random.Range(minimum, maximum + 1);

        for(int i = 0; i < ObjectCount; ++i)
        {
            Vector3 randomPosition = RandomPosition();
            if(randomPosition != exitZonePosition) {
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }
    }

    void InstantiateBeginAndExitZone(int level) {
        int distance = level * 5 ;
//        Debug.Log(distance);

        int xBegin = 1;
        int yBegin = 1;
        beginZonePosition = new Vector3(xBegin, yBegin, 0f);
        Instantiate(beginZone, beginZonePosition, Quaternion.identity);

        int xExit = Random.Range(1, distance);
        int yExit = distance + xExit;
        xExit = xBegin + xExit;
        yExit = yBegin + yExit;
        if(xExit > columns - 1) {
            //update yExit to repercute the truncation
            yExit = yExit + ((columns -1) - xExit);
            xExit = columns - 1;
        }

        if(yExit > rows - 1) {
            yExit = rows - 1;
        }
        exitZonePosition = new Vector3(xExit, yExit, 0f);
        Instantiate(exit, exitZonePosition, Quaternion.identity);
    }

    public void SetupScene(int level)
    {
        InitialiseList();
        BoardSetup();
        InstantiateBeginAndExitZone(level);
        int enemyCount = 1;//(int) Mathf.Log(level, 2.0f) * 4;
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        Instantiate(player, beginZonePosition, Quaternion.identity);
    }

    public Vector3 getBeginZonePosition() {
        return beginZonePosition;
    }
}