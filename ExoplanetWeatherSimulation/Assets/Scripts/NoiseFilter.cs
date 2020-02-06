using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
    NoiseSettings settings;
    Noise noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    // Point is where we evaluate and apply processing
    public float Evaluate(Vector3 point)
    {
        // noise.Evaluate(point) is in [-1, 1] so ad 1 for [0, 1]
        float noiseValue = (noise.Evaluate(point * settings.roughness + settings.center) + 1) * 0.5f;
        return noiseValue * settings.strength;
    }
}
