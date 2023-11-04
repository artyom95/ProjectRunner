using UnityEngine;

namespace Settings
{
    public class MapTypeProvider : MonoBehaviour
    {
        private int[,] _firstArrayMap;
        private int[,] _secondArrayMap;
        private int[,] _thirdArrayMap;
        private int[,] _currentMap;
        private int _numberScene;

        private void Awake()
        {
            //     _numberScene = 1;
            CreateMaps();
            //   ChoiceMap();
        }


        private void CreateMaps()
        {
            _firstArrayMap = new [,]
            {
                { -1, 0, 0, 0, 0, 5, -1, 1 },
                { 9, -1, 2, 6, 7, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 4, 5, 6, 2, 8, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 4, 0, 0, 0, 0 },
                { 0, 0, 0, -1, 1, 0, 0, 0 },
                { 0, 0, 0, 7, 8, 3, 0, 0 },
                { -1, 3, -1, 0, 0, 0, 0, 0 }
            };
            _secondArrayMap = new [,]
            {
                { -1, -1, 5, 0, 0, 5, -1, 1, 0, 0 },
                { 9, 1, -1, 6, 7, 0, 0, -1, -1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, -1, -1 },
                { 0, 4, 2, 0, 2, 8, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 6, 4, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, -1, 1, 0, 0, 0, 1, 3 },
                { 0, 0, 0, 7, 8, 3, 0, 0, -1, -1 },
                { -1, 5, -1, 0, 0, 0, 0, 3, -1, 0 }
            };
            _thirdArrayMap = new [,]
            {
                { -1, 0, 7, -1, 0, 5, -1, 1 },
                { 9, 3, 2, 8, 7, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 4, 5,0, 2, 8, 6, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 4, -1, -1, 3,-1 },
                { 0, 6, -1, 5, 1, 0, 0, 0 }
            };
        }

        private void ChoiceMap(int currentLevel)
        {
            switch (currentLevel)
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

        public int[,] GetTypeMap(int currentLevel)
        {
            ChoiceMap(currentLevel);
            return _currentMap;
        }
    }
}