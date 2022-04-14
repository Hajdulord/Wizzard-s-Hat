using UnityEngine;
using WizzardsHat.Player;

namespace WizzardsHat.Pipes
{
    public class PipeController : MonoBehaviour
    {
        [Range(0f, 10f)]
        [SerializeField] private float _speed = 4f;

        private PolygonCollider2D _cameraBoundingBox;
        private PlayerController _player;
        private float _maxMovementPosition;

        private void Awake()
        {
            _cameraBoundingBox = FindObjectOfType<Constraint>().GetComponent<PolygonCollider2D>();
            _maxMovementPosition = -(_cameraBoundingBox.bounds.extents.x 
                                   + GetComponentInChildren<BoxCollider2D>().bounds.extents.x * 2 
                                   + 1);
            _player = FindObjectOfType<PlayerController>();
            _player.GameEndAction += OnGameEnd;
        }

        private void Update()
        {
            if (transform.position.x < _maxMovementPosition - 10f)
            {
                PipeObjectPool.Instance.ReturnToQueue(gameObject);
            }
            
            transform.Translate(new Vector3(-_speed * Time.deltaTime, 0, 0));
        }

        private void OnGameEnd()
        {
            PipeObjectPool.Instance.ReturnToQueue(gameObject);
        }

        private void OnDestroy()
        {
            _player.GameEndAction -= OnGameEnd;
        }
    }
}
