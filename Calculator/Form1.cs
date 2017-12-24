using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
       
            /// <summary>
            /// Заголовок сообщений об ошибках
            /// </summary>
            private const string title = "Калькулятор";

            /// <summary>
            /// Первый аргумент
            /// </summary>
            private double _x;
        

            /// <summary>
            /// Первый аргумент + индикатор
            /// </summary>
            double x
            {
                get
                {
                    return _x;
                }
                set
                {
                    // Сохранение значения
                    _x = value;
                    // Обновление индикатора на экране
                    indicator.Text = _x.ToString();
                }
            }

        /// <summary>
        /// Второй аргумент 
        /// </summary>
        double y;
        /// <summary>
        /// Память
        /// </summary>
        double z;

        int k = 10;
             
            /// <summary>
            /// Признак ввода нового числа
            /// </summary>
            bool newNumber = true;

            /// <summary>
            /// нажата точка
            /// </summary>
            bool cpoint = false;
            bool res = false;
            Operation operation;
            /// <summary>
            /// Конструктор по умолчанию, без параметров
            /// </summary>
           
            /// <summary>
            /// обработчик нажатия на цифровую клавиатуру
            /// </summary>
            /// <param name="sender">цифровая клавиша</param>
            /// <param name="e">параметры события</param>
            private void buttonDigit_Click(object sender, EventArgs e)
            {
                try
                {
                    //явное приведение к заданному типу
                    Button b = (Button)sender;
                    string tag = (string)b.Tag;
                    int digit = int.Parse(tag);
                    // Изменение значения аккумулятор
                    if (newNumber)
                    {
                        cpoint = false;
                        // Начало ввода нового числа
                        x = digit;
                        newNumber = false;
                    }
                    else
                    {
                    // Продолжение ввода старого числа
                    if (!cpoint)
                         {
                            x = 10 * x + digit;
                         }
                    else
                        {
                            x += (double)digit / k;
                            k *= 10;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //окно с текстом об ошибке
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            private void MainForm_Load(object sender, EventArgs e)
            {

            }

            private void buttonOp_Click(object sender, EventArgs e)
            {
                try
                {
                    if (!res)
                    result();
                   Control b = (Control)sender;
                    string tag = (string)b.Tag;
                    operation = (Operation)Enum.Parse(typeof(Operation), tag);
                    /*  Enum.TryParse<Operation>(tag, out operation);
                     *  альтернативный вариант
                     *  */
                   // y = x;
                    newNumber = true;
                    res = false;
                }
                catch (Exception ex)
                {
                    // Окно с сообщением об ошибке
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            private void buttonResult_Click(object sender, EventArgs e)
            {
                result();
                res = true;
        }

        private void result()
        {
            try
            {
                switch (operation)
                {
                    case Operation.Addition:
                        y = x + y;
                        break;
                    case Operation.Subtraktion:
                        y = y - x;
                        break;
                    case Operation.Multiplication:
                        y = x * y;
                        break;
                    case Operation.Division:
                        y = y / x;
                        break;
                    default:
                        y = x;
                        break;

                }
                indicator.Text = y.ToString();

            }
            catch (Exception ex)
            {
                // Окно с сообщением об ошибке
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
            private void buttonC_Click(object sender, EventArgs e)
            {
                try
                {
                    x = 0;
                    y = 0;
                res = false;
                operation = (Operation)Enum.Parse(typeof(Operation), "Addition");
                }
                catch (Exception ex)
                {
                    // Окно с сообщением об ошибке
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            private void buttonPM_Click(object sender, EventArgs e)
            {
                try
                {
                res = false;
                x = Convert.ToDouble(indicator.Text) * -1;
                y = 0;

                }
                catch (Exception ex)
                {
                    // Окно с сообщением об ошибке
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            private void buttonSQRT_Click(object sender, EventArgs e)
            {
                try
                {
                res = false;
                if (x >= 0)
                        x = Math.Sqrt(Convert.ToDouble(indicator.Text));
                    else
                        MessageBox.Show("Число меньше 0!", title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    // Окно с сообщением об ошибке
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        private void button11_Click(object sender, EventArgs e)
        {
            z = 0;
        }

        private void MR_Click(object sender, EventArgs e)
        {
            x = z;
        }

        private void Mp_Click(object sender, EventArgs e)
        {
            z += x;
        }

        private void Mm_Click(object sender, EventArgs e)
        {
            z -= x;
        }

        private void Point_Click(object sender, EventArgs e)
        {
            if (!cpoint)
            {
                cpoint = true;
                k = 10;
                indicator.Text += ",";
            }
        }
    }
}
