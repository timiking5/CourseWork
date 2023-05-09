using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourseWork.Data_Structures;

public class Graph
{
    public int NumOfVertexes = 0;
    public List<Vertex> Vertexes { get; set; } = new();  // (int num, int distance, int parent)
    public List<List<(int to, int weight)>> Edges { get; set; } = new();
    public Graph(int n)
    {
        NumOfVertexes = n;
        for (int i = 0; i < n; i++)
        {
            Vertexes.Add(new (i, int.MaxValue / 2, null));
            Edges.Add(new List<(int to, int weight)>());
        }
    }
    public void AddEdge(int from, int to, int weigth = 1)
    {
        if (from >= NumOfVertexes)
        {
            return;
        }
        Edges[from].Add((to, weigth));
    }
    private void InitialzieSingleSource(int source = 0)
    {
        for (int i = 0; i < NumOfVertexes; i++)
        {
            Vertexes[i].Distance = int.MaxValue / 2;
            Vertexes[i].Parent = null;
        }
        Vertexes[source].Distance = 0;
    }
    private void Relax(int u, int v, int w)
    {
        if (Vertexes[v].Distance > Vertexes[u].Distance + w)
        {
            Vertexes[v].Distance = Vertexes[u].Distance + w;
            Vertexes[v].Parent = u;
        }
    }
    public bool BellmanFord(int source)
    {
        InitialzieSingleSource(source);
        for (int _ = 1; _ < NumOfVertexes - 1; _++)
        {
            for (int i = 0; i < Edges.Count; i++)
            {
                foreach (var edge in Edges[i])
                {
                    Relax(i, edge.to, edge.weight);
                }
            }
        }
        for (int i = 0; i < Edges.Count; i++)
        {
            foreach (var edge in Edges[i])
            {
                if (Vertexes[edge.to].Distance > Vertexes[i].Distance + edge.weight)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void Dijkstra(int source)
    {
        InitialzieSingleSource(source);
        HashSet<int> relaxedVertexes = new();
        while (relaxedVertexes.Count <  NumOfVertexes)
        {
            Vertex u = Vertexes.Where(x => !relaxedVertexes.Contains(x.Num)).MinBy(x => x.Distance);
            relaxedVertexes.Add(u.Num);
            foreach (var edge in Edges[u.Num])
            {
                Relax(u.Num, edge.to, edge.weight);
            }
        }
    }
    public bool BFS(int source)
    {
        if (!BFSAppliable())
        {
            return false;
        }
        InitialzieSingleSource(source);
        Queue<int> queue = new();
        HashSet<int> visited = new();
        visited.Add(source);
        queue.Enqueue(source);
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (var edge in Edges[current])
            {
                if (!visited.Contains(edge.to))
                {
                    queue.Enqueue(edge.to);
                    visited.Add(edge.to);
                    Vertexes[edge.to].Distance = Vertexes[current].Distance + 1;
                    Vertexes[edge.to].Parent = current;
                }
            }
        }
        return true;
    }
    private bool BFSAppliable()
    {
        for (int i = 0; i < Edges.Count; i++)
        {
            foreach (var edge in Edges[i])
            {
                if (edge.weight != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void DAGShortestPath(int source)
    {
        List<int> vertexes = TopologicalSort();
        InitialzieSingleSource(source);
        for (int i = 0; i < vertexes.Count; i++)
        {
            foreach (var edge in Edges[vertexes[i]])
            {
                Relax(vertexes[i], edge.to, edge.weight);
            }
        }
    }
    public List<int> TopologicalSort()
    {
        List<int> result = new();
        foreach (var vertex in Vertexes)
        {
            vertex.Color = 'W';  // White
            vertex.Parent = null;
        }
        for (int i = 0; i < NumOfVertexes; i++)
        {
            if (Vertexes[i].Color == 'W')
            {
                DFSVisit(Vertexes[i], result);
            }
        }
        return result;
    }
    private void DFSVisit(Vertex vertex, List<int> result)
    {
        vertex.Color = 'G';  // Gray
        foreach (var edge in Edges[vertex.Num])
        {
            if (Vertexes[edge.to].Color == 'W')
            {
                Vertexes[edge.to].Parent = vertex.Num;
                DFSVisit(Vertexes[edge.to], result);
            }
        }
        vertex.Color = 'B';
        result.Insert(0, vertex.Num);
    }
    public (int[,], int[,]) AllShortestPathsPairs()
    {
        int[,] allShortestPaths = InitializeMatrix();
        int[,] allParents = InitializeMatrix();
        for (int i = 0; i < NumOfVertexes; i++)
        {
            BellmanFord(i);
            // Dijkstra(i);
            foreach (var vertex in Vertexes)
            {
                allShortestPaths[i, vertex.Num] = vertex.Distance;
                allParents[i, vertex.Num] = vertex.Parent == null ? -1 : (int)vertex.Parent;
            }
        }
        return (allShortestPaths, allParents);
    }
    private int[,] InitializeWeightsMatrix()
    {
        int[,] matrix = new int[NumOfVertexes, NumOfVertexes];
        for (int i = 0; i < NumOfVertexes; i++)
        {
            for (int j = 0; j < NumOfVertexes; j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 0;
                    continue;
                }
                var edge = Edges[i].Where(x => x.to == j).FirstOrDefault();
                if (edge.to == 0 && edge.weight == 0)
                {
                    matrix[i, j] = int.MaxValue / 2;
                    continue;
                }
                matrix[i, j] = edge.weight;
            }
        }
        return matrix;
    }
    private int[,] InitializeMatrix(bool fl = false)
    {
        int[,] matrix = new int[NumOfVertexes, NumOfVertexes];
        if (fl)
        {
            InitializeSolutionMatrix(matrix);
        }
        return matrix;
    }
    private void InitializeSolutionMatrix(int[,] matrix)
    {
        for (int i = 0; i < NumOfVertexes; i++)
        {
            for (int j = 0; j < NumOfVertexes; j++)
            {
                if (i == j)
                {
                    matrix[i ,j] = 0;
                    continue;
                }
                matrix[i, j] = int.MaxValue / 2;
            }
        }
    }
    
    public int[,] FloydWarshal()
    {
        int[,] shortestPaths = InitializeWeightsMatrix();
        for (int k = 0; k < NumOfVertexes; k++)
        {
            for (int i = 0; i < NumOfVertexes; i++)
                for (int j = 0; j < NumOfVertexes; j++)
                    if (shortestPaths[i, k] + shortestPaths[k, j] < shortestPaths[i, j])
                        shortestPaths[i, j] = shortestPaths[i, k] + shortestPaths[k, j];
        }
        return shortestPaths;
    }
    public void PrintAllPairPaths(int[,] parents, int i, int j)
    {
        if (i == j)
        {
            Console.Write($"{i} ");
        }
        else if (parents[i, j] == -1)
        {
            Console.WriteLine($"Пути из {i} в {j} не существует");
        }
        else
        {
            PrintAllPairPaths(parents, i, parents[i, j]);
            Console.Write($"{j} ");
        }
    }
}
