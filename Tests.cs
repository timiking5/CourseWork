using CourseWork.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public static class Tests
    {
        public static void TestBellmanFord()
        {
            Console.WriteLine("Bellman-Ford algorithm");
            int source = 0;
            Graph graph = new(5);
            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 2, 7);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(2, 3, -3);
            graph.AddEdge(2, 4, 9);
            graph.AddEdge(3, 1, -2);
            graph.AddEdge(4, 0, 2);
            graph.AddEdge(4, 3, 7);
            bool ans = graph.BellmanFord(source);
            if (ans)
            {
                foreach (var vertex in graph.Vertexes)
                {
                    Console.WriteLine($"Distance between vertex {source} and {vertex.Num} = {vertex.Distance}");
                }
                return;
            }
            Console.WriteLine("Fail");
        }
        public static void TestDijkstra()
        {
            Console.WriteLine("Dijkstra algorithm");
            int source = 0;
            Graph graph = new(5);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(1, 3, 1);
            graph.AddEdge(2, 1, 3);
            graph.AddEdge(2, 3, 9);
            graph.AddEdge(2, 4, 2);
            graph.AddEdge(3, 4, 4);
            graph.AddEdge(4, 3, 6);
            graph.AddEdge(4, 0, 7);
            graph.Dijkstra(source);
            foreach (var vertex in graph.Vertexes)
            {
                Console.WriteLine($"Distance between vertex {source} and {vertex.Num} = {vertex.Distance}");
            }
        }
        public static void TestBFS()
        {
            Console.WriteLine("BFS Algorithm");
            int source = 0;
            Graph graph = new(6);
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 1);
            graph.AddEdge(0, 3, 1);
            graph.AddEdge(1, 4, 1);
            graph.AddEdge(1, 5, 1);
            graph.AddEdge(3, 5, 1);
            bool ans = graph.BFS(source);
            if (ans)
            {
                foreach (var vertex in graph.Vertexes)
                {
                    Console.WriteLine($"Distance between vertex {source} and {vertex.Num} = {vertex.Distance}");
                }
                return;
            }
            Console.WriteLine("FAIL");
        }
        public static void TestTopSort()
        {
            Console.WriteLine("Topological sort algorithm");
            Graph graph = new(9);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 8);
            graph.AddEdge(5, 8);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 7);
            graph.AddEdge(8, 7);
            List<int> result = graph.TopologicalSort();
            result.ForEach(Console.WriteLine);
        }
        public static void TestDAGShortestPath()
        {
            Graph graph = new(6);
            int source = 1;
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(0, 2, 3);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(1, 3, 6);
            graph.AddEdge(2, 3, 7);
            graph.AddEdge(2, 4, 4);
            graph.AddEdge(2, 5, 2);
            graph.AddEdge(3, 4, -1);
            graph.AddEdge(3, 5, 1);
            graph.AddEdge(4, 5, -2);
            graph.DAGShortestPath(source);
            foreach (var vertex in graph.Vertexes)
            {
                Console.WriteLine($"Distance between vertex {source} and {vertex.Num} = {vertex.Distance}");
            }
        }
        public static void TestAllShortestPathsPairs()
        {
            Console.WriteLine("All Shortest Paths Pairs");
            Graph graph = new(5);
            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 2, 7);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(2, 3, -3);
            graph.AddEdge(2, 4, 9);
            graph.AddEdge(3, 1, -2);
            graph.AddEdge(4, 0, 2);
            graph.AddEdge(4, 3, 7);
            (int[,] shortestPaths, int[,] allParents) = graph.AllShortestPathsPairs();
            Console.WriteLine("Path weights");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(shortestPaths[i, j]);
                    if (j != 4) Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.WriteLine("Parents");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(allParents[i, j]);
                    if (j != 4) Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
        public static void TestFloydWarshal()
        {
            Console.WriteLine("Floyd Warshall algorithm");
            Graph graph = new(5);
            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 2, 7);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(2, 3, -3);
            graph.AddEdge(2, 4, 9);
            graph.AddEdge(3, 1, -2);
            graph.AddEdge(4, 0, 2);
            graph.AddEdge(4, 3, 7);
            int[,] shortestPaths = graph.FloydWarshal();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(shortestPaths[i, j]);
                    if (j != 4) Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
        public static void RealWordExample()
        {
            Console.WriteLine("Real World Exapmle");
            Graph graph = new(7);
            Dictionary<int, string> cities = new()
            {
                { 0, "Казань" },
                { 1, "Мамадыш" },
                { 2, "Чистополь" },
                { 3, "Наб.Челны" },
                { 4, "Нижнекамск" },
                { 5, "Бугульма" },
                { 6, "Уфа" }
            };
            graph.AddEdge(0, 1, 163);
            graph.AddEdge(0, 2, 126);
            graph.AddEdge(1, 3, 95);
            graph.AddEdge(1, 4, 116);
            graph.AddEdge(2, 4, 102);
            graph.AddEdge(2, 5, 185);
            graph.AddEdge(4, 6, 332);
            graph.AddEdge(5, 6, 240);
            graph.AddEdge(3, 6, 291);
            graph.Dijkstra(0);
            Vertex currentVertex = graph.Vertexes[6];
            List<int> route = new();
            while (currentVertex.Num != 0)
            {
                route.Add(currentVertex.Num);
                currentVertex = graph.Vertexes[(int)currentVertex.Parent];
            }
            route.Add(0);
            route.Reverse();
            route.Remove(6);
            for (int i  = 0; i < route.Count; i++)
            {
                Console.Write($"{cities[route[i]]} --> ");
            }
            Console.Write("Уфа\n");
            Console.WriteLine($"Путь составляет {graph.Vertexes[6].Distance} км");
        }
    }
}
