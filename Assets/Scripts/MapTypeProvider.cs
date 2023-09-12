using UnityEngine;

public class MapTypeProvider : MonoBehaviour
{
    private int[,] _firstArrayMap;
    private int[,] _secondArrayMap;
    private int[,] _thirdArrayMap;
    private int[,] _currentMap;
    private int _numberScene;

    private void Awake()
    {
        _numberScene = 1;
        CreateMaps();
        ChoiceMap();
    }

    private void CreateMaps()
    {
        _firstArrayMap = new int[10, 8]
        {
            { -1, 0, 0, 0, 0, 5, -1, 1 },
            { 9, -1, 2, 6, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 4, 5, 6, 2, 8, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0,0 , -1, 1, 0, 0, 0 },
            { 0, 0, 0, 7, 8, 3, 0, 0 },
            { -1, 3, -1, 0, 0, 0, 0, 0 }
        };
    }

    private void ChoiceMap()
    {
        switch (_numberScene)
        {
            case 1:
                _currentMap = _firstArrayMap;
                break;
            case 2:
                _currentMap = _secondArrayMap;
                break;
            case 3:
                _currentMap = _thirdArrayMap;
                break;
        }
    }

    public int[,] GetTypeOfMap()
    {
        return _currentMap;
    }
}