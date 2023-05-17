using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    public class HexCellularAutomata : HexMapGen
    {
        public HexCellularGenData GenerationData;

        public override void Generate()
        {
            Map.Clear();
            FillWalls();
            SmoothWalls();
            Load(Map);
        }

        public virtual void FillWalls()
        {
            string seed = "";
            seed = GenerationData.Seed;
            if (GenerationData.UseRandomSeed)
                seed = Randomizer.RandomString(10);
            System.Random rndNumber = new System.Random(seed.GetHashCode());

            for (int x = -GenerationData.Width; x < GenerationData.Width; x++)
            {
                for (int y = GenerationData.Height; y > -GenerationData.Height; y--)
                {
                    if (IsInside(x, y, GenerationData.Width, GenerationData.Height))
                    {
                        int rnd = rndNumber.Next(0, 100);
                        Hex hex = CreateNewHex(x, y, rnd < GenerationData.FillPercent ? GenerationData.GroundType : GenerationData.EmptyType);
                    }
                }
            }
        }

        public virtual void SmoothWalls()
        {
            for (int i = 0; i < GenerationData.ResmoothWallTimes; i++)
            {
                ConnectMapWalls();
            }
        }

        public virtual void ConnectMapWalls()
        {
            for (int x = -GenerationData.Width; x < GenerationData.Width; x++)
            {
                for (int y = GenerationData.Height; y > -GenerationData.Height; y--)
                {
                    if (IsInside(x, y, GenerationData.Width, GenerationData.Height))
                    {
                        int neighborWallsCount = GetNeighborWallsCount(new Hex(x, y));
                        if (neighborWallsCount > GenerationData.MinNeighborWalls)
                        {
                            Find(x, y).Type = GenerationData.GroundType;
                        }
                        else if (neighborWallsCount < GenerationData.MinNeighborWalls)
                        {
                            Find(x, y).Type = GenerationData.EmptyType;
                        }
                    }
                }
            }
        }

        public virtual int GetNeighborWallsCount(Hex hex)
        {
            int neighborCount = 0;
            hex.Load(GenerationData.GroundType);
            foreach (Hex neighbor in hex.NeighborList())
            {
                if (Find(neighbor) != null && Find(neighbor).Type == GenerationData.GroundType)
                {
                    neighborCount += 1;
                }
            }
            return neighborCount;
        }

        // public virtual Vector2 GetRandomEmptyPosition()
        // {
        //     Vector2 position = new Vector2();
        //     do
        //     {
        //         position.x = Randomizer.RandomNumber(0, GenerationData.Width);
        //         position.y = Randomizer.RandomNumber(0, GenerationData.Height);
        //     }
        //     while (_map[(int)position.x, (int)position.y] == 1);

        //     position.x = -GenerationData.Width / 2 + position.x + 0.5F;
        //     position.y = -GenerationData.Height / 2 + position.y + 0.5F;
        //     return position;
        // }

    }

}
