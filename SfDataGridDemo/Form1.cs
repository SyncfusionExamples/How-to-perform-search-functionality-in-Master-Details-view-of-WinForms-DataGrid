using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SfDataGridDemo
{
    public partial class Form1 : Form
    {
        SfDataGrid detailsViewGrid;
        public Form1()
        {
            InitializeComponent();
            SampleCustomization();
            this.Load += OnLoad;
        }

        /// <summary>
        /// Occurs when the form is loading.
        /// </summary>
        /// <param name="sender">The sender of event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            this.sfDataGrid1.ExpandAllDetailsView();
        }

        /// <summary>
        /// Sets the sample customization settings.
        /// </summary>
        private void SampleCustomization()
        {           
            this.sfDataGrid1.AutoGenerateRelations = true;           
            this.sfDataGrid1.AutoGeneratingRelations += OnAutoGeneratingRelations;
            var parentTable = GetParentDataTable();
            var childTable = GetChildDataTable();
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(parentTable);
            dataSet.Tables.Add(childTable);
            dataSet.Relations.Add(new DataRelation("Parent_Child", dataSet.Tables[0].Columns["ID"], dataSet.Tables[1].Columns["ID"]));
            this.sfDataGrid1.DataSource = dataSet.Tables[0];            
        }

        private void OnAutoGeneratingRelations(object sender, AutoGeneratingRelationsEventArgs e)
        {
            // To get the DeatilsView dataGrid
            detailsViewGrid = e.GridViewDefinition.DataGrid;
        }
        
        DataTable GetParentDataTable()
        {
            DataTable collection = new DataTable();
            collection.Columns.Add("ID", typeof(int));
            collection.Columns.Add("Name", typeof(string));
            collection.Columns.Add("Q1", typeof(float));
            collection.Columns.Add("Q2", typeof(float));
            collection.Columns.Add("Q3", typeof(float));
            collection.Rows.Add(1001, "Belgim", 872.81, 978.89, 685.90);
            collection.Rows.Add(1002, "Oliver", 978.76, 458.21, 675.99);
            collection.Rows.Add(1003, "Bernald", 548.31, 234.32, 423.44);
            collection.Rows.Add(1004, "James", 123.31, 6543.12, 978.31);

            return collection;
        }

        DataTable GetChildDataTable()
        {
            DataTable collection = new DataTable();
            collection.Columns.Add("ID", typeof(int));
            collection.Columns.Add("Name", typeof(string));
            collection.Columns.Add("City", typeof(string));
            collection.Columns.Add("Quantity", typeof(int));
            collection.Columns.Add("Unit Price", typeof(int));
            collection.Rows.Add(1001, "Belgim", "California", 10, 50);
            collection.Rows.Add(1001, "Belgim", "Colorado", 20, 35);
            collection.Rows.Add(1001, "Belgim", "Alaska", 50, 65);
            collection.Rows.Add(1001, "Belgim", "Roraima", 10, 40);

            collection.Rows.Add(1002, "Oliver", "California", 32, 40);
            collection.Rows.Add(1002, "Oliver", "Alaska", 24, 35);
            collection.Rows.Add(1002, "Oliver", "Roraima", 98, 50);
            collection.Rows.Add(1002, "Oliver", "Colorado", 78, 65);

            collection.Rows.Add(1003, "Bernald", "California", 89, 35);
            collection.Rows.Add(1003, "Bernald", "Alaska", 10, 65);
            collection.Rows.Add(1003, "Bernald", "Colorado", 20, 50);
            collection.Rows.Add(1003, "Bernald", "Roraima", 30, 40);

            collection.Rows.Add(1004, "James", "Colorado", 22, 50);
            collection.Rows.Add(1004, "James", "Roraima", 53, 40);
            collection.Rows.Add(1004, "James", "California", 65, 65);
            collection.Rows.Add(1004, "James", "Alaska", 25, 35);

            return collection;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            // To perform search in DetailsView DataGrid
            detailsViewGrid.SearchController.Search(this.textBox1.Text);
        }

        private void previousButtonClick(object sender, EventArgs e)
        {
            // To navigate the previous cell match with search text
            if (detailsViewGrid.SearchController.FindPrevious(this.textBox1.Text))
            {
                var detailsViewGrid = sfDataGrid1.GetDetailsViewGrid(this.sfDataGrid1.SearchController.CurrentRowColumnIndex.RowIndex);
                detailsViewGrid.MoveToCurrentCell(detailsViewGrid.SearchController.CurrentRowColumnIndex);
            }            
        }

        private void nextButtonClick(object sender, EventArgs e)
        {
            // To navigate the next cell match with search text
            if (detailsViewGrid.SearchController.FindNext(this.textBox1.Text))
            {
                var detailsViewGrid = sfDataGrid1.GetDetailsViewGrid(this.sfDataGrid1.SearchController.CurrentRowColumnIndex.RowIndex);
                detailsViewGrid.MoveToCurrentCell(detailsViewGrid.SearchController.CurrentRowColumnIndex);
            }
        }
    }
}
