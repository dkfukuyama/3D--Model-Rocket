using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Numerics;


namespace FSRPログ解析
{
    class Q_vals
    {
        public double gx, gy, gz, gabs, gabs_deg;

        public Q_vals()
        {

        }

        public Q_vals(double v1, double v2, double v3, double v4, double v5)
        {
            this.gx = v1;
            this.gy = v2;
            this.gz = v3;
            this.gabs = v4;
            this.gabs_deg = v5;
        }
        public Quaternion GetQuaternion()
        {
           return  new Quaternion(new Vector3D(gx, gy, gz), gabs_deg);
        }
        public override string ToString()
        {
            return String.Format("GX={0}, GY={1}, GZ={2}, GABS={3}, deg {4}", gx, gy, gz, gabs, gabs_deg);
        }
    }

    class Q_calc
    {
        List<Q_vals> qList;
        List<Quaternion> QuatList;
        public Q_calc()
        {
            qList = new List<Q_vals>();
            QuatList = new List<Quaternion>();
        }

        public string calc1(String s)
        {
            //初期姿勢
            //Z軸周り　	5.776094941
            //X軸周り	78.60804331

            var Q0 = new Quaternion(new Vector3D(1, 0, 0), 78.60804331) *

            new Quaternion(new Vector3D(0, 0, 1), 5.776094941);

            Console.WriteLine(Q0);

            String ret = "";
            var ar1 = s.Split(new String[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("----------------------------");




            var sw = new System.IO.StreamWriter("a.csv");



            for (int i = 1; i < ar1.Length; i++)
            {
                var ar2 = ar1[i].Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (ar2.Length < 5) break;
                var q = new Q_vals(double.Parse(ar2[0]), double.Parse(ar2[1]), double.Parse(ar2[2]), double.Parse(ar2[3])*0.25, double.Parse(ar2[4])*0.25);
                //Console.WriteLine(q);
                qList.Add(q);
                var Qua = q.GetQuaternion();
                Q0 = Q0 * Qua;
                QuatList.Add(Qua);

                sw.Write(Qua.ToString() + ",");
                sw.Write(Q0.ToString() + ",");

                Console.Write(Qua.ToString() + ",");
                Console.Write(Q0.ToString() + ",");

                var m = new Matrix3D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                m.Rotate(Q0);

                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33));
                Console.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33));

                
            }
            sw.Flush();
            sw.Close();
            return ret;
       }
    }
}
