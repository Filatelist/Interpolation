using System;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab1
{
    class Program
    {
        static void Main(string[] args) 
        {
            try
            {
                double[] massiv_x = new double[] { 0.88, 1.68, 2.3, 2.8, 3.5, 4.11, 4.78, 5, 6.5, 7.2, 8.9, 9.3, 9.33, 9.89, 10.2 };
                double[] massiv_y = new double[] { 0.59490, -0.10877, -0.61806, -0.80887, -0.80546, -0.53678, 0.06751, 0.27987, 0.82859, 0.57152, -0.76138, -0.83725, -0.83904, -0.77941, -0.65506 };
                double[] massiv_y_ = new double[91];
                cycle(massiv_y_);
                double x = 0;
                double[] massiv_y_quad = new double[91];
                int n = 0;
                double[] massiv_y_Lagr = new double[91];
                for (int i = 0; i <= 90; i++)
                {  
                    x = 0.1 * i + 1;
                    n = closerNeighbor(x, massiv_x);
                    massiv_y_quad[i] = quadApprox(massiv_x, massiv_y, n, x);
                    massiv_y_Lagr[i] = lagrApprox(massiv_x, massiv_y, x);
                }

                double[] dif_massiv_y_acc = new double[91];
                cycle_2(dif_massiv_y_acc);
                double[] dif_massiv_y_quad = new double[91];
                for (int i = 0; i <= 90; i++)
                {
                    x = 0.1 * i + 1;
                    n = closerNeighbor(x, massiv_x);
                    dif_massiv_y_quad[i] = quadApprox_dif(massiv_x, massiv_y, n, x);

                }
                quadApprox_dif(massiv_x, massiv_y, n, x);
                double[] dif_massiv__y_acc = new double[91];
                cycle_(dif_massiv__y_acc);
                double[] dif2_massiv_y_quad = new double[91];
                for (int i = 0; i <= 90; i++)
                {
                    x = 0.1 * i + 1;
                    n = closerNeighbor(x, massiv_x);
                    dif2_massiv_y_quad[i] = quadApprox_dif2(massiv_x, massiv_y, n, x);
                }

                Console.WriteLine("x\ty_acc\ty_quad\ty_Lagr\tx'_acc\tx'_quad\tx''_acc\tx''_quad");
                for (int counter = 0; counter <= 90; counter++)
                {
                    Console.WriteLine($"{0.1 * counter + 1}\t{massiv_y_[counter]:F3}\t{massiv_y_quad[counter]:F3}\t{massiv_y_Lagr[counter]:F3}\t{dif_massiv_y_acc[counter]:F3}\t{dif_massiv_y_quad[counter]:F3}\t{dif_massiv__y_acc[counter]:F3}\t{dif2_massiv_y_quad[counter]:F3}");
                }
                Console.ReadKey();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        static void cycle(double[] massiv_y)
        {
            for (int i = 0; i <= 90; i++)
            {
                massiv_y[i] = Foo(0.1 * i + 1);
            }
        }
        static double Foo(double x)
        {
            return (Math.Sin(Math.Cos(x)));
        }
        static int closerNeighbor(double x, double[] massiv_x)
        {
            double value = 10;
            double zna4enie = 1;
            int number = 0;
            for (int i = 0; i < massiv_x.Length - 2; i++)
            {
                zna4enie = Math.Abs(x - massiv_x[i]) + Math.Abs(x - massiv_x[i + 1]) + Math.Abs(massiv_x[i + 2]);
                if (zna4enie < value)
                {
                    value = zna4enie;
                    number = i;
                }
            }
            return number;
        }
        
        static double quadApprox(double[] massiv_x, double[] massiv_y, int n, double x)
        {
            double a;
            double b;
            double c;
            a = (massiv_y[n + 2] - massiv_y[n]) / ((massiv_x[n + 2] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])) - ((massiv_y[n + 1] - massiv_y[n]) / ((massiv_x[n + 1] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])));
            b = (massiv_y[n + 1] - massiv_y[n]) / (massiv_x[n + 1] - massiv_x[n]) - a * (massiv_x[n + 1] + massiv_x[n]);
            c = massiv_y[n] - b * massiv_x[n] - a * massiv_x[n] * massiv_x[n];

            return (a * x * x + b * x + c);
        }
        static double lagrApprox(double[] massiv_x, double[] massiv_y, double x)
        {
            double l_n = 0;
            double l_i = 1;
            for (int i = 0; i < massiv_x.Length; i++)
            {
                for (int j = 0; j < massiv_x.Length; j++)
                {
                    if (i == j)
                        continue;
                    else l_i *= (x - massiv_x[j]) / (massiv_x[i] - massiv_x[j]);
                }
                l_n += l_i * massiv_y[i];
                l_i = 1;
            }
            return l_n;
        }
        static double Foo_2(double x)
        {
            return (Math.Sin(x) * (-Math.Cos(Math.Cos(x))));
        }
        static double Foo_3(double x)
        {
            return (Math.Sin(x) * Math.Sin(x) * (-Math.Sin(Math.Cos(x))) - Math.Cos(Math.Cos(Math.Cos(x))));
        }
        
        static void cycle_2(double[] dif_massiv_y_acc)
        {
            for (int i = 0; i < 90; i++)
            {
                dif_massiv_y_acc[i] = Foo_2(0.1 * i + 1);
            }
        }
        static double quadApprox_dif(double[] massiv_x, double[] massiv_y, int n, double x)
        {
            double a;
            double b;
            a = (massiv_y[n + 2] - massiv_y[n]) / ((massiv_x[n + 2] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])) - ((massiv_y[n + 1] - massiv_y[n]) / ((massiv_x[n + 1] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])));
            b = (massiv_y[n + 1] - massiv_y[n]) / (massiv_x[n + 1] - massiv_x[n]) - a * (massiv_x[n + 1] + massiv_x[n]);
            return (2 * a * x + b);
        }
        static void cycle_(double[] dif2_massiv_y_acc)
        {
            for (int i = 0; i < 90; i++)
            {
                dif2_massiv_y_acc[i] = Foo_3(0.1 * i + 1);
            }
        }
        static double quadApprox_dif2(double[] massiv_x, double[] massiv_y, int n, double x)
        {
            double a;
            a = (massiv_y[n + 2] - massiv_y[n]) / ((massiv_x[n + 2] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])) - ((massiv_y[n + 1] - massiv_y[n]) / ((massiv_x[n + 1] - massiv_x[n]) * (massiv_x[n + 2] - massiv_x[n + 1])));
            return 2 * a;
        }
    }

}






