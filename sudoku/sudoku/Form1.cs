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
                MessageBox.Show("Please Enter Just Number 1~9. This TextBox don't Accept Other Key!\n" +
                    "\nلطفاً فقط اعداد ۱ الی ۹ را وارد کنید. این فیلد کلید دیگری را قبول نمی‌کند ", "Enter InCorrect Key"
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
            int[] row, col, square;
            int index;
            for (int i = 0; i < 9; i++)
            {
                row = new int[9];
                col = new int[9];

                for (int j = 0; j < 9; j++)
                {
                    if (cells[j, i].Text != "" && cells[i, j].Text != "")
                    {
                        int rowCell = Convert.ToInt16(cells[j, i].Text);
                        int colCell = Convert.ToInt16(cells[i, j].Text);

                        if (0 < rowCell && rowCell < 10 && !row.Contains(rowCell) &&
                            0 < colCell && colCell < 10 && !col.Contains(colCell))
                        {
                            row[j] = rowCell;
                            col[j] = colCell;
                            continue;
                        }
                    }

                    MessageBox.Show("Try More...");
                    return;
                }
            }
            
            for (int h = 1, s1 = 0, e1 = 3, s2 = 0, e2 = 3; h <= 9; h++, s2 += 3, e2 += 3)
            {
                if ((h - 1) % 3 == 0)
                {
                    s2 = 0;
                    e2 = 3;
                }

                square = new int[9];
                index = 0;

                for (int i = s1; i < e1; i++)
                {
                    for (int j = s2; j < e2; j++)
                    {
                        int cell = Convert.ToInt16(cells[j, i].Text);

                        if (!square.Contains(cell))
                        {
                            square[index++] = cell;
                            continue;
                        }

                        MessageBox.Show("Try More...");
                        return;
                    }
                }

                if (h % 3 == 0)
                {
                    s1 += 3;
                    e1 += 3;
                }
            }
        }
    }
}
