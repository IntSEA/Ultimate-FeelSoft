using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes
{
    class Category
    {
        private double proba;
        private int[][] values;
        private int name;
        private double[] averege;
        private double[] vari;
        private int propiedades;
        private double[] plus;
        public Category(int name, double pr,int[][] values)
        {
            Proba = pr;
            this.name = name;
            this.values = values;
            Propiedades = values[0].Length;
            Averege = new double[Propiedades];
            Vari = new double[Propiedades];
            plus = new double[propiedades];
            CalculateAverage();
            //CalculateVar();

        }

        public int Propiedades { get => propiedades; set => propiedades = value; }
        public double[] Averege { get => averege; set => averege = value; }
        public int Name { get => name; set => name = value; }
        public double[] Vari { get => vari; set => vari = value; }
        public double Proba { get => proba; set => proba = value; }
        public double[] Plus { get => plus; set => plus = value; }

        private void CalculateAverage()
        {
            for (int i = 0; i < values[0].Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < values.Length; j++)
                {
                    sum += values[j][i];
                }
                plus[i] = sum;
                Averege[i] = sum / values.Length;
            }
            for (int i = 0; i < propiedades; i++)
            {
                plus[i] = (plus[i] + 1) / (plus.Sum()+propiedades);
            }
        }

        private void CalculateVar()
        {
            for (int i = 0; i < values[0].Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < values.Length; j++)
                {
                    sum += (values[j][i] - Averege[i]) * (values[j][i] - Averege[i]);
                }

                vari[i] = sum / (values.Length - 1);
            }
        }
    }
}

   
  