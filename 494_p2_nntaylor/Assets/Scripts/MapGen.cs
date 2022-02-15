using System;
using UnityEngine;
using Random = System.Random;

public class MapGen : MonoBehaviour
{
    public uint mapWidth = 100, mapHeight = 100;
    [Range(0, 100)] public uint percentIsWall = 50;
    public string seed = "Random";

    private GameObject _tileFloor, _tileWall, _tileUnbreakable;
    private GameObject _player;
    private Vector2 _playerPos;
    private int[,] _levelMap;
    
    private void Awake()
    {
        _tileUnbreakable = Resources.Load<GameObject>("PreFabs/Tiles/Tile_WallUnbreakable");
        _tileFloor = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Floor");
        _tileWall = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Wall");
        _player = Resources.Load<GameObject>("PreFabs/Player");
    }

    private void Start()
    {
        GenerateMap();
        InstantiateTiles();
        InstantiatePlayer();
    }
    
    private void GenerateMap()
    {
        _levelMap = new int[mapWidth, mapHeight];

        // Check if seed is Default Value, if so RANDOMIZE IT
        // https://answers.unity.com/questions/603000/generating-a-good-random-seed.html
        if (seed == "Random") seed = DateTime.Now.Ticks.ToString();

        // Seed PseudoRandom Number Generator (pnrg):
        // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=net-6.0
        // https://docs.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-6.0#system-string-gethashcode
        Random prng = new Random(seed.GetHashCode());

        // Get Initial Map Fill:
        for (uint i = 0; i < mapWidth; ++i)
        {
            for (uint j = 0; j < mapHeight; ++j)
            {
                // 2 == unbreakable wall, 1 == wall, 0 == floor
                if (i == 0 || j == 0 || i == mapWidth - 1 || j == mapHeight - 1)
                {
                    // set all edges to 1
                    _levelMap[i, j] = 2;
                }
                else
                {
                    _levelMap[i, j] = (prng.Next(0, 100) < percentIsWall) ? 1 : 0;
                }
            }
        }
        // Cellular Automata Check and Adjust:
        for (uint i = 1; i < mapWidth - 1; ++i)
        {
            for (uint j = 1; j < mapHeight - 1; ++j)
            {
                // 1 == wall, 0 == floor
                _levelMap[i, j] = CellularAutomataCheck(i, j) ? 1 : 0;
            }
        }
        
        // Place Player
        int halfWidth = (int)mapWidth / 2, halfHeight = (int)mapHeight / 2;
        int count = 0;
        int range = 5;
        while (true)
        {
            int possibleX = halfWidth + prng.Next(-range, range), possibleY = halfHeight + prng.Next(-range, range);
            if (_levelMap[possibleX, possibleY] == 0)
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

    private bool CellularAutomataCheck(uint x, uint y)
    {
        // returns true if tile at (x, y) should be wall
        // returns false if tile at (x, y) should be floor
        uint nearbyWallCount = 0;

        // Check North:
        if (_levelMap[x, y + 1] == 1)
            nearbyWallCount++;

        // Check East:
        if (_levelMap[x + 1, y] == 1)
            nearbyWallCount++;

        // Check South:
        if (_levelMap[x, y - 1] == 1)
            nearbyWallCount++;

        // Check West:
        if (_levelMap[x - 1, y] == 1)
            nearbyWallCount++;

        if (nearbyWallCount == 2)
        {
            // Check Self (for ties):
            if (_levelMap[x, y] == 1)
                return true;
            else
                return false;
        }
        else if (nearbyWallCount > 2)
            return true;
        else
            return false;
    }

    private void InstantiateTiles()
    {
        // Instantiate Tiles
        for (uint i = 0; i < mapWidth; ++i)
        {
            for (uint j = 0; j < mapHeight; ++j)
            {
                GameObject go = null;

                if (_levelMap[i, j] == 3)       // no tile
                    go = Instantiate(new GameObject(), transform);
                else if (_levelMap[i, j] == 2)       // unbreakable tile
                    go = Instantiate(_tileUnbreakable, transform);
                else if (_levelMap[i, j] == 1)  // wall tile
                    go = Instantiate(_tileWall, transform);
                else                            // floor tile
                    go = Instantiate(_tileFloor, transform);

                go.transform.position = new Vector3(i, j, 0.0f);
            }
        }
    }

    private void InstantiatePlayer()
    {
        GameObject player = Instantiate(_player);
        player.transform.position = _playerPos;
    }
}
