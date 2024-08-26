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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Labs
{
    public partial class Ghomori_form : Double_simplex_form
    {
        public Ghomori_form(double[,] matrix, string to_find) : base(matrix, to_find)
        {
            Text = "Метод Гомори";
        }

        private void Ghomori_form_Load(object sender, EventArgs e)
        {
            table_rows_count = Matrix.GetLength(0) + 1;
            table_columns_count = Matrix.GetLength(1) + 1;
            exes = table_columns_count - table_rows_count;//количество оригинальных иксов
            createTable(10, 50, table_rows_count, table_columns_count, dataGridView1);
            writeArrayToTable(dataGridView1, Matrix);
            spavnButton(Calculate, dataGridView1.Size.Width + 30, 50, "Вычислить");
            Calculate.Click += Calculate_click;
        }
        
        private void reachTheInteger(object sender, EventArgs e)
        {
            //функция, добавляющая новое уравнение, изменяет количество строк в матрице
            double[,] matrix = addTheExtraEquation(dataGridView2);
            //в cteateTable количество строк поменять в соответствии с новым размером матрицы
            addTableRow(dataGridView2);
            writeArrayToTable(dataGridView2, matrix);
            NextStep.Click-=reachTheInteger;
            NextStep.Click += NextStep_click;
            return;
        }
        override protected double[,] CalculationStep(DataGridView Table)
        {
            double[,] matrix = GetArrayFromTable(Table);//получили исходную матрицу
            int solution_col;//разрешающий столбец
            int solution_row;//разрешающая строка
            //проверяем, решается ли двойственным
            if (d_solutionRowIfNotOptimal(matrix) == -2)
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
                    
                    if (isIntegerSolutionReached())//проверка на целостность решения
                    {
                        spavnLabel(message, 10, 10, "Решение оптимально, в оценочной строке Z и столбце свободных членов B нет отрицетельных");
                        terminateElement(NextStep);
                        result.Location = new Point(10, dataGridView2.Bottom + 30);
                        result.Size = new Size(500, 1000);
                        result.Text = Interpretation(Table);
                        Controls.Add(result);
                        result.Show();
                    }
                    else
                    {
                        spavnLabel(message, 10, 10, "Оптимальное решение нецелочисленно, добавим еще одно уравнение");
                        NextStep.Click -= NextStep_click;
                        NextStep.Click += reachTheInteger;
                        
                    }
                    
                }//оптимально
            }
            return matrix;
        }
        protected bool isIntegerSolutionReached()
        {
            int lenght = table_columns_count - 2;
            double[] the_answer = getAnswer(dataGridView2);
            for (int i = 0; i < lenght; i++)
            {
                if (the_answer[i] - (int)the_answer[i]!=0)
                    return false;
            } 
            return true;
        }
        protected double[,] addTheExtraEquation(DataGridView Table)
        {
            
            double[,] matrix = GetArrayFromTable(Table);
            int rows1 = matrix.GetLength(0);
            int cols1 = matrix.GetLength(1);
            double[] new_equation = new double[cols1];
            double max_fractional_part = 0;
            int max_fractional_part_index = -1;
            for (int i = 1; i<Table.RowCount-1; i++)
            {
                for(int j = 1; j<table_columns_count-table_rows_count+2; j++)
                {
                    if (Table.Rows[i].Cells[0].Value.ToString() == $"X{j}")
                    {
                        double fractional_part = Convert.ToDouble(Table.Rows[i].Cells[table_columns_count-1].Value) - (int)Convert.ToDouble(Table.Rows[i].Cells[table_columns_count - 1].Value);
                        if (fractional_part > max_fractional_part)
                        {
                            max_fractional_part = fractional_part;
                            max_fractional_part_index = i - 1;
                        }  
                    }
                }                
            }
            for(int j=0; j< matrix.GetLength(1); j++)
            {
                new_equation[j] = matrix[max_fractional_part_index, j]-(int)matrix[max_fractional_part_index, j];
            }
            double[,] new_matrix = new double[rows1 + 1, cols1];
            // Копируем все строки из matrix, кроме последней
            for (int i = 0; i < rows1 - 1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    new_matrix[i, j] = matrix[i, j];
                }
            }

            // Добавляем new_equation как следующую строку
            for (int j = 0; j < cols1; j++)
            {
                new_matrix[rows1 - 1, j] = new_equation[j]*(-1);
            }

            // Добавляем последнюю строку из matrix
            for (int j = 0; j < cols1; j++)
            {
                new_matrix[rows1, j] = matrix[rows1 - 1, j];
            }
            return new_matrix;
        }

        protected void addTableRow(DataGridView Table)
        {
            table_rows_count++;
            Table.Rows.Add();
            Table.Size = new Size(500, 22 * table_rows_count); //размер фона
            int x = table_columns_count - 1;
            Table.Rows[table_rows_count-2].Cells[0].Value = "X"+x;
            Table.Rows[table_rows_count-1].Cells[0].Value = "Z";
            return;
        }
    }
}
