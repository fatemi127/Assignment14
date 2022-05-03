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

namespace sudoku
{
    public partial class Form1 : Form
    {
        TextBox[,] cells;

        public Form1()
        {
            InitializeComponent();
            cells = new TextBox[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new TextBox();
                    cells[i, j].Multiline = true;
                    cells[i, j].TextAlign = HorizontalAlignment.Center;
                    cells[i, j].Font = new Font("Tahoma", 20);
                    //cells[i, j].BackColor = Color.LightCyan;
                    cells[i, j].Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
                    tableLayoutPanel1.Controls.Add(cells[i, j], i, j);
                    cells[i, j].TextChanged += new System.EventHandler(this.cells_TextChanged);

                }
            }



            for (int i = 0; i < 9; i++)
            {
                //   if (i % 2 == 1)

                for (int j = 0; j < 9; j++)
                {
                    if (i < 3 && j < 3 || i > 5 && j > 5
                        || 
                        i < 3 && j > 5 || i > 5 && j < 3
                        ||
                        i > 4 && i < 6 && j > 4 && j < 6
                        ||
                        i > 3 && i < 6 && j >4 && j < 6
                        ||
                        i > 2 && i < 6 && j > 4 && j < 6
                        ||
                        i > 4 && i < 6 && j > 3 && j < 6
                        ||
                        i > 4 && i < 6 && j > 2 && j < 6
                        ||
                        i > 3 && i < 6 && j > 2 && j < 6
                        ||
                        i > 2 && i < 6 && j > 2 && j < 6)
                    {
                        
                        cells[i, j].BackColor = Color.LightSkyBlue;
                    }

                }
                //  }

            }
        }
             

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void cells_TextChanged(object sender, EventArgs e)
        {
            string Spil = this.ActiveControl.Text.ToString();
            if (Spil != "1" && Spil != "2" && Spil != "3" && Spil != "4" && Spil != "5" &&
                Spil != "6" && Spil != "7" && Spil != "8" && Spil != "9" && Spil != string.Empty)
            {
                this.ActiveControl.Text = string.Empty;
                MessageBox.Show("Please Enter Just Number 1~9. This TextBox don't Accept Other Key!" +
                    "\nلطفاً فقط اعداد 1 الی 9 را وارد کنید. این فیلد کلید دیگری را قبول نمی کند ", "Enter InCorrect Key"
                    , MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.ActiveControl.Text = string.Empty;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            reset();
            if (x.ShowDialog() == DialogResult.OK)
            {
                string file_path = x.FileName;
                StreamReader my_file_reader = new StreamReader(file_path);
                string big_text = my_file_reader.ReadToEnd();
                //MessageBox.Show (big_text);
                char[] my_seperators = { ' ', '\n' ,'\r'};
                big_text = big_text.Replace("\n", "");

                string[] numbers = big_text.Split(my_seperators);
                int index = 0;
                for (int i = 0; i < 9; i++)

                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (numbers[index] != "0")
                        {
                            cells[i, j].ReadOnly = true;
                            cells[j,i].Text = numbers[index]; 
                        }
                        index++;
                    }
                    
                }
            }
           
        }
        private void reset()
        {
            for (int i = 0; i < 9; i++)
               
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].ReadOnly = false;
                    cells[i, j].Text = "";

                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_check_Click(object sender, EventArgs e)
        {

        }
    }
}
