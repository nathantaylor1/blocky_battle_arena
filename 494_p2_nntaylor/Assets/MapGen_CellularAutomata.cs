using System;
using UnityEngine;
using Random = System.Random;

public class MapGen_CellularAutomata : MonoBehaviour
{
    public uint mapWidth = 100, mapHeight = 100;
    [Range(0, 100)] public uint percentIsWall = 50;
    public string seed = "Random";

    private GameObject _tileFloor, _tileWall;
    private int[,] _levelMap;
    private bool _hasGenerated = false;
    
    private void Awake()
    {
        _tileFloor = Resources.Load<GameObject>("Tiles/Tile_Floor");
        _tileWall = Resources.Load<GameObject>("Tiles/Tile_Wall");
    }

    private void Start()
    {
        MapGen();
    }

    private bool CellularAutomataCheck(uint x, uint y)
    {
        // returns true if tile at (x, y) should be wall
        // returns false if tile at (x, y) should be floor
        uint nearbyWallCount = 0;

        if (x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1)
            return true;
        
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

    private void MapGen()
    {
        if (_hasGenerated) return;
        _hasGenerated = true;
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
                // 1 == wall, 0 == floor
                if (i == 0 || j == 0 || i == mapWidth - 1 || j == mapHeight - 1)
                {
                    print(true);
                    // set all edges to 1
                    _levelMap[i, j] = 1;
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

        // Instantiate Tiles
        for (uint i = 0; i < mapWidth; ++i)
        {
            for (uint j = 0; j < mapHeight; ++j)
            {
                GameObject go = null;

                if (_levelMap[i, j] == 1)
                    go = Instantiate(_tileWall, transform);
                else
                    go = Instantiate(_tileFloor, transform);

                go.transform.position = new Vector3(i, j, 0.0f);
            }
        }
    }
}
