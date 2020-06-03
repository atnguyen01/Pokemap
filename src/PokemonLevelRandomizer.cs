﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;

namespace pokemongenerator
{
  class PokemonLevelRandomizer
  {
    public int width { get; private set; }
    public int height { get; private set; }
    public int seed { get; private set; }

    public PokemonLevelRandomizer(int width, int height, string seed = "1234")
    {
      this.width = width;
      this.height = height;
      this.seed = seed.Sum(c => Convert.ToInt32(c));
    }

    static void Main(string[] args)
    {
      try
      {
        switch (args.Length)
        {
          case 2: (new PokemonLevelRandomizer(int.Parse(args[0]), int.Parse(args[1]))).Generate(); break;
          case 3: (new PokemonLevelRandomizer(int.Parse(args[0]), int.Parse(args[1]), args[2])).Generate(); break;
          default: Console.WriteLine("Usage: <width> <height> <?seed>"); break;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        Environment.Exit(1);
      }
    }

    private void Generate()
    {
      // TODO
      MapGenerator g = new MapGenerator(width, height);
      g.AddStep(new TerrainGenerator(seed));
      g.Generate();
    }
  }
}
