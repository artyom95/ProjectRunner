using UnityEngine;

namespace Settings
{
    [RequireComponent(typeof(Renderer))]
    public class TileSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _partForColoring ;
        [SerializeField] private Color[] _arrayColor = new Color[10] ;
        public Color Color => _renderer.material.color;
        private MeshRenderer _renderer; // Start is called before the first frame update

        private void Awake()
        {
            _renderer = _partForColoring.GetComponent<MeshRenderer>();
        }
    

        public void SetColor(int number)
        {
            _renderer.material.color = _arrayColor[number-1];
        }
    }
}