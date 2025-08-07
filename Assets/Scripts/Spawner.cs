using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawer : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _capacity;
    [SerializeField] private int _minCoordinate;
    [SerializeField] private int _maxCoordinate;
    [SerializeField] private float _time;
    
    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab), 
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => CubeDestroy(cube),
            collectionCheck: true, 
            defaultCapacity: _capacity, 
            maxSize: _maxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 tempPosition = GenerateCubePosition();
        cube.Intialize(tempPosition);
        cube.gameObject.SetActive(true);
        cube.CubeCollided += CubeDestroy;
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.CubeCollided -= CubeDestroy;
    }

    private void CubeDestroy(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    private Vector3 GenerateCubePosition()
    {
        Vector3 position;
        position.y = _maxCoordinate;
        position.x = Random.Range(_minCoordinate, _maxCoordinate);
        position.z = Random.Range(_minCoordinate, _maxCoordinate);

        return position;
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_time);
        bool isWork = true;

        while (isWork)
        {
            yield return wait;

            GetCube();
        }
    }
}