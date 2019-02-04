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

namespace TextToBinaryConsoleAppp
{
    public partial class Form1 : Form
    {
        private float gridSize = 1;
        private int downSampleRatio = 10;
        private string PointCloudPath;
        private string SavePath;
        private float Colorcoef;
        private bool isImported = false;
        ScanArea Area;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Point Cloud files |*.txt;*.off;*.xyz;|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PointCloudPath = openFileDialog.FileName;
                Instruction.Text = "Please Press DownSample button";
            }
        }

        private void DSRatioTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(DSRatioTextbox.Text);
                downSampleRatio = int.Parse(DSRatioTextbox.Text);
            }
            catch (FormatException)
            {
                downSampleRatio = 10;
                if(DSRatioTextbox.Text != "")
                    MessageBox.Show("Please enter an integer");
            }
            catch(OverflowException)
            {
                downSampleRatio = 10;
            }
        }

        private void GridSizeTextbox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine(GridSizeTextbox.Text);
                gridSize = float.Parse(GridSizeTextbox.Text);
                
            }
            catch (FormatException)
            {
                gridSize = 1;
                if(GridSizeTextbox.Text != "")
                    MessageBox.Show("Please enter a float");
            }
            catch (OverflowException)
            {
                gridSize = 1;
            }
           
            
           
        }

        private void DownSample_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!this.backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
                Instruction.Text = "Downsampling Point Cloud, please wait";
                this.DownSample.Enabled = false;
                this.SaveButton.Enabled = false;
                this.Open.Enabled = false;
            }
            Console.WriteLine(PointCloudPath);
            Console.WriteLine(downSampleRatio);
            Console.WriteLine(gridSize);


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (File.Exists(PointCloudPath))
            {
                // Reference Area with given Grid Size and Down Sample Ratio
                Area = new ScanArea(gridSize, downSampleRatio);
                var fs = new FileStream(PointCloudPath, FileMode.Open, FileAccess.Read);
                using (var bs = new BufferedStream(fs))
                using (var r = new StreamReader(bs))
                {
                    string s;

                    while ((s = r.ReadLine()) != null)
                    {

                        var split = s.Split();
                        //Add points to Area by converting from mathematical right hand coordinate system to Unity left hand coordinate system
                        Area.AddPoint(new Vector3(float.Parse(split[1]), float.Parse(split[2]), -float.Parse(split[0])));

                    }
                    // Assign points to cells based on cell index
                    Area.AssignPointsToCells();

                    int meshCount = 0;
                    for (int i = 0; i < Area.Resolution; i++)
                    {
                        if (Area.cells[i].cellPoints.Count != 0)
                            meshCount++;
                    }

                    //Expected color coefficient in a uniformly distributed point cloud for each cell
                    Colorcoef = Area.Points.Count / meshCount;
                    isImported = true;
                }
            }
            else
            {
                MessageBox.Show("Please Select a valid point cloud");
            }

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Cursor = Cursors.Default;
            if (isImported)
            {
                Instruction.Text = " Point Cloud is down Sampled, Please save the Point Cloud";
                MessageBox.Show("Done!");
            }
            else
                Instruction.Text = "Please Select the point cloud";
            this.SaveButton.Enabled = true;
            this.DownSample.Enabled = true;
            this.Open.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //Save Point Cloud as PC.bin to selected folder 
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            if(fbdlg.ShowDialog()==DialogResult.OK)
            {
                SavePath = fbdlg.SelectedPath;
                Console.WriteLine(SavePath);
            }
            Cursor = Cursors.WaitCursor;
            Instruction.Text = " Saving Point Cloud";
            if (Area != null)
            {
                using (var Binaryfs = new FileStream(SavePath+"/PC.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                using (var bw = new BinaryWriter(Binaryfs))
                {
                    for (int i = 0; i < Area.Resolution; i++)
                    {
                        if (Area.cells[i].cellPoints.Count != 0)
                        {
                            Area.cells[i].CalcDownSampledPoints();

                            //first write number of points in each cell and Color coefficient of each cell  as binary
                            bw.Write(Area.cells[i].cellPointsDownSampled.Count);
                            bw.Write((Area.cells[i].cellPoints.Count / Colorcoef));
                            
                            //Then write coordinates of each points from down sampled Point Cloud
                            for (int j = 0; j < Area.cells[i].cellPointsDownSampled.Count; j++)
                            {
                                bw.Write(Area.cells[i].cellPointsDownSampled[j].x);
                                bw.Write(Area.cells[i].cellPointsDownSampled[j].y);
                                bw.Write(Area.cells[i].cellPointsDownSampled[j].z);                         
                            }
                        }
                    }

                }
            }
            
            Cursor = Cursors.Default;
            Instruction.Text = "Point Cloud Saved";
            MessageBox.Show("Saved");
        }
        
    }
}
