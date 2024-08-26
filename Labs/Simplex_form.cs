using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class Simplex_form : Form
    {
        protected DataGridView dataGridView1 = new DataGridView();
        protected DataGridView dataGridView2 = new DataGridView();
        protected Button Calculate = new Button();
        protected Button NextStep = new Button();
        private Button ToDoubleSimplexForm = new Button();
        protected Label message = new Label();
        protected Label result = new Label();
        protected int table_rows_count;
        protected int table_columns_count;
        protected int exes;
        protected double[,] Matrix;
        protected string to_find;

        public Simplex_form(double[,] matrix, string to_find)
        {
            Matrix = matrix;
            InitializeComponent();
            this.to_find = to_find;
        }
        protected void Simplex_form_Load(object sender, EventArgs e)
        {
            table_rows_count = Matrix.GetLength(0)+1;
            table_columns_count = Matrix.GetLength(1)+1;
            exes = table_columns_count - table_rows_count;//количество оригинальных иксов
            createTable(10, 50, table_rows_count, table_columns_count, dataGridView1);
            writeArrayToTable(dataGridView1, Matrix);
            spavnButton(Calculate, dataGridView1.Size.Width + 30, 50, "Вычислить");
            Calculate.Click += Calculate_click;
        }
        protected void Calculate_click(object sender, EventArgs e)
        {
            spavnLabel(message, 10, 10, "Для продолжения нажмите <<Следующий шаг>>");
            spavnButton(NextStep, dataGridView1.Size.Width + 30, 50, "Следующий шаг");
            NextStep.Click += NextStep_click;
            terminateElement(Calculate);//убираем кнопку "вычислить"
            createTable(10, dataGridView1.Size.Height + 50 + 30, dataGridView1.RowCount, dataGridView1.ColumnCount, dataGridView2);//рисуем таблицу
            double[,] matrix = CalculationStep(dataGridView1);//получаем результат первой итерации
            writeArrayToTable(dataGridView2, matrix);//выводим результат первой итерации
            return;
        }
        protected void NextStep_click(object sender, EventArgs e)
        {
            double[,] matrix = CalculationStep(dataGridView2);
            writeArrayToTable(dataGridView2, matrix);
            return;
        }
        private void ToDoubleSimplexForm_click(object sender, EventArgs e)
        {
            Double_simplex_form double_Simplex_Form = new Double_simplex_form(Matrix, to_find);
            double_Simplex_Form.Show(this);
            return;
        }
        protected virtual double[,] CalculationStep(DataGridView Table)
        {
            double[,] matrix = GetArrayFromTable(Table);//получили исходную матрицу
            if (ifHasBasicSolution(matrix))
            {
                //проверяем на оптимальность
                int solution_col = solutionColumnIfNotOptimal(matrix);//разрешающий столбец
                int solution_row = -1;//разрешающая строка
                if (solution_col >= 0)//не оптимально
                {
                    if (isAnyPositive(matrix, solution_col))//решение есть
                    {
                        solution_row = solutionRowRelationsBased(matrix, solution_col);
                        matrix = newBasicSolution(matrix, solution_col, solution_row);
                        dataGridView2.Rows[solution_row + 1].Cells[0].Value = Table.Rows[0].Cells[solution_col + 1].Value;
                    }
                    else
                    {
                        spavnLabel(message, 10, 10, $"Нет решения, в разрешающем столбце {solution_col + 2} нет положительных");
                        terminateElement(NextStep);
                    }//нет решения
                }
                else
                {
                    spavnLabel(message, 10, 10, "Решение оптимально, в оценочной строке Z нет отрицетельных");
                    terminateElement(NextStep);
                    result.Location = new Point(10, dataGridView2.Bottom + 30);
                    result.Size = new Size(500, 1000);
                    result.Text = Interpretation(Table);
                    Controls.Add(result);
                    result.Show();
                }//оптимально
            }
            else
            {
                spavnLabel(message, 10, 10, "Невозможно решить базовым симплекс-методом, т.к. существуют B<0");
                terminateElement(NextStep);
                spavnButton(ToDoubleSimplexForm, dataGridView1.Size.Width + 30, 50, "Перейти к двойственному");
                ToDoubleSimplexForm.Click += ToDoubleSimplexForm_click;
            }
            return matrix;
        }

        //проверяет, решается ли базовым симплекс методом
        private bool ifHasBasicSolution(double[,] matrix)
        {
            int wight = matrix.GetLength(1);//по горизонтали
            int height = matrix.GetLength(0);//по вертикали
            for(int j=0; j < height - 1; j++)
            {
                if (matrix[j, wight - 1] < 0)
                    return false;
            }
            return true;
        }

        //возвращает индекс по горизонтали наименьшего элемента последней строки (т.е. номер искомого столбца),
        //если он отрицателен. иначе возвращает -1.
        protected int solutionColumnIfNotOptimal(double[,] matrix)
        {
            int wight = matrix.GetLength(1);//по горизонтали
            int height = matrix.GetLength(0);//по вертикали
            double minEl = matrix[height - 1, 0];
            int minimum = 0;
            for (int i = 0; i < wight - 1; i++)
            {
                if (matrix[height - 1, i] <= minEl)
                {
                    minEl = matrix[height - 1, i];
                    minimum = i;
                }
            }
            if (minEl >= 0) return -1;//в последней строке все положительные, оптимально
            return minimum; //возвращается индекс последнего меньшего в строке
        }

        //возвращает истину, если в столбце есть положительные. иначе ложь
        protected bool isAnyPositive(double[,] matrix, int column)
        {
            int height = matrix.GetLength(0);//по вертикали
            for (int j = 0; j < height - 1; j++)
            {
                if (matrix[j, column] > 0)
                    return true;
            }
            return false;
        }

        //подсчитывает симплексные отношения для положительных элементов
        //разрешающего столбца, возвращает номер строки, в котором находится 
        //минимальное
        protected int solutionRowRelationsBased(double[,] matrix, int column)
        {
            int wight = matrix.GetLength(1);//по горизонтали
            int height = matrix.GetLength(0);//по вертикали
            int minRelationPos = 0;
            double minRelation = 1000;
            double relation;
            for (int j = 0; j < height - 1; j++)
            {
                if (matrix[j, column] > 0)
                {
                    relation = matrix[j, wight - 1] / matrix[j, column];
                    if (relation < minRelation)
                    {
                        minRelation = relation;
                        minRelationPos = j;
                    }
                }
            }
            return minRelationPos;
        }

        protected double[,] newBasicSolution(double[,] matrix, int solution_col, int solution_row)
        {
            int wight = matrix.GetLength(1);//по горизонтали
            int height = matrix.GetLength(0);//по вертикали
            double center_element = matrix[solution_row, solution_col];//элемент на пересечении разрешающих строки и столбца
            // сохраняем оригинальные значения разрешающего столбца
            double[] originai_solution_col_values = new double[height];
            for (int j = 0; j < height; j++)
                originai_solution_col_values[j] = matrix[j, solution_col];
            //пересчитываем разрешающую строку
            for (int i = 0; i < wight; i++)
                matrix[solution_row, i] = matrix[solution_row, i] / center_element;
            //пересчитываем остальную таблицу

            for (int i = 0; i < wight; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (j != solution_row)
                    {
                        matrix[j, i] += matrix[solution_row, i] * (-originai_solution_col_values[j]);
                    }
                }
            }
            return matrix;
        }
        protected double[] getAnswer(DataGridView Table)
        {
            int lenght = table_columns_count - 2;
            double[] the_answer = new double[lenght];

            for (int i = 0; i < lenght; i++)
            {
                for (int j = 1; j < Table.RowCount - 1; j++)
                {
                    string str = Table.Rows[j].Cells[0].Value.ToString();
                    if (str == "X" + (i + 1))
                        the_answer[i] = Convert.ToDouble(Table.Rows[j].Cells[table_columns_count - 1].Value);
                }
            }
            return the_answer;
        }
        protected string Interpretation(DataGridView Table)
        {
            string res = "Получено оптимальное решение: X = ( ";
            int lenght = table_columns_count - 2;
            double[] the_answer = getAnswer(Table);

            for (int c = 0;c < lenght-1; c++)
            {
                res += $"{the_answer[c]}; ";
            }
            res += $"{the_answer[lenght-1]} ) \nкоторому соответствует Z{ to_find} =";
            if(to_find=="min")
                res+=$" {Convert.ToDouble(Table.Rows[table_rows_count - 1].Cells[table_columns_count - 1].Value)*(-1)}.\n";
            else { res += $" {Table.Rows[table_rows_count - 1].Cells[table_columns_count - 1].Value}.\n"; }
            
            for (int i = 0; i < exes; i++)
            {
                res += $"\n Объём производства продукции X{i+1} должен составлять\n" +
                    $"{the_answer[i]} ед. за исследуемый временной период.\n";
            }
            res += $" \n Доход от реализации продукции составит ";
            if (to_find == "min")
                res += $" {Convert.ToDouble(Table.Rows[table_rows_count - 1].Cells[table_columns_count - 1].Value) * (-1)}.\n";
            else { res += $" {Table.Rows[table_rows_count - 1].Cells[table_columns_count - 1].Value}.\n"; }
            res+=" ден. ед. за исследуемый временной период.";
            return res;
        }

        protected double[,] GetArrayFromTable(DataGridView dataGridView)
        {
            int rows = dataGridView.RowCount;
            int cols = dataGridView.ColumnCount;
            double[,] matrix = new double[rows - 1, cols - 1];
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    matrix[i - 1, j - 1] = Math.Round(Convert.ToDouble(dataGridView.Rows[i].Cells[j].Value),2);
                }
            }
            return matrix;
        }

        //заносит числа из массива(матрицы) в таблицу
        protected void writeArrayToTable(DataGridView dataGridView, double[,] matrix)
        {
            int rows = dataGridView.RowCount;
            int cols = dataGridView.ColumnCount;
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = matrix[i - 1, j - 1];
                }
            }
            return;
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

            //заполняем шапку таблицы(верхнюю строку)
            dataGridView.Rows[0].Cells[0].Value = "Xk(i)";
            for (int i = 1; i < cols - 1; i++)
            {
                dataGridView.Rows[0].Cells[i].Value = "X" + i;
            }
            dataGridView.Rows[0].Cells[cols - 1].Value = "B(i)";

            //заполняем первый столбец
            int x_starts_with = cols - rows;
            for (int i = 1; i < rows - 1; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = "X" + (i + x_starts_with);
            }
            dataGridView.Rows[rows - 1].Cells[0].Value = "Z";

            // Добавляем таблицу на форму
            Controls.Add(dataGridView);
            return;
        }
        //рисует кнопку на форме в заданном координатами месте
        protected void spavnButton(Button button, int x, int y, string message)
        {
            button.Location = new Point(x, y);
            button.Text = message;
            button.AutoSize = true;
            button.BackColor = Color.LightBlue;
            button.Padding = new Padding(6);
            button.Font = new Font("Microsoft Sans Serif", 9);
            Controls.Add(button);
            return;
        }
        protected void spavnLabel(Label label, int x, int y, string message)
        {
            label.Location = new Point(x, y);
            label.Size = new Size(500, 20);
            label.Text = message;
            if (!Controls.Contains(label))
                Controls.Add(label);
            return;
        }
        //удаляет элемент с формы
        protected void terminateElement(Control element)
        {
            if (Controls.Contains(element))
                Controls.Remove(element);
            return;
        }
    }
}
