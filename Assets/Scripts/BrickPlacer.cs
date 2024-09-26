using UnityEngine;

public class BrickPlacer : MonoBehaviour
{
    private Vector3 BlockPosition = new Vector3(-100, -100, -100);
    private float blockSize = .2f;

    // Start is called before the first frame update
    void Start() { }

    public void SpawnCube(Vector3 position)
    {
        Vector3 loweredPosition = new Vector3(position.x, blockSize * -0.5f, position.z);
        // Create a cube at the given position with default rotation
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = loweredPosition;

        Destroy(cube.GetComponent<Collider>());

        // Optional: Set the size of the cube (scaling)
        cube.transform.localScale = new Vector3(
            blockSize - 0.01f,
            blockSize - 0.01f,
            blockSize - 0.01f
        );

        // Optional: Set the color of the cube's material (requires a Renderer component)
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = Color.red; // Change color to red (or any other)
    }

    public float RoundToNearestMultipleOfX(float number, float x)
    {
        return Mathf.RoundToInt(number / x) * x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 nearestGriddedPosition = new Vector3(
            RoundToNearestMultipleOfX(position.x, blockSize),
            RoundToNearestMultipleOfX(position.y, blockSize),
            RoundToNearestMultipleOfX(position.z, blockSize)
        );
        if (nearestGriddedPosition != BlockPosition)
        {
            for (int i = -4; i < 5; i++)
            {
                for (int j = -4; j < 5; j++)
                {
                    BlockPosition = nearestGriddedPosition;
                    Vector3 eachBrickPosition = new Vector3(
                        BlockPosition.x + i * blockSize,
                        BlockPosition.y,
                        BlockPosition.z + j * blockSize
                    );
                    SpawnCube(eachBrickPosition);
                }
            }
        }
    }
}
