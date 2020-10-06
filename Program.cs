using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph gr = new Graph();
            //Bài 1
            gr.ReadFile2GraphV1("BacDoThiVoHuong.INP");
            gr.WriteDegUndGraph2File("BacDoThiVoHuong.OUT");

            //Bài 2
            gr.ReadFile2GraphV1("BacVaoRa.INP");
            gr.WriteDegDirGraph2File("BacVaoRa.OUT");

            //Bài 3
            gr.ReadFile2GraphV2("DanhSachKe.INP");
            gr.WriteDegVerticesGraph("DanhSachKe.OUT");

            //Bài 4
            gr.ReadFile2GraphV3("DanhSachCanh.INP");
            gr.WriteDegEdgesGraph("DanhSachCanh.OUT");

            Console.ReadKey();
        }
    }
}
