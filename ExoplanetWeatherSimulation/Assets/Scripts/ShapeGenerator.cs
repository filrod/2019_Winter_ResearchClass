using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    NoiseFilter[] noiseFilters; //NoiseFilter noiseFilter;

    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];  //this.noiseFilter = new NoiseFilter(settings.noiseSettings);

        // Loop over noise filter layers and create noise filters
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        //return pointOnUnitSphere * settings.planetRadius;
        float elevation = 0; // noiseFilter.Evaluate(pointOnUnitSphere);

        // Loop through noise filters and set elevation
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere);
            }
        }
        return pointOnUnitSphere * settings.planetRadius * (1+elevation);
    }
}
