using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace LyThuyetDoThi
{
    class Graph
    {
        int[,] arrGraph;

        LinkedList<int>[] lstGraph;

        Tuple<int, int>[] edgeGraph;

        int numVertices;

        int numEdges;

        LinkedList<(int, int, int)> edges;

        public Graph() { }

        #region Đọc đồ thị
        //Ma trận kề
        public void ReadFile2Graph(string fname)
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

        //Danh sách kề
        public void ReadFile2LstGraph(string fname)
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

        //Danh sách cạnh
        public void ReadFile2EdgeGraph(string fname)
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

        //Danh sách cạnh có trọng số
        public void ReadFile2Edges(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            edges = new LinkedList<(int, int, int)>();
            for (int i = 1; i < numEdges + 1; i++)
            {
                line = lines[i].Split(' ');
                edges.AddLast((Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()), Int32.Parse(line[2].Trim())));
            }
        }
        #endregion

        #region Buổi 1
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
        #endregion

        #region Buổi 2
        //Bài 1: Chuyển danh sách cạnh sang danh sách kề
        public void Canh2DSKe(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                sw.Write(numVertices);
                string s = "\n";
                for(int i = 0; i < numVertices; i++)
                {
                    foreach(Tuple<int,int> a in edgeGraph)
                    {
                        if (a.Item1 == i + 1)
                            s = s + a.Item2 + " ";
                        if (a.Item2 == i + 1)
                            s = s + a.Item1 + " ";
                    }
                    sw.Write(s.TrimEnd());
                    s = "\n";
                }
            }
        }

        //Bài 2: Chuyển danh sách kề sang danh sách cạnh
        public void DSKe2Canh(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                int SumDeg = 0;
                foreach (LinkedList<int> a in lstGraph)
                    SumDeg += a.Count();
                numEdges = SumDeg / 2;
                sw.Write("{0} {1}", numVertices, numEdges);
                int temp = 0;
                int edge = 0;
                for(int i = 0; i < numVertices; i++)
                {
                    foreach(var a in lstGraph[i])
                    {
                        if (a > i + 1) sw.Write(String.Format("\n{0} {1}", i + 1, a));
                        edge++;
                    }
                    if (edge == numEdges) break;
                }
            }
        }

        //Bài 3: Bồn chứa
        public void BonChua(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                LinkedList<int> temp = new LinkedList<int>();
                int bonChua = 0;
                for (int i = 0; i < numVertices; i++)
                {
                    int inDegree = 0;
                    int outDegree = 0;
                    for (int j = 0; j < numVertices; j++)
                    {
                        inDegree += arrGraph[j, i];
                        outDegree += arrGraph[i, j];
                    }
                    if (inDegree > 0 && outDegree == 0)
                    {
                        bonChua++;
                        temp.AddLast(i + 1);
                    }
                }
                if (bonChua != 0)
                {
                    sw.WriteLine(bonChua);
                    foreach (var a in temp)
                        sw.Write(a + " ");
                }
                else sw.Write(0);
            }
        }

        //Bài 4: Độ dài trung bình của cạnh
        public void TrungBinhCanh(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                int maxEdge = edges.ElementAt(0).Item3;
                int sumEdge = maxEdge;
                int countEdges = 1;
                LinkedList<(int, int, int)> temp = new LinkedList<(int, int, int)>();
                for(int i = 1; i < numEdges; i++)
                {
                    if (edges.ElementAt(i).Item3 > maxEdge)
                    {
                        maxEdge = edges.ElementAt(i).Item3;
                        countEdges = 1;
                    }
                    else if (edges.ElementAt(i).Item3 == maxEdge) countEdges++;
                    sumEdge += edges.ElementAt(i).Item3;
                }
                sw.Write(String.Format("{0} \n{1}", sumEdge / numEdges, countEdges));
                foreach (var a in edges)
                    if (maxEdge == a.Item3) sw.Write("\n{0} {1} {2}", a.Item1, a.Item2, a.Item3);
            }
        }
        #endregion

        #region Buổi 3 BFS

        //Cấu trúc dữ liệu
        int n, s, x, y;
        //Danh sách các đỉnh đã viếng thăm
        LinkedList<int> vertices;

        //Các biến hỗ trợ
        Queue<int> q;
        bool[] visited;
        int[] pre;
        int[] distances;

        public void BFS_Visit(int s)
        {
            visited = new bool[n];
            pre = new int[n];
            distances = new int[n];
            vertices = new LinkedList<int>();
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
                pre[i] = -1;
                distances[i] = 0;
            }

            q = new Queue<int>();
            q.Enqueue(s);

            visited[s] = true;
            vertices.AddLast(s);

            while (q.Count != 0)
            {
                int u = q.Dequeue();

                foreach (int v in lstGraph[u])
                {
                    if (visited[v]) continue;

                    visited[v] = true;
                    q.Enqueue(v);
                    pre[v] = u;
                    distances[v] = distances[u] + 1;
                    vertices.AddLast(v);
                }
            }
        }

        //BFS_Duyệt các đỉnh cùng miền liên thông với s
        public void ReadFileBFS(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            n = Int32.Parse(line[0].Trim());
            s = Int32.Parse(line[1].Trim()) - 1;
            lstGraph = new LinkedList<int>[n];
            for (int i = 1; i < n+1; i++)
            {
                line = lines[i].Split(' ');
                lstGraph[i - 1] = new LinkedList<int>();
                if (line[0] == "") continue;
                for (int j = 0; j < line.Length; j++)
                    lstGraph[i - 1].AddLast(Int32.Parse(line[j].Trim())-1);
            }
        }

        public void WriteXtoAllVertices(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                BFS_Visit(s);
                sw.WriteLine(vertices.Count() - 1);
                for(int i = 1;i<vertices.Count; i++)
                {
                    sw.Write(String.Format("{0,-3}", vertices.ElementAt(i) + 1));
                }
            }
        }

        //BFS_Duyệt đường đi từ x -> y
        LinkedList<int> path;

        public void ReadFileBFSXtoY(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            n = Int32.Parse(line[0].Trim());
            x = Int32.Parse(line[1].Trim()) - 1;
            y = Int32.Parse(line[2].Trim()) - 1;
            lstGraph = new LinkedList<int>[n];
            for (int i = 1; i < n + 1; i++)
            {
                line = lines[i].Split(' ');
                lstGraph[i - 1] = new LinkedList<int>();
                if (line[0] == "") continue;
                for (int j = 0; j < line.Length; j++)
                    lstGraph[i - 1].AddLast(Int32.Parse(line[j].Trim()) - 1);
            }
        }

        void FindPath(int t)
        {
            for (int i = t; i != -1; i = pre[i]) path.AddFirst(i);
        }

        public void WriteRoadXtoY(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                BFS_Visit(x);
                path = new LinkedList<int>();
                FindPath(y);
                sw.WriteLine(distances[y]+1);
                for (int i = 0; i < path.Count(); i++)
                {
                    sw.Write(String.Format("{0,-3}", path.ElementAt(i)+1));
                }
            }
        }
        //BFS_Kiểm tra liên thông
        public void ReadFileBFSConnect(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            n = Int32.Parse(lines[0].Trim());
            lstGraph = new LinkedList<int>[n];
            for (int i = 1; i < n + 1; i++)
            {
                string[] line = lines[i].Split(' ');
                lstGraph[i - 1] = new LinkedList<int>();
                if (line[0] == "") continue;
                for (int j = 0; j < line.Length; j++)
                    lstGraph[i - 1].AddLast(Int32.Parse(line[j].Trim()) - 1);
            }
        }

        public void IsConnect(string fname)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                BFS_Visit(0);
                if (vertices.Count() == n) sw.Write("YES");
                else sw.Write("NO");
            }
        }

        //BFS_Đếm miền liên thông
        void BFS_NumPath(int s)
        {
            visited[s] = true;
            q = new Queue<int>();
            q.Enqueue(s);
            while (q.Count != 0)
            {
                int u = q.Dequeue();

                foreach (int v in lstGraph[u])
                {
                    if (visited[v]) continue;

                    visited[v] = true;
                    q.Enqueue(v);
                }
            }
        }

        public void WriteNumPath(string fname)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fname))
            {
                int dem = 0;
                visited = new bool[n];
                for (int i = 0; i < visited.Length; i++) visited[i] = false;
                for (int i = 0; i < visited.Length; i++)
                {
                    if (!visited[i])
                    {
                        BFS_NumPath(i);
                        dem++;
                    }
                }
                sw.Write(dem);
            }
        }
        #endregion
    }
}