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

            #region Buổi 1
            ////Bài 1
            //gr.ReadFile2Graph("BacDoThiVoHuong.INP");
            //gr.WriteDegUndGraph2File("BacDoThiVoHuong.OUT");

            ////Bài 2
            //gr.ReadFile2Graph("BacVaoRa.INP");
            //gr.WriteDegDirGraph2File("BacVaoRa.OUT");

            ////Bài 3
            //gr.ReadFile2LstGraph("DanhSachKe.INP");
            //gr.WriteDegVerticesGraph("DanhSachKe.OUT");

            ////Bài 4
            //gr.ReadFile2EdgeGraph("DanhSachCanh.INP");
            //gr.WriteDegEdgesGraph("DanhSachCanh.OUT");
            #endregion

            #region Buổi 2
            //Bài 1
            //gr.ReadFile2EdgeGraph("Canh2DSKe.INP");
            //gr.Canh2DSKe("Canh2DSKe.OUT");

            //Bài 2
            gr.ReadFile2LstGraph("DSKe2Canh.INP");
            gr.DSKe2Canh("DSKe2Canh.OUT");

            //Bài 3
            //gr.ReadFile2Graph("BonChua.INP");
            //gr.BonChua("BonChua.OUT");

            //Bài 4
            //gr.ReadFile2Edges("TrungBinhCanh.INP");
            //gr.TrungBinhCanh("TrungBinhCanh.OUT");
            #endregion
        }
    }
}
