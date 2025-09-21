using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;   
    [SerializeField] private Transform orbitCenter;    

    [SerializeField, Range(1, 50)] private int cubeCount = 8;           
    [SerializeField, Range(1f, 30f)] private float radius = 5f;          
    [SerializeField] private DispersionType dispersionType = DispersionType.Even;
    [SerializeField, Range(0f, Mathf.PI)] private float inbetweenDistance = 0.3f;

    private readonly List<GameObject> cubes = new();

    public List<GameObject> GetCubes() => cubes;

    private void Awake()
    {
        CreateCubes();
    }

    private void CreateCubes()
    {
        foreach (var cube in cubes)
        {
            Destroy(cube);
        }
        cubes.Clear();

        for (int i = 0; i < cubeCount; i++)
        {
            var angleOffset = dispersionType switch
            {
                DispersionType.Even => Mathf.PI * 2 / cubeCount * i,
                DispersionType.Close => inbetweenDistance * i,
                _ => 0f
            };

            Vector3 position = new Vector3(
                Mathf.Cos(angleOffset) * radius,
                0f,
                Mathf.Sin(angleOffset) * radius
            );

            var cube = Instantiate(cubePrefab, position, Quaternion.identity, orbitCenter);
            cubes.Add(cube);
        }
    }
}

