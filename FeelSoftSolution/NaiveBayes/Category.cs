using System.Linq;


namespace NaiveBayes
{
    class Category
    {
        private double proba;
        private int[][] values;
        private int name;
        private int propiedades;
        private double[] plus;
        public Category(int name, double pr,int[][] values)
        {
            Proba = pr;
            this.name = name;
            this.values = values;
            Propiedades = values[0].Length;
          
            plus = new double[propiedades];
            CalculateAverage();

        }

        public int Propiedades { get => propiedades; set => propiedades = value; }
        public int Name { get => name; set => name = value; }
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
            }
            for (int i = 0; i < propiedades; i++)
            {
                plus[i] = (plus[i] + 1) / (plus.Sum()+propiedades);
            }
        }
    }
}

   
  