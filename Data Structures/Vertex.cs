using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data_Structures;

public class Vertex
{
    public int Num { get; set; }
    public int Distance { get; set; }
    public int? Parent { get; set; }
    public char Color { get; set; }
    public Vertex(int num, int distance, int? parent, char color = 'W')
    {
        Num = num;
        Distance = distance;
        Parent = parent;
        Color = color;
    }
}

