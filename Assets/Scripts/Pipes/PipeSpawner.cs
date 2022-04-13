using UnityEngine;
using WizzardsHat.UI;

namespace WizzardsHat.Pipes
{
    public class PipeSpawner : MonoBehaviour
    {
        [Range(0, 20)]
        [SerializeField] private int _count = 5;
        [Range(0, 20)]
        [SerializeField] private float _spawnTime = 10;
        
        private Menu _menu;
        private bool _gameStarted;
        private float _nextSpawnTime;
        private float _cameraBoundingBoxVerticalExtent;

        private void Awake()
        {
            _cameraBoundingBoxVerticalExtent = FindObjectOfType<Constraint>()
                .GetComponent<PolygonCollider2D>()
                .bounds.extents.y;
            _menu = FindObjectOfType<Menu>();
            _menu.StartButtonPressed += OnStart;
            PipeObjectPool.Instance.AddPool(_count);
        }

        private void Update()
        {
            if (_gameStarted && Time.time >= _nextSpawnTime)
            {
                SpawnPipe();
                _nextSpawnTime = Time.time + _spawnTime;
            }
        }

        private void SpawnPipe()
        {
            var pipe = PipeObjectPool.Instance.Get();
            var range = _cameraBoundingBoxVerticalExtent - 2f;
            pipe.transform.position = new Vector3(transform.position.x, Random.Range(-range, range), 0);
            pipe.SetActive(true);
        }

        private void OnStart() => _gameStarted = true;

        private void OnDestroy()
        {
            _menu.StartButtonPressed -= OnStart;
        }
    }
}
