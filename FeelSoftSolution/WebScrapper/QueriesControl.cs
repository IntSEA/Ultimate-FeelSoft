﻿using SocialNetworkConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WebScrapper
{
    public partial class QueriesControl : UserControl
    {
        public QueriesControl() : base()
        {
            InitializeComponent();
            InitializeDataContainers();
            InitializeQueryConfigurations();

        }

        private void InitializeQueryConfigurations()
        {
            string fajardoConfig = "..//..//..//SocialNetworkConnection/Resources/SocialNetworks/Fajardo/Configurations/FajardoConfig.qnc";
            string petroConfig = "..//..//..//SocialNetworkConnection/Resources/SocialNetworks/Petro/Configurations/PetroConfig.qnc";
            AddConfig(fajardoConfig);
            AddConfig(petroConfig);
        }

        private void AddConfig(string path)
        {
            StreamReader sr = new StreamReader(path);
            string queryFormat = sr.ReadToEnd();
            sr.Close();
            IQueryConfiguration queryConfig = new QueryConfiguration(queryFormat);
            AddQueryConfigurations(queryConfig);
        }

        public void SetMain(WebScrapperViewer main)
        {
            this.main = main;
        }

        private void InitializeDataContainers()
        {
            configurations = new List<IQueryConfiguration>();
        }



        private void BtnInitSearchClick(object sender, EventArgs e)
        {
            MakeQueryRequest();
        }

        internal List<IQueryConfiguration> GetConfigurations()
        {
            return configurations;
        }

        private void MakeQueryRequest()
        {

            main.Search(configurations);
        }


        private void BtnCreateQuery_Click(object sender, EventArgs e)
        {

            queryForm = new QueryConfigurationForm();
            DialogResult result = queryForm.ShowDialog();

            if (DialogResult.OK == result)
            {
                currentConfiguration = queryForm.GetQueryConfiguration();
                int index = configurations.FindLastIndex(x => x.Name.Equals(currentConfiguration));
                IQueryConfiguration cloneConfiguration = (IQueryConfiguration)currentConfiguration.Clone();

                configurations.Add(cloneConfiguration);


                cbxQueries.Items.Add(cloneConfiguration);

                cbxQueries.SelectedItem = cloneConfiguration;
            }

        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            RemoveQueryConfiguration();

        }

        private void RemoveQueryConfiguration()
        {
            IQueryConfiguration removedObject = (IQueryConfiguration)cbxQueries.SelectedItem;
            if (removedObject != null)
            {
                cbxQueries.Items.Remove(removedObject);
                int index = configurations.FindLastIndex(x => x.Name.Equals(removedObject.Name));
                configurations.RemoveAt(index);
                MessageBox.Show(removedObject.ToString() + " was removed");
            }
            else
            {
                MessageBox.Show("Seleccione un elemento valido");
            }
        }

        private void CbxQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxQueries.SelectedItem != null)
            {
                currentConfiguration = (IQueryConfiguration)cbxQueries.SelectedItem;
            }
        }

        private void BtnImportQueryConfigurationClick(object sender, EventArgs e)
        {
            main.ImportQueryConfiguration();

        }

        public void AddQueryConfigurations(IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration != null)
            {
                IQueryConfiguration cloneQueryConfiguration = (IQueryConfiguration)queryConfiguration.Clone();
                cbxQueries.Items.Add(cloneQueryConfiguration);

                configurations.Add(cloneQueryConfiguration);

                cbxQueries.Text = queryConfiguration.Name;
            }
            else
            {
                cbxQueries.Text = "";
            }
        }

        private void BtnExportQueryConfigurationClick(object sender, EventArgs e)
        {
            if (currentConfiguration == null)
            {
                MessageBox.Show("Selecione una configuración de busqueda");
            }
            else
            {
                main.ExportQueryConfiguration(currentConfiguration);
            }
        }

        internal IQueryConfiguration GetCurrentQueryConfiguration()
        {
            if (currentConfiguration == null)
            {
                BtnCreateQuery_Click(btnCreateQuery, null);

            }
            return currentConfiguration;

        }

        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentConfiguration != null)
            {
                
                queryForm = new QueryConfigurationForm();
                queryForm.SetQueryConfiguration(currentConfiguration);
                DialogResult result = queryForm.ShowDialog();


                if (DialogResult.OK == result)
                {
                    currentConfiguration = queryForm.GetQueryConfiguration();
                   
                    int index = configurations.FindIndex(x => x != null && x.Name.Equals(currentConfiguration.Name, StringComparison.OrdinalIgnoreCase));
                    if (index != -1)
                    {
                        configurations.RemoveAt(index);
                        configurations.Insert(index, currentConfiguration);
                    }
                    else
                    {
                        configurations.Add(currentConfiguration);
                    }

                    cbxQueries.Items.Clear();
                    cbxQueries.Items.AddRange(configurations.ToArray());
                }

            }
            else
            {
                MessageBox.Show("Primero seleccione una configuración valida");
            }
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentConfiguration != null)
            {
                cbxQueries.Items.Remove(currentConfiguration);
                cbxQueries.Text = "";
                int index = configurations.FindLastIndex(x => x != null && x.Name.Equals(currentConfiguration));
                if (index != -1)
                {
                    configurations.RemoveAt(index);
                    configurations.Insert(index, currentConfiguration);
                }
            }
            else
            {
                MessageBox.Show("Primero seleccione una configuración valida");
            }
        }
    }
}
