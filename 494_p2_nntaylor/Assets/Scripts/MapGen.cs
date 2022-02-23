using System;
using UnityEngine;
using Random = System.Random;

public class MapGen : MonoBehaviour
{
    public static MapGen instance;
    public uint mapWidth = 100, mapHeight = 100;
    [Range(0, 100)] public uint percentIsWall = 47;
    public string seed = "random";
    public uint numGoblins = 20;

    private GameObject _tileFloor, _tileWall, _tileUnbreakable;
    public GameObject player;
    private Vector2 _playerPos;
    [NonSerialized] public int[,] levelMap;
    [NonSerialized] public Random _prng;
    
    private Vector2[] _goblinLocations;
    private GameObject _goblin;
    
    private void Awake()
    {
        instance = this;
        _tileUnbreakable = Resources.Load<GameObject>("PreFabs/Tiles/Tile_WallUnbreakable");
        _tileFloor = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Floor");
        _tileWall = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Wall");
        _goblinLocations = new Vector2[numGoblins];
        
        /*if (STATIC_LevelCount.levelCount == 0)*/
            // Assets/Resources/PreFabs/Level1_Enemy.prefab
            _goblin = Resources.Load<GameObject>("PreFabs/Level1_Enemy");
    }

    public void StartMapGen()
    {
        GenerateMap();
        InstantiateMap();
    }
    
    private void GenerateMap()
    {
        levelMap = new int[mapWidth, mapHeight];

        seed = seed.ToLower();
        // Check if seed is Default Value, if so RANDOMIZE IT
        // https://answers.unity.com/questions/603000/generating-a-good-random-seed.html
        if (seed == "random"  || seed == "") seed = DateTime.Now.Ticks.ToString();

        // Seed PseudoRandom Number Generator (pnrg):
        // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=net-6.0
        // https://docs.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-6.0#system-string-gethashcode
        _prng = new Random(seed.GetHashCode());

        // Get Initial Map Fill:
        for (uint i = 0; i < mapWidth; ++i)
        {
            for (uint j = 0; j < mapHeight; ++j)
            {
                // 2 == unbreakable wall, 1 == wall, 0 == floor
                if (i == 0 || j == 0 || i == mapWidth - 1 || j == mapHeight - 1)
                {
                    // set all edges to 1
                    levelMap[i, j] = 2;
                }
                else
                {
                    levelMap[i, j] = (_prng.Next(0, 100) < percentIsWall) ? 1 : 0;
                }
            }
        }

        CellularAutomata();
        PlacePlayer();
        PlaceGoblins();
    }

    private void CellularAutomata()
    {
        /* 4-5 rule:
                if it has 3 or fewer adjacent wall squares (counting all 8 cardinal compass points),
                    the square 'starves' and becomes a floor
                else if it has greater than 5 adjacent wall squares, the square becomes a wall
                else leave it as is
            */
        for (int x = 1; x < mapWidth - 1; ++x)
        {
            for (int y = 1; y < mapHeight - 1; ++y)
            {
                int numWallsNearby = 0;

                for (int i = x - 1; i <= x + 1; ++i)
                {
                    for (int j = y + 1; j >= y - 1; --j)
                    {
                        if (levelMap[i, j] == 1 || levelMap[i, j] == 2)
                        {
                            ++numWallsNearby;
                        }
                        /* else do nothing */
                    }
                }
                if (numWallsNearby >= 5) 
                    levelMap[x, y] = 1;
                else if (numWallsNearby <= 3)
                    levelMap[x, y] = 0;
                /* else do nothing */
            }
        }
    }

    private void PlacePlayer()
    {
        int halfW = (int)mapWidth / 2, halfH = (int)mapHeight / 2;
        levelMap[halfW-1, halfH-1] = 0;
        levelMap[halfW-1, halfH]   = 0;
        levelMap[halfW-1, halfH+1] = 0;
        levelMap[halfW,   halfH-1] = 0;
        levelMap[halfW,   halfH]   = 0;
        levelMap[halfW,   halfH+1] = 0;
        levelMap[halfW+1, halfH-1] = 0;
        levelMap[halfW+1, halfH]   = 0;
        levelMap[halfW+1, halfH+1] = 0;
        _playerPos = new Vector2(halfW, halfH);
    }
    
    private void PlaceGoblins()
    {
        GameController.instance.numEnemies = (int)numGoblins;
        for (uint i = 0; i < numGoblins; ++i)
        {
            int x, y;
            while (true)
            {
                x = _prng.Next(6, (int)mapWidth - 6);
                y = _prng.Next(6, (int)mapHeight - 6);
                if (levelMap[x, y] == 0) break;
            }

            _goblinLocations[i] = new Vector2(x, y);
        }
    }

    private void InstantiateMap()
    {
        InstantiateTiles();
        InstantiatePlayer();
        InstantiateGoblins();
    }

    private void InstantiateTiles()
    {
        // Instantiate Tiles
        for (uint i = 0; i < mapWidth; ++i)
        {
            for (uint j = 0; j < mapHeight; ++j)
            {
                GameObject go = null;

                if (levelMap[i, j] == 3)       // no tile
                    go = Instantiate(new GameObject(), transform);
                else if (levelMap[i, j] == 2)       // unbreakable tile
                    go = Instantiate(_tileUnbreakable, transform);
                else if (levelMap[i, j] == 1)  // wall tile
                    go = Instantiate(_tileWall, transform);
                else                            // floor tile
                    go = Instantiate(_tileFloor, transform);

                go.transform.position = new Vector3(i, j, 0.0f);
            }
        }
    }

    private void InstantiatePlayer()
    {
        player.transform.position = _playerPos;
    }

    private void InstantiateGoblins()
    {
        for (uint i = 0; i < numGoblins; ++i)
        {
            GameObject go = Instantiate(_goblin);
            go.transform.position = _goblinLocations[i];
        }
    }
}
