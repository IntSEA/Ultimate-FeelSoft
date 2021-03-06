﻿namespace MainView
{
    partial class ReportPane
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.words = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sentencesTraining = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.failTraining = new System.Windows.Forms.Label();
            this.failDecided = new System.Windows.Forms.Label();
            this.sentencesDecided = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.words)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentencesTraining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentencesDecided)).BeginInit();
            this.SuspendLayout();
            // 
            // words
            // 
            chartArea1.AxisY.LabelStyle.Format = "{0.}%";
            chartArea1.Name = "ChartArea1";
            this.words.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.words.Legends.Add(legend1);
            this.words.Location = new System.Drawing.Point(21, 14);
            this.words.Name = "words";
            this.words.Size = new System.Drawing.Size(642, 303);
            this.words.TabIndex = 0;
            this.words.Text = "chart1";
            // 
            // sentencesTraining
            // 
            chartArea2.AxisY.LabelStyle.Format = "{0.}%";
            chartArea2.Name = "ChartArea1";
            this.sentencesTraining.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.sentencesTraining.Legends.Add(legend2);
            this.sentencesTraining.Location = new System.Drawing.Point(21, 323);
            this.sentencesTraining.Name = "sentencesTraining";
            this.sentencesTraining.Size = new System.Drawing.Size(509, 300);
            this.sentencesTraining.TabIndex = 1;
            this.sentencesTraining.Text = "chart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(669, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Error de Entrenamiento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(669, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Error de Decision";
            // 
            // failTraining
            // 
            this.failTraining.AutoSize = true;
            this.failTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failTraining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.failTraining.Location = new System.Drawing.Point(669, 72);
            this.failTraining.Name = "failTraining";
            this.failTraining.Size = new System.Drawing.Size(51, 20);
            this.failTraining.TabIndex = 4;
            this.failTraining.Text = "label3";
            // 
            // failDecided
            // 
            this.failDecided.AutoSize = true;
            this.failDecided.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failDecided.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.failDecided.Location = new System.Drawing.Point(669, 182);
            this.failDecided.Name = "failDecided";
            this.failDecided.Size = new System.Drawing.Size(51, 20);
            this.failDecided.TabIndex = 5;
            this.failDecided.Text = "label3";
            // 
            // sentencesDecided
            // 
            chartArea3.AxisY.LabelStyle.Format = "{0.}%";
            chartArea3.Name = "ChartArea1";
            this.sentencesDecided.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.sentencesDecided.Legends.Add(legend3);
            this.sentencesDecided.Location = new System.Drawing.Point(554, 323);
            this.sentencesDecided.Name = "sentencesDecided";
            this.sentencesDecided.Size = new System.Drawing.Size(506, 300);
            this.sentencesDecided.TabIndex = 6;
            this.sentencesDecided.Text = "chart2";
            // 
            // ReportPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sentencesDecided);
            this.Controls.Add(this.failDecided);
            this.Controls.Add(this.failTraining);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sentencesTraining);
            this.Controls.Add(this.words);
            this.Name = "ReportPane";
            this.Size = new System.Drawing.Size(1078, 630);
            this.Load += new System.EventHandler(this.ReportPane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.words)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentencesTraining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentencesDecided)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart words;
        private System.Windows.Forms.DataVisualization.Charting.Chart sentencesTraining;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label failTraining;
        private System.Windows.Forms.Label failDecided;
        private Controller.Controller controller;
        private System.Windows.Forms.DataVisualization.Charting.Chart sentencesDecided;
    }
}
