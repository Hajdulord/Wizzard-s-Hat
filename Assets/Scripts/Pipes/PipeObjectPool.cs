using System.Collections.Generic;
using UnityEngine;

namespace WizzardsHat.Pipes
{
    public class PipeObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        
        public static PipeObjectPool Instance { get; private set; }

        private Queue<GameObject> _pool = new();

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public GameObject Get()
        {
            if (_pool.Count == 0)
                AddPool(1);
            return _pool.Dequeue();
        }

        public void ReturnToQueue(GameObject objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _pool.Enqueue(objectToReturn);
        }

        public void AddPool(int count)
        {
            var newObject = Instantiate(_prefab);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
        }

    }
}