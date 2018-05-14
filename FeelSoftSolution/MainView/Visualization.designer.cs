
using System.Collections.Generic;

namespace MainView
{
    partial class Visualization
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lbStart = new System.Windows.Forms.Label();
            this.lbEnd = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbFacebook = new System.Windows.Forms.CheckBox();
            this.cbTwitter = new System.Windows.Forms.CheckBox();
            this.cbNaive = new System.Windows.Forms.CheckBox();
            this.cbDictionary = new System.Windows.Forms.CheckBox();
            this.lbTechniques = new System.Windows.Forms.Label();
            this.lbNetwork = new System.Windows.Forms.Label();
            this.cbBoth = new System.Windows.Forms.CheckBox();
            this.lbFavorability = new System.Windows.Forms.Label();
            this.cbFavorability = new System.Windows.Forms.CheckBox();
            this.cbNeutro = new System.Windows.Forms.CheckBox();
            this.cbUnfavorable = new System.Windows.Forms.CheckBox();
            this.chListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.LabelStyle.Format = "{0.}%";
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(25, 105);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.MediumSpringGreen,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Green,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))))};
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(873, 353);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(142, 34);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(368, 34);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAceptar.Location = new System.Drawing.Point(595, 26);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(99, 36);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lbStart
            // 
            this.lbStart.AutoSize = true;
            this.lbStart.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStart.ForeColor = System.Drawing.Color.White;
            this.lbStart.Location = new System.Drawing.Point(139, 18);
            this.lbStart.Name = "lbStart";
            this.lbStart.Size = new System.Drawing.Size(144, 17);
            this.lbStart.TabIndex = 4;
            this.lbStart.Text = "Indique fecha de inicio";
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEnd.ForeColor = System.Drawing.Color.White;
            this.lbEnd.Location = new System.Drawing.Point(365, 18);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(179, 17);
            this.lbEnd.TabIndex = 5;
            this.lbEnd.Text = "Indique fecha de finalización";
            // 
            // chart2
            // 
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(25, 480);
            this.chart2.Name = "chart2";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Comentarios";
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(873, 216);
            this.chart2.TabIndex = 8;
            this.chart2.Text = "chart2";
            title2.Name = "Title1";
            title2.Text = "Número de comentarios";
            this.chart2.Titles.Add(title2);
            // 
            // cbFacebook
            // 
            this.cbFacebook.AutoSize = true;
            this.cbFacebook.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFacebook.ForeColor = System.Drawing.Color.White;
            this.cbFacebook.Location = new System.Drawing.Point(932, 319);
            this.cbFacebook.Name = "cbFacebook";
            this.cbFacebook.Size = new System.Drawing.Size(103, 24);
            this.cbFacebook.TabIndex = 9;
            this.cbFacebook.Text = "Facebook";
            this.cbFacebook.UseVisualStyleBackColor = true;
            // 
            // cbTwitter
            // 
            this.cbTwitter.AutoSize = true;
            this.cbTwitter.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTwitter.ForeColor = System.Drawing.Color.White;
            this.cbTwitter.Location = new System.Drawing.Point(932, 349);
            this.cbTwitter.Name = "cbTwitter";
            this.cbTwitter.Size = new System.Drawing.Size(75, 24);
            this.cbTwitter.TabIndex = 10;
            this.cbTwitter.Text = "Twitter";
            this.cbTwitter.UseVisualStyleBackColor = true;
            // 
            // cbNaive
            // 
            this.cbNaive.AutoSize = true;
            this.cbNaive.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNaive.ForeColor = System.Drawing.Color.White;
            this.cbNaive.Location = new System.Drawing.Point(932, 470);
            this.cbNaive.Name = "cbNaive";
            this.cbNaive.Size = new System.Drawing.Size(119, 24);
            this.cbNaive.TabIndex = 11;
            this.cbNaive.Text = "Naive Bayes";
            this.cbNaive.UseVisualStyleBackColor = true;
            // 
            // cbDictionary
            // 
            this.cbDictionary.AutoSize = true;
            this.cbDictionary.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDictionary.ForeColor = System.Drawing.Color.White;
            this.cbDictionary.Location = new System.Drawing.Point(932, 500);
            this.cbDictionary.Name = "cbDictionary";
            this.cbDictionary.Size = new System.Drawing.Size(112, 24);
            this.cbDictionary.TabIndex = 12;
            this.cbDictionary.Text = "Diccionario";
            this.cbDictionary.UseVisualStyleBackColor = true;
            // 
            // lbTechniques
            // 
            this.lbTechniques.AutoSize = true;
            this.lbTechniques.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTechniques.ForeColor = System.Drawing.SystemColors.Control;
            this.lbTechniques.Location = new System.Drawing.Point(928, 437);
            this.lbTechniques.Name = "lbTechniques";
            this.lbTechniques.Size = new System.Drawing.Size(156, 19);
            this.lbTechniques.TabIndex = 13;
            this.lbTechniques.Text = "Tecnica de análisis";
            // 
            // lbNetwork
            // 
            this.lbNetwork.AutoSize = true;
            this.lbNetwork.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNetwork.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNetwork.Location = new System.Drawing.Point(928, 280);
            this.lbNetwork.Name = "lbNetwork";
            this.lbNetwork.Size = new System.Drawing.Size(190, 19);
            this.lbNetwork.TabIndex = 14;
            this.lbNetwork.Text = "Red social para análisis";
            // 
            // cbBoth
            // 
            this.cbBoth.AutoSize = true;
            this.cbBoth.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBoth.ForeColor = System.Drawing.Color.White;
            this.cbBoth.Location = new System.Drawing.Point(932, 379);
            this.cbBoth.Name = "cbBoth";
            this.cbBoth.Size = new System.Drawing.Size(78, 24);
            this.cbBoth.TabIndex = 15;
            this.cbBoth.Text = "Ambos";
            this.cbBoth.UseVisualStyleBackColor = true;
            // 
            // lbFavorability
            // 
            this.lbFavorability.AutoSize = true;
            this.lbFavorability.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFavorability.ForeColor = System.Drawing.SystemColors.Control;
            this.lbFavorability.Location = new System.Drawing.Point(928, 558);
            this.lbFavorability.Name = "lbFavorability";
            this.lbFavorability.Size = new System.Drawing.Size(64, 19);
            this.lbFavorability.TabIndex = 16;
            this.lbFavorability.Text = "Mostrar";
            // 
            // cbFavorability
            // 
            this.cbFavorability.AutoSize = true;
            this.cbFavorability.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFavorability.ForeColor = System.Drawing.Color.White;
            this.cbFavorability.Location = new System.Drawing.Point(932, 591);
            this.cbFavorability.Name = "cbFavorability";
            this.cbFavorability.Size = new System.Drawing.Size(129, 24);
            this.cbFavorability.TabIndex = 17;
            this.cbFavorability.Text = "Favorabilidad";
            this.cbFavorability.UseVisualStyleBackColor = true;
            // 
            // cbNeutro
            // 
            this.cbNeutro.AutoSize = true;
            this.cbNeutro.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNeutro.ForeColor = System.Drawing.Color.White;
            this.cbNeutro.Location = new System.Drawing.Point(932, 621);
            this.cbNeutro.Name = "cbNeutro";
            this.cbNeutro.Size = new System.Drawing.Size(78, 24);
            this.cbNeutro.TabIndex = 18;
            this.cbNeutro.Text = "Neutro";
            this.cbNeutro.UseVisualStyleBackColor = true;
            // 
            // cbUnfavorable
            // 
            this.cbUnfavorable.AutoSize = true;
            this.cbUnfavorable.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUnfavorable.ForeColor = System.Drawing.Color.White;
            this.cbUnfavorable.Location = new System.Drawing.Point(932, 651);
            this.cbUnfavorable.Name = "cbUnfavorable";
            this.cbUnfavorable.Size = new System.Drawing.Size(154, 24);
            this.cbUnfavorable.TabIndex = 19;
            this.cbUnfavorable.Text = "Desfavorabilidad";
            this.cbUnfavorable.UseVisualStyleBackColor = true;
            // 
            // chListBox
            // 
            this.chListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.chListBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chListBox.ForeColor = System.Drawing.Color.White;
            this.chListBox.FormattingEnabled = true;
            this.chListBox.Location = new System.Drawing.Point(932, 131);
            this.chListBox.Name = "chListBox";
            this.chListBox.Size = new System.Drawing.Size(143, 130);
            this.chListBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(928, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Candidatos";
            // 
            // Visualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chListBox);
            this.Controls.Add(this.cbUnfavorable);
            this.Controls.Add(this.cbNeutro);
            this.Controls.Add(this.cbFavorability);
            this.Controls.Add(this.lbFavorability);
            this.Controls.Add(this.cbBoth);
            this.Controls.Add(this.lbNetwork);
            this.Controls.Add(this.lbTechniques);
            this.Controls.Add(this.cbDictionary);
            this.Controls.Add(this.cbNaive);
            this.Controls.Add(this.cbTwitter);
            this.Controls.Add(this.cbFacebook);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.lbEnd);
            this.Controls.Add(this.lbStart);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.chart1);
            this.Name = "Visualization";
            this.Size = new System.Drawing.Size(1142, 731);
            this.Load += new System.EventHandler(this.Visualization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lbStart;
        private System.Windows.Forms.Label lbEnd;
        private MainFrame main;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.CheckBox cbFacebook;
        private System.Windows.Forms.CheckBox cbTwitter;
        private System.Windows.Forms.CheckBox cbNaive;
        private System.Windows.Forms.CheckBox cbDictionary;
        private System.Windows.Forms.Label lbTechniques;
        private System.Windows.Forms.Label lbNetwork;
        private System.Windows.Forms.CheckBox cbBoth;
        private System.Windows.Forms.Label lbFavorability;
        private System.Windows.Forms.CheckBox cbFavorability;
        private System.Windows.Forms.CheckBox cbNeutro;
        private System.Windows.Forms.CheckBox cbUnfavorable;
        private System.Windows.Forms.CheckedListBox chListBox;
        private System.Windows.Forms.Label label1;
    }
}
