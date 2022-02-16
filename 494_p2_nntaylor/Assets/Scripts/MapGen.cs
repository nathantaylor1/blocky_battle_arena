using System;
using UnityEngine;
using Random = System.Random;

public class MapGen : MonoBehaviour
{
    public static MapGen instance;
    public uint mapWidth = 100, mapHeight = 100;
    [Range(0, 100)] public uint percentIsWall = 50;
    public string seed = "Random";
    public uint numPickaxes = 12;

    private GameObject _tileFloor, _tileWall, _tileUnbreakable;
    public GameObject player;
    private Vector2 _playerPos;
    [NonSerialized] public int[,] levelMap;
    [NonSerialized] public Random _prng;
    private Vector2[] pickaxeLocations;
    private GameObject _pickaxe;
    
    private void Awake()
    {
        instance = this;
        _tileUnbreakable = Resources.Load<GameObject>("PreFabs/Tiles/Tile_WallUnbreakable");
        _tileFloor = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Floor");
        _tileWall = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Wall");
        pickaxeLocations = new Vector2[12];
        _pickaxe = Resources.Load<GameObject>("PreFabs/Collectables/PickaxePowerUp");
    }

    private void Start()
    {
        GenerateMap();
        InstantiateMap();
    }
    
    private void GenerateMap()
    {
        levelMap = new int[mapWidth, mapHeight];

        // Check if seed is Default Value, if so RANDOMIZE IT
        // https://answers.unity.com/questions/603000/generating-a-good-random-seed.html
        if (seed == "Random") seed = DateTime.Now.Ticks.ToString();

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
        PlacePickAxes();
    }

    private bool CellularAutomataCheck(uint x, uint y)
    {
        // returns true if tile at (x, y) should be wall
        // returns false if tile at (x, y) should be floor
        uint nearbyWallCount = 0;

        // Check North:
        if (levelMap[x, y + 1] == 1)
            nearbyWallCount++;

        // Check East:
        if (levelMap[x + 1, y] == 1)
            nearbyWallCount++;

        // Check South:
        if (levelMap[x, y - 1] == 1)
            nearbyWallCount++;

        // Check West:
        if (levelMap[x - 1, y] == 1)
            nearbyWallCount++;

        if (nearbyWallCount == 2)
        {
            // Check Self (for ties):
            if (levelMap[x, y] == 1)
                return true;
            else
                return false;
        }
        else if (nearbyWallCount > 2)
            return true;
        else
            return false;
    }

    private void CellularAutomata()
    {
        // Cellular Automata Check and Adjust:
        for (uint i = 1; i < mapWidth - 1; ++i)
        {
            for (uint j = 1; j < mapHeight - 1; ++j)
            {
                // 1 == wall, 0 == floor
                levelMap[i, j] = CellularAutomataCheck(i, j) ? 1 : 0;
            }
        }
    }

    private void PlacePlayer()
    {
        // Place Player
        int halfWidth = (int)mapWidth / 2, halfHeight = (int)mapHeight / 2;
        int count = 0;
        int range = 5;
        while (true)
        {
            int possibleX = halfWidth + _prng.Next(-range, range), possibleY = halfHeight + _prng.Next(-range, range);
            if (levelMap[possibleX, possibleY] == 0)
            {
                _playerPos = new Vector2(possibleX, possibleY);
                break;
            }

            if (count > range * 2)
            {
                range += 5;
                count = 0;
                continue;
            }
            ++count;
        }
    }

    private void PlacePickAxes()
    {
        for (uint i = 0; i < numPickaxes; ++i)
        {
            int x = _prng.Next(6, 94), y = _prng.Next(6, 94);
            int count = 0, range = 5;
            while (true)
            {
                int possibleX = x + _prng.Next(-range, range), possibleY = y + _prng.Next(-range, range);
                if (levelMap[possibleX, possibleY] == 0)
                {
                    pickaxeLocations[i] = new Vector2(possibleX, possibleY);
                    break;
                }

                if (count > range * 2)
                {
                    range += 5;
                    count = 0;
                    continue;
                }
                ++count;
            }
        }
    }

    private void InstantiateMap()
    {
        InstantiateTiles();
        InstantiatePlayer();
        InstantiatePickAxes();
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

    private void InstantiatePickAxes()
    {
        for (uint i = 0; i < numPickaxes; ++i)
        {
            GameObject go = Instantiate(_pickaxe);
            go.transform.position = pickaxeLocations[i];
        }
    }

}
