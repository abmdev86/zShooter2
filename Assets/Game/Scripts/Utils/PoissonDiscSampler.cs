

using System.Collections.Generic;
using UnityEngine;

public class PoissonDiscSampler
{
  /// <summary>
  /// max amount of attempts before making a sample inactive.
  /// </summary>
  const int k = 30;
  /// <summary>
  /// The rectangle in which the points will be placed.
  /// </summary>
  readonly Rect rect;

  /// <summary>
  /// radius squared
  /// </summary>
  readonly float radius2;
  /// <summary>
  /// The cell size of the grid of points.
  /// </summary>
  readonly float cellSize;
  /// <summary>
  /// The grid of points
  /// </summary>
  Vector2[,] grid;
  /// <summary>
  ///  The List of locations near which we're trying to add new points to.
  /// </summary>
  List<Vector2> activeSamples = new List<Vector2>();
  /// <summary>
  /// Create a sampler with the following parameters
  /// </summary>
  /// <param name="width"> each sample's x coordinate will be between [0, width]</param>
  /// <param name="height">each samples's y coordinate will be between [0, height]</param>
  /// <param name="radius"> each sample will be at least 'radius' units away from any other sample, and at most 2 * 'radius'</param>

  public PoissonDiscSampler(float width, float height, float radius)
  {
    rect = new Rect(0, 0, width, height);
    radius2 = radius * radius;
    cellSize = radius / Mathf.Sqrt(2);
    grid = new Vector2[Mathf.CeilToInt(width / cellSize), Mathf.CeilToInt(height / cellSize)];
  }

  public IEnumerable<Vector2> Samples()
  {
    Vector2 firstSample = new Vector2(Random.value * rect.width, Random.value * rect.height);
    yield return AddSample(firstSample);
    while (activeSamples.Count > 0)
    {
      // pick a random active sample
      int i = (int)Random.value * activeSamples.Count;
      Vector2 sample = activeSamples[i];

      bool found = false;
      for (int j = 0; j < k; ++j)
      {
        float angle = 2 * Mathf.PI * Random.value;
        float r = Mathf.Sqrt(Random.value * 3 * (2 * radius2));
        Vector2 candidate = sample + r * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Accepts canidates if inside the rect and farther that 2* radius to any exsisting sample
        if (rect.Contains(candidate) && IsFarEnough(candidate))
        {
          found = true;
          yield return AddSample(candidate);
          break;
        }
      }
      if (!found)
      {
        activeSamples[i] = activeSamples[activeSamples.Count - 1];
        activeSamples.RemoveAt(activeSamples.Count - 1);
      }
    }
  }
  bool IsFarEnough(Vector2 sample)
  {
    GridPos pos = new GridPos(sample, cellSize);

    int xmin = Mathf.Max(pos.x - 2, 0);
    int ymin = Mathf.Max(pos.y - 2, 0);
    int xmax = Mathf.Min(pos.x + 2, grid.GetLength(0) - 1);
    int ymax = Mathf.Min(pos.y + 2, grid.GetLength(1) - 1);

    for (int y = ymin; y <= ymax; y++)
    {
      for (int x = xmin; x <= xmax; x++)
      {
        Vector2 s = grid[x, y];
        if (s != Vector2.zero)
        {
          Vector2 d = s - sample;
          if (d.x * d.x + d.y * d.y < radius2) return false;
        }
      }
    }
    return true;
  }

  /// <summary>
  ///  Adds the sample to the active samples queue and the grid before
  ///  returning it
  /// </summary>
  /// <param name="sample">Vector2 value of the x,y pos in the grid</param>
  /// <returns>the sample after adding it to the queue and grid</returns>

  Vector2 AddSample(Vector2 sample)
  {
    activeSamples.Add(sample);
    GridPos pos = new GridPos(sample, cellSize);
    grid[pos.x, pos.y] = sample;
    return sample;
  }
  /// <summary>
  /// Helper struct to calculate the x and y indices of a sample in the grid.
  /// </summary>

  struct GridPos
  {
    public int x;
    public int y;
    public GridPos(Vector2 sample, float cellsize)
    {
      x = (int)(sample.x / cellsize);
      y = (int)(sample.y / cellsize);
    }
  }
}
