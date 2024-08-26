using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class Simplex_start : Form
    {
        protected DataGridView Table_exes = new DataGridView();
        protected DataGridView Table_z = new DataGridView();
        protected int table_rows_count;
        protected int table_columns_count;
        protected string to_find = "max";
        protected string mode;

        public Simplex_start(string mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void Simplex_start_Load(object sender, EventArgs e)
        {
            label_if_empty.Visible = false;
            starting_button.Enabled = false;
            maxButton.Checked = true;
            maxButton.Visible = false;
            minButton.Visible = false;
        }
        protected void Simplex_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Блокируем кнопку при закрытии SecondaryForm
            table_button.Enabled = true;
            starting_button.Enabled = true;
        }
        protected virtual void table_button_Click(object sender, EventArgs e)
        {
            starting_button.Enabled = true;
            table_rows_count = Convert.ToInt32(inequalities_q.Value) + 1;//количество строк
            table_columns_count = Convert.ToInt32(Exes_q.Value)+2;//+ колонка B + знак
            createTable(30, 122, table_rows_count, table_columns_count, Table_exes);
            //заполняем шапку таблицы(верхнюю строку)
            for (int i = 0; i < table_columns_count - 2; i++)
            {
                Table_exes.Rows[0].Cells[i].Value = "X" + (i+1);
            }
            Table_exes.Rows[0].Cells[table_columns_count - 1].Value = "B";
            Table_exes.Rows[0].Cells[table_columns_count - 2].Value = "знак";
            //заполняем знаки
            for (int j = 1; j < table_rows_count; j++)
            {
                Table_exes.Rows[j].Cells[table_columns_count - 2].Value = "<=";
            }
            createTable(30, Table_exes.Bottom + 20, 2, table_columns_count-1, Table_z);

            //заполняем шапку таблицы(верхнюю строку)
            for (int i = 0; i < table_columns_count - 2; i++)
            {
                Table_z.Rows[0].Cells[i].Value = "X" + (i+1);
            }
            Table_z.Rows[0].Cells[table_columns_count - 2].Value = "C";

            minButton.Visible = true;
            maxButton.Visible = true;
        }
        protected void createTable(int x, int y, int rows, int cols, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.Location = new Point(x, y); //местоположение таблицы
            dataGridView.Size = new Size(500, 22 * rows); //размер фона
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;

            for (int i = 0; i < cols; i++)
            {
                dataGridView.Columns.Add("Column" + (i + 1), "Column " + (i + 1));
            }
            for (int i = 0; i < rows; i++)
            {
                dataGridView.Rows.Add();
            }

            // Добавляем таблицу на форму
            Controls.Add(dataGridView);
        }

        protected virtual void starting_button_Click(object sender, EventArgs e)/////try convert to double
        {
            bool is_empty_field = false;
            
            for (int i = 0; i < table_columns_count - 1; i++)
            {
                if (Table_z.Rows[1].Cells[i].Value != null)
                {
                    try { double smth = Convert.ToDouble(Table_z.Rows[1].Cells[i].Value); }
                    catch (FormatException)
                    {
                        is_empty_field = true;
                        break;
                    }
                }
                else { is_empty_field = true; }
                
            }
            if (!is_empty_field)
            {
                for (int i = 0; i < table_columns_count; i++)
                {
                    for (int j = 1; j < table_rows_count; j++)
                    {
                        if(i!= table_columns_count - 2)
                        {
                            if (Table_exes.Rows[j].Cells[i].Value != null)
                            {
                                try { double smth = Convert.ToDouble(Table_exes.Rows[j].Cells[i].Value); }
                                catch (FormatException)
                                {
                                    is_empty_field = true;
                                    break;
                                }
                            }
                            else { is_empty_field = true; }
                        }
                    }
                }
            }
            if (!is_empty_field)
            {
                for (int j = 1; j < table_rows_count; j++)
                {
                    string str = Table_exes.Rows[j].Cells[table_columns_count - 2].Value.ToString();
                    if (str!= "<=" && str != ">=")
                    {
                        is_empty_field = true;
                        break;
                    }
                }
            }

            if (!is_empty_field)
            {
                label_if_empty.Visible = false;
                table_button.Enabled = false;
                starting_button.Enabled = false;
                if (mode == "ordinary")
                {
                    Simplex_form simplexForm = new Simplex_form(getMatrix(), to_find);
                    simplexForm.FormClosed += Simplex_form_FormClosed;
                    simplexForm.Show(this);
                }
                if(mode == "double")
                {
                    Double_simplex_form double_Simplex_Form = new Double_simplex_form(getMatrix(), to_find);
                    double_Simplex_Form.FormClosed += Simplex_form_FormClosed;
                    double_Simplex_Form.Show(this);
                }
                if(mode == "ghomori")
                {
                    Ghomori_form ghomori_Form = new Ghomori_form(getMatrix(), to_find);
                    ghomori_Form.FormClosed += Simplex_form_FormClosed;
                    ghomori_Form.Show(this);
                }
                
            }
            else { label_if_empty.Visible = true; }
        }

        protected virtual double[,] getMatrix()
        {
            int rows = table_rows_count;//количество строк в матрице/количество уравнений
            int columns = rows + table_columns_count - 2;//количество столбиков в матрице/основные иксы + доп.
            double[,] matrix = new double[rows, columns];
            //если в В есть отрицательные
            for(int  i = 1; i < table_rows_count; i++)
            {
                if (Convert.ToDouble(Table_exes.Rows[i].Cells[table_columns_count - 1].Value) < 0)
                {
                    if (Table_exes.Rows[i].Cells[table_columns_count - 2].Value.ToString() == "<=")
                        Table_exes.Rows[i].Cells[table_columns_count - 2].Value = ">=";
                    else
                        Table_exes.Rows[i].Cells[table_columns_count - 2].Value = "<=";
                    for(int j=0; j < table_columns_count; j++)
                    {
                        if (j != table_columns_count - 2)
                            Table_exes.Rows[i].Cells[j].Value = Convert.ToDouble(Table_exes.Rows[i].Cells[j].Value) * (-1);
                    }
                }
            }
            //заполним строку Z
            int border = columns - rows + 1;
            for(int i =0; i<border; i++)
                matrix[rows - 1, i] = Convert.ToDouble(Table_z.Rows[1].Cells[i].Value)*(-1);
            //заполним основные x
            border--;
            for (int i = 0; i < border; i++)
            {
                for (int j =0; j < rows-1; j++)
                    matrix[j, i] = Convert.ToDouble(Table_exes.Rows[j+1].Cells[i].Value);
            }
            //заполним доп. иксы
            //border;
            int one = 0;//строка, куда ставить единицу
            for(int i = border; i < columns-1; i++)
            {
                if (Table_exes.Rows[one + 1].Cells[table_columns_count - 2].Value.ToString() == "<=")
                    matrix[one, i] = 1;
                else
                    matrix[one, i] = -1;
                one++;
            }
            //заполним b
            for (int j = 0; j < rows - 1; j++)
                matrix[j, columns - 1] = Convert.ToDouble(Table_exes.Rows[j+1].Cells[table_columns_count - 1].Value);
            //приведение к каноническому виду
            for (int j = 1; j < table_rows_count; j++)
            {
                if (Table_exes.Rows[j].Cells[table_columns_count - 2].Value.ToString() == ">=")
                {
                    for(int i = 0; i<columns; i++)
                        matrix[j - 1, i] = matrix[j - 1, i] * (-1);
                }
            }
            
            if (minButton.Checked == true)//если ищем минимум
            {
                to_find = "min";
                for (int i = 0; i < columns; i++)
                    matrix[rows - 1, i] = matrix[rows - 1, i] *(- 1);
            }
            else { to_find = "max"; }

            return matrix;
        }
            
    }
}
