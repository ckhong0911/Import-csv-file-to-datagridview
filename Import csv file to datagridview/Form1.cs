using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Import_csv_file_to_datagridview
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      #region member setting
      OpenFileDialog openFile = new OpenFileDialog
      {
        Filter = "(Comma split)|*.csv;*.txt",
        Title = "Please select file",
        FileName = "",
        FilterIndex = 1,
        Multiselect = true, // multiple choice
      };
      #endregion

      if (openFile.ShowDialog() == DialogResult.OK)
      {
        // reset datagridview
        dgv.DataSource = null;

        #region create a data table and defind columns name
        DataTable myTable = new DataTable();
        string[] colNames = { "SetNo", "CompName", "Comment", "FdrType", "PitchIndex", "No", "Replace", "Modle Name", "", "", "Pattern Name", "Total" };

        foreach (string col in colNames)
        {
          myTable.Columns.Add(col);
        }
        #endregion


        foreach (string file in openFile.FileNames)
        {
          try
          {
            if (File.Exists(file) == true)
            {
              #region import file data
              StreamReader csvReader = new StreamReader(file);
              string textLine;      // string line data
              string[] splitLine;   // use array to save split data

              do
              {
                textLine = csvReader.ReadLine();
                splitLine = textLine.Split(',');

                myTable.Rows.Add(splitLine);
              }
              while (csvReader.Peek() != -1);

              csvReader.Close(); 
              csvReader.Dispose();
              #endregion
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message);
          }
        }

        // display data on datagridview
        dgv.DataSource = myTable;
      }

      // Thanks for watching !!
      // Hope you like !!
    }
  }
}
