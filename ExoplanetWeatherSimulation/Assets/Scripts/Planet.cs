using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Store custom editor foldout toggle bool here to allow its valuie not to be lost in PlanetEditor.cs
    [HideInInspector]
    public bool shapeSettingsCustomMenuFoldout;
    [HideInInspector]
    public bool colourSettingsCustomMenuFoldout;

    // Auto update option from the editor
    public bool autoUpdate = true;

    // Set a terrain resolution
    [Range(2, 256)]
    public int resolution = 10;

    // Planet Colour and shape settings
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    // Make a shape generator object for the planet's radius size
    ShapeGenerator shapeGenerator;

    // Mesh filters used when displaying the terrain faces [make serialized but hide in the inspector]
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Terrain_Face[] terrainFaces;


    // Initialize the meshFilters, one for each side to total 6
    void Initialize()
    {
        // Initialize shape generator
        shapeGenerator = new ShapeGenerator(shapeSettings);

        // Ony initialize if meshFilters has not been populated
        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];
        terrainFaces = new Terrain_Face[6];

        // set-up local directions
        Vector3[] directions = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        // Cycle through the mesh faces
        for (int i = 0; i < 6; i++)
        {
            // Only create a mesh object if the entry for meshFilters is null
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                // Add mesh renderer
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(
                    Shader.Find("Standard")
                    ); // Make a default material and make it use the default shader
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new Terrain_Face(
                shapeGenerator,
                meshFilters[i].sharedMesh, 
                resolution, 
                directions[i]
                );
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    // Gernerate then mesh
    void GenerateMesh()
    {
        foreach (Terrain_Face face in terrainFaces)
        {
            face.constructMesh();
        }
    }

    // Set the colour
    void GenerateColours()
    {
        foreach (MeshFilter mf in meshFilters)
        {
            mf.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColour;
        }
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }
}
