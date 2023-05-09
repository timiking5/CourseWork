using CourseWork.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork;

public class Runner
{
    public static void Main(string[] args)
    {
        Tests.TestBellmanFord();
        Tests.TestDijkstra();
        Tests.TestBFS();
        Tests.TestTopSort();
        Tests.TestDAGShortestPath();
        Tests.TestAllShortestPathsPairs();
        Tests.TestFloydWarshal();
    }
    
}