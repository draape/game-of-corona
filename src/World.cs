using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfCorona
{
    public class World
    {
        public IPerson[,] Population { get; set; }

        public void Populate(List<IPerson> persons)
        {
            if(Math.Sqrt(persons.Count) % 1 > 0) // TODO test
                throw new ArgumentException("The square of the population count must be an integer.");
            
            var gridSize = (int)Math.Ceiling(Math.Sqrt(persons.Count));
            var worldMap = new IPerson[gridSize, gridSize];
            
            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    worldMap[i, j] = persons[i * gridSize + j];
                }
            }

            Population = worldMap;
        }

        public void Sunrise()
        {
            // Save population snapshot to compare with
            for (var i = 0; i < Population.GetLength(0); i++)
            {
                for (var j = 0; j < Population.GetLength(1); j++)
                {
                    var person = Population[i, j];
                    var neighbours = GetNeighbours(i, j).Where(x => x != null);
                    
                    foreach (var neighbour in neighbours)
                    {
                        person.Meet(neighbour);
                    }
                }
            }
        }

        IEnumerable<IPerson> GetNeighbours(int i, int j)
        {
            yield return GetNeighbour(i - 1, j - 1);
            yield return GetNeighbour(i - 1, j);
            yield return GetNeighbour(i - 1, j + 1);
            yield return GetNeighbour(i, j - 1);
            yield return GetNeighbour(i, j + 1);
            yield return GetNeighbour(i + 1, j - 1);
            yield return GetNeighbour(i + 1, j);
            yield return GetNeighbour(i + 1, j + 1);
        }

        IPerson GetNeighbour(int i, int j) =>
            IsIndexWithinBounds(i, j)
                ? Population[i, j]
                : null;

        bool IsIndexWithinBounds(int i, int j) => 
            i >= 0 && j >= 0 && i < Population.GetLength(0) && j < Population.GetLength(1);

        public void Sunset()
        {
            if(Population is null)
                throw new Exception("The world is not populated");
            
            foreach (var person in Population)
            {
                person.Sleep();
            }
        }
    }
}