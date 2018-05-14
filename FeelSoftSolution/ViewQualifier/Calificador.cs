
using Qualifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewQualifier
{
    public partial class Calificador : Form
    {
        public Calificador()
        {
           
            InitializeComponent();
           

            clasificador = new Clasificador();
            buExaminar.Enabled = false;

            butListo.Enabled = false;
        }

        private void BuExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                textDireccion.Text = search.FileName;


            }
            clasificador.Load(search.FileName, textRuta2.Text);
            textCrudo.Text = clasificador.RawText[clasificador.Counter];
            textProcesado.Text = clasificador.ProcessedText[clasificador.Counter];

            //dejo listo la lista de calificaciones
            clasificador.LoadWordList(textRuta2.Text);

            int i = 0;
            foreach (var line in clasificador.WorldList)
            {
                dataGridPalabras.Rows.Add(clasificador.WorldList[i], clasificador.CalificationList[i]);
                i++;
            }

            labContador.Text = clasificador.Counter + " de " + clasificador.RawText.Count;
        }

       

        private void ButSiguiente_Click(object sender, EventArgs e)
        {
            clasificador.Counter++;
            textCrudo.Text = clasificador.RawText[clasificador.Counter];
            textProcesado.Text = clasificador.ProcessedText[clasificador.Counter];
        }

        private void ButExaminar2_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();
            if (search.ShowDialog() == DialogResult.OK)
            {
                textRuta2.Text = search.FileName;
                buExaminar.Enabled = true;
            }
        }

        private void ViewTextualProcessor_Load(object sender, EventArgs e)
        {

        }

     
        private void ButListo_Click(object sender, EventArgs e)

        {
            string[] valueCol = new string[dataGridPalabras.RowCount];
            Boolean valueTrue = true;
            for (int i = 0; i < dataGridPalabras.RowCount - 1 && valueTrue; i++)
            {
                valueCol[i] = dataGridPalabras.Rows[i].Cells[1].Value.ToString();
                if (valueCol[i] == "none")
                {
                    valueTrue = false;
                    MessageBox.Show("Alguna(s) de las palabras no han sido calificadas");
                }
            }

            if (textCalificacion.Text == "")
            {
                MessageBox.Show("Debe calificar la frase");
            }
            else
            {
                StreamWriter stream = File.AppendText(textBox1.Text);
                stream.WriteLine(textCrudo.Text + "|" + textProcesado.Text + "|" + textCalificacion.Text);
                stream.Close();

                if (valueTrue)
                {
                    foreach (var item in clasificador.WorldList)
                    {

                        StreamReader reader = new StreamReader(textRuta2.Text);
                        string sLine = reader.ReadLine();
                        Boolean appertain = false;

                        while (sLine != null)
                        {

                            string[] dic = sLine.Split('|');
                            if (item.Equals(dic[0]))
                            {
                                appertain = true;
                            }
                            sLine = reader.ReadLine();
                        }
                        reader.Close();
                        if (!appertain)
                        {
                            StreamWriter writer = File.AppendText(textRuta2.Text);
                            writer.WriteLine(item + "|" + valueCol[clasificador.WorldList.IndexOf(item)]);

                            writer.Close();





                        }


                    }
                }
                clasificador.Counter++;
                textCrudo.Text = clasificador.RawText[clasificador.Counter];
                textProcesado.Text = clasificador.ProcessedText[clasificador.Counter];

                clasificador.UpdateWordList(textRuta2.Text);
                clasificador.LoadWordList(textRuta2.Text);
                int k = 0;
                dataGridPalabras.Rows.Clear();
                foreach (var line in clasificador.WorldList)
                {

                    dataGridPalabras.Rows.Add(clasificador.WorldList[k], clasificador.CalificationList[k]);
                    k++;
                }
                textCalificacion.Text = "";
                labContador.Text = clasificador.Counter + " de " + clasificador.RawText.Count;

                clasificador.SaveCounter(clasificador.Counter, textDireccion.Text);
            }
        }
        

       

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();
            if (search.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = search.FileName;
                butListo.Enabled = true;
            }
        }


        private void BtAddCompoundExpression_Click(object sender, EventArgs e)
        {
            /*String compoundWord = Interaction.InputBox("Ingrese nuevo StopWord");

            if (String.IsNullOrEmpty(compoundWord) || String.IsNullOrWhiteSpace(compoundWord))
            {
                MessageBox.Show("Ingrese una clave no vacia");
            }
            else
            {
                processor.addCompoundWord(compoundWord);

            }

            processor.analysisCompoundsText();
            update();


            labContador.Text = clasificador.Counter + " de " + clasificador.RawText.Count;*/
        }

       
    }
}
