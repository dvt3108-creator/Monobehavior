using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class CubeRotator : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] private float rotationSpeed = 2f;
    [SerializeField] private RotationDirection direction = RotationDirection.Clockwise;

    private CubeCreator cubeCreator;
    private List<GameObject> cubes;
    private float currentAngle = 0f;
    private int dirMultiplier = 1;

    private void Start()
    {
        cubeCreator = GetComponent<CubeCreator>();
        cubes = cubeCreator.GetCubes();

        dirMultiplier = direction == RotationDirection.Clockwise ? -1 : 1;
    }

    private void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime * dirMultiplier;

        foreach (var cube in cubes)
        {
            var pos = cube.transform.localPosition;
            var angle = Mathf.Atan2(pos.z, pos.x) + rotationSpeed * Time.deltaTime * dirMultiplier;

            var radius = pos.magnitude;
            cube.transform.localPosition = new Vector3(
                Mathf.Cos(angle) * radius,
                pos.y,
                Mathf.Sin(angle) * radius
            );
        }
    }
}
