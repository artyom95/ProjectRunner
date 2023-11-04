using UnityEngine;

namespace Settings
{
    public class CubeDownloader : MonoBehaviour
    {
        [SerializeField] private GameObject _cube;
        [SerializeField] private Grid _grid;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var cube = Instantiate(_cube);
                    cube.transform.position = _grid.CellToWorld(new Vector3Int(i, 0, j));
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo))
                {
                    Debug.Log(_grid.WorldToLocal(hitInfo.transform.position));
                }
            }
        }
    }
}