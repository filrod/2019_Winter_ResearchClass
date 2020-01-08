using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Face
{

    Mesh mesh;
    /** Holds the level of detail */
    int resolution;
    /** Provides information about which way the Terrain_Face is facing */
    Vector3 localUp;

    /** Axis vector tagent to the mesh for coordinates */
    Vector3 axisA;
    Vector3 axisB;

    /** Conctructor for the Terrain_Face object */
    public Terrain_Face(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // Set axis vectors
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

        public void constructMesh()
        {
            // hold vertices
            Vector3[] vertices = new Vector3[resolution * resolution];
            int amountOfVerticesPerSide = resolution - 1;
            int trianglesInASquare = 2;
            int verticesInATriangle = 3;
            int[] triangles = new int[
                amountOfVerticesPerSide * amountOfVerticesPerSide 
                * trianglesInASquare * verticesInATriangle
                ];

            // Nested for loops to see what fraction of the mesh we have covered. End is (1, 1)
            int i = 0;
            int triangleIndex = 0;
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    Vector2 meshCompletion = new Vector2(x, y) / (resolution - 1);
                    // Where the vertex is along the face
                    Vector3 pointOnUnitCube = localUp + (meshCompletion.x - 0.5f) * 2 * axisA + (meshCompletion.y - 0.5f) * 2 * axisB;
                    // Calculate index to add to vertices array
                    vertices[i] = pointOnUnitCube;
                    i++;

                    if (x != resolution - 1 && y != resolution - 1)
                    {
                        // Lower Left Hand Triangle of a quad
                        triangles[triangleIndex] = i;
                        triangles[triangleIndex + 1] = i + resolution+1;
                        triangles[triangleIndex + 2] = i + resolution;

                        // Upper Right hand Triangle of a quad
                        triangles[triangleIndex + 3] = i;
                        triangles[triangleIndex + 4] = i + 1;
                        triangles[triangleIndex + 5] = i + resolution+1;

                        // We moved along by 6 entries in triangles so increment by 6
                        triangleIndex += 6;
                    }
                }
            }

            // Assign to mesh
            mesh.Clear();  // in case we lower resolution to avoid indexOutOfBounds error
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}
