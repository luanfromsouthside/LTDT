using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi
{
    class Graph
    {
        int[,] arrGraph;

        LinkedList<int>[] lstGraph;

        Tuple<int, int>[] edgeGraph;

        int numVertices;

        int numEdges;

        public Graph() { }

        //Đọc đồ thị câu 1 và 2 - ma trận kề
        public void ReadFile2GraphV1(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            numVertices = Int32.Parse(lines[0].Trim());
            arrGraph = new int[numVertices, numVertices];
            for(int i = 1; i < numVertices + 1; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < numVertices; j++)
                    arrGraph[i - 1, j] = Int32.Parse(line[j].Trim());
            }
        }

        //Đọc đồ thị câu 3 - danh sách kề
        public void ReadFile2GraphV2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            numVertices = Int32.Parse(lines[0].Trim());
            lstGraph = new LinkedList<int>[numVertices];
            for (int i = 1; i < numVertices + 1; i++)
            {
                string[] line = lines[i].Split(' ');
                lstGraph[i - 1] = new LinkedList<int>();
                for (int j = 0; j < line.Length; j++)
                    lstGraph[i - 1].AddLast(Int32.Parse(line[j].Trim()));
            }
        }

        //Đọc đồ thị câu 4 - danh sách cạnh
        public void ReadFile2GraphV3(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            edgeGraph = new Tuple<int, int>[numEdges]; 
            for(int i = 1; i < numEdges + 1; i++)
            {
                line = lines[i].Split(' ');
                edgeGraph[i - 1] = new Tuple<int, int>(Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()));
            }
        }

        //Câu 1: Bậc đồ thị vô hướng
        public void WriteDegUndGraph2File(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                sw.WriteLine(numVertices);
                for(int i = 0; i < numVertices; i++)
                {
                    int degree = 0;
                    for (int j = 0; j < numVertices; j++)
                        degree += arrGraph[i, j];
                    sw.Write(String.Format("{0,-3}", degree));
                }
            }
        }

        //Câu 2: Bậc vào - Bậc ra đồ thị có hướng
        public void WriteDegDirGraph2File(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                sw.WriteLine(numVertices);
                for (int i = 0; i < numVertices; i++)
                {
                    int inDegree = 0;
                    int outDegree = 0;
                    for (int j = 0; j < numVertices; j++)
                    {
                        inDegree += arrGraph[j, i];
                        outDegree += arrGraph[i, j];
                    }
                    sw.WriteLine(String.Format("{0,-3}{1,-3}", inDegree, outDegree));
                }
            }
        }

        //Câu 3: Bậc đồ thị danh sách kề
        public void WriteDegVerticesGraph(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                sw.WriteLine(numVertices);
                for (int i = 0; i < numVertices; i++)
                    sw.Write(String.Format("{0,-3}", lstGraph[i].Count));
            }
        }

        //Câu 4: Bậc đồ thị danh sách cạnh
        public void WriteDegEdgesGraph(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                sw.WriteLine(numVertices);
                int[] temp = new int[numVertices];
                for (int i = 0; i < numVertices; i++) temp[i] = 0;
                for (int i = 0; i < numEdges; i++)
                {
                    temp[edgeGraph[i].Item1 - 1]++;
                    temp[edgeGraph[i].Item2 - 1]++;
                }
                for (int i = 0; i < numVertices; i++)
                    sw.Write(String.Format("{0,-3}", temp[i]));
            }
        }
    }
}
