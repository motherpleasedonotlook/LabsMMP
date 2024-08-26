using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class Double_simplex_form : Simplex_form
    {
        public Double_simplex_form(double[,] matrix, string to_find):base(matrix, to_find)
        {
            Text = "Двойственный симплекс-метод";
        }

        private void Double_simplex_form_Load(object sender, EventArgs e)
        {
            table_rows_count = Matrix.GetLength(0) + 1;
            table_columns_count = Matrix.GetLength(1) + 1;
            exes = table_columns_count - table_rows_count;//количество оригинальных иксов
            createTable(10, 50, table_rows_count, table_columns_count, dataGridView1);
            writeArrayToTable(dataGridView1, Matrix);
            spavnButton(Calculate, dataGridView1.Size.Width + 30, 50, "Вычислить");
            Calculate.Click += Calculate_click;
        }

        
        override protected double[,] CalculationStep(DataGridView Table)
        {
            double[,] matrix = GetArrayFromTable(Table);//получили исходную матрицу
            int solution_col;//разрешающий столбец
            int solution_row;//разрешающая строка
            //проверяем, решается ли двойственным
            if(d_solutionRowIfNotOptimal(matrix) == -2)
            {
                spavnLabel(message, 10, 10, "Задача неразрешима в силу несовместности системы ограничений");
                terminateElement(NextStep);
            }
            else if (d_solutionRowIfNotOptimal(matrix) != -1)
            {
                spavnLabel(message, 10, 10, "Решается двойственным симплекс методом, т.к. cуществует B<0");
                solution_row = d_solutionRowIfNotOptimal(matrix);//получаем разрешающую строку
                solution_col = d_solutionColumnRelationsBased(matrix, solution_row);//получаем разрешающий столбец
                matrix = newBasicSolution(matrix, solution_col, solution_row);
                dataGridView2.Rows[solution_row + 1].Cells[0].Value = Table.Rows[0].Cells[solution_col + 1].Value;
            }
            else
            {
                spavnLabel(message, 10, 10, "Решается базовым симплекс методом, т.к. не cуществует B<0");
                //проверяем на оптимальность
                solution_col = solutionColumnIfNotOptimal(matrix);//разрешающий столбец
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
                    spavnLabel(message, 10, 10, "Решение оптимально, в оценочной строке Z и столбце свободных членов B нет отрицетельных");
                    terminateElement(NextStep);
                    result.Location = new Point(10, dataGridView2.Bottom + 30);
                    result.Size = new Size(500, 1000);
                    result.Text = Interpretation(Table);
                    Controls.Add(result);
                    result.Show();
                }//оптимально
            }
            return matrix;
        }

        ////////////////////////////////касается двойственного////////////////////////////////////

        //возвращает индекс разрешающей строки, если есть B(j)<0 и при них есть хоть один X(i)<0. Если нет X(i)<0 -> -2; Иначе -1.
        protected int d_solutionRowIfNotOptimal(double[,] matrix)
        {
            int height = matrix.GetLength(0);//по вертикали
            int wight = matrix.GetLength(1);//по горизонтали
            double min_el = 0;
            int min_el_index = -1;
            for (int j = 0; j<height-1;j++)
            {
                if (matrix[j, wight - 1] < 0)
                {
                    bool is_compatible = false;
                    for(int i = 0; i<wight-1; i++)
                    {
                        if (matrix[j, i] < 0)
                        {
                            is_compatible = true;
                            break;
                        }
                    }
                    if (is_compatible)
                    {
                        if (matrix[j, wight - 1] < min_el)
                        {
                            min_el = matrix[j, wight - 1];
                            min_el_index = j;
                        }
                    }
                    else { return -2; }
                }
                
            }
            return min_el_index;
        }
        //возвращает индекс разрешающего столбца, если при B(i)<0 есть X(i)<0.
        protected int d_solutionColumnRelationsBased(double[,] matrix, int row)
        {
            int height = matrix.GetLength(0);//по вертикали
            int wight = matrix.GetLength(1);//по горизонтали
            int min_relation_index = -1;
            double min_relation = 1000;
            for (int i = 0; i<wight-1; i++)
            {
                if (matrix[row, i] < 0)
                {
                    double relation = matrix[height-1,i];
                    if(relation < min_relation)
                    {
                        min_relation = relation;
                        min_relation_index = i;
                    }
                }
            }
            return min_relation_index;
        }

        
    }
}
