using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Diagnostics;
namespace Calculator_StrategyPattern
{

    public partial class Calculator : Window

    {
        private AreaInterface geometric_figure; //reference of our interface 
        private List<double> values = new List<double>(); //List with the values of the boxes
        private List<TextBox> values_box = new List<TextBox>(); //List with the of the boxes
        private List<RadioButton> radio_buttons = new List<RadioButton>();//List with the radio buttons
        private Boolean stop = false; // to kill thread
        private int index = 0;

        public Calculator()
        {
            InitializeComponent();
            radio_buttons.Add(triangle_radio);//filling the list of radio buttons
            radio_buttons.Add(Rectangle_radio);
            radio_buttons.Add(Circle_radio);
            radio_buttons.Add(Polygon_radioy);
            //
            values_box.Add(base_box);//filling the list of textboxs
            values_box.Add(height_box);
            values_box.Add(radio_box);
            values_box.Add(sides_box);
            thread_result();
        }

        /// <summary>
        /// This method will allow to obtain data from the text box for the necessary operation
        /// </summary>
        /// <param name="box1"> Enable to use the box, false to not</param>
        private void myBoxes(Boolean box1, Boolean box2, Boolean box3, Boolean box4)
        {
            Boolean isCorrect = false;
            Boolean[] box_status = new Boolean[] { box1, box2, box3, box4 };//new matrix with the data box we need
            for (int i = 0; i < box_status.Length; i++) { values_box[i].IsEnabled = box_status[i]; }
            values_box.ForEach(box =>
            {
                if (box.IsEnabled && box.Text.Length != 0) { isCorrect = true; }
                else if (box.IsEnabled) { isCorrect = false; }//if we need the value of this box, and this is not empty we will take it value
            });

            if (index == 4 && isCorrect)//if the operation is of a regular polygon
            {
                double side = Convert.ToDouble(sides_box.Text);
                if (side < 5)//the side of it must be 5 or mor 
                {
                    isCorrect = false;
                    values_box.ForEach(box => box.Text = "");// If not, let's show a message to the user.
                    MessageBox.Show("Please select the correct option for this. (Rectangle | Triangle)");
                }
            }
            if (isCorrect) { calculate(); }//if all the boxes has correct value let's execute the strategy 
        }


        public void calculate()//Method to start the strategy
        {
            values = new List<double>();//initializing the List
            try
            {
                switch (index)//index of the operation
                {
                    case 1:
                        values.Add(Convert.ToDouble(base_box.Text)); //add the necessary values to the list
                        values.Add(Convert.ToDouble(height_box.Text));
                        geometric_figure = new TriangleArea(values); //selecting the object from our interface to solve the problem
                        break;
                    case 2:
                        values.Add(Convert.ToDouble(base_box.Text));
                        values.Add(Convert.ToDouble(height_box.Text));
                        geometric_figure = new Rectangle(values);
                        break;
                    case 3:
                        values.Add(Convert.ToDouble(radio_box.Text));
                        values.Add(Convert.ToDouble(Math.PI));
                        geometric_figure = new Circle(values);
                        break;
                    case 4:
                        values.Add(Convert.ToDouble(base_box.Text));
                        values.Add(Convert.ToDouble(sides_box.Text));
                        values.Add(Convert.ToDouble(radio_box.Text));
                        geometric_figure = new Polygon(values);
                        break;
                }
                geometric_figure.calculateArea(); //calling the method to do the math operation
                result_label.Content = geometric_figure.showResult(); //showing the result on screen

            }
            catch
            {
                MessageBox.Show("only numbers and no blanks"); // possible user errors
                values_box.ForEach(box => box.Text = "");
            }
        }



        //this thread will update values and status in background
        private void thread_result()
        {
            new Thread(() =>
            {
                while (!stop)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        if (triangle_radio.IsChecked == true)//if user wants to do a triangle operation
                        {
                            index = 1;//setting the index
                            form_label.Content = "FORMULA (B*H)/2"; //showing the form
                            myBoxes(true, true, false, false); // enabling the necessary boxes to obtain the information
                            //this method will execute the strategy.
                        }
                        if (Rectangle_radio.IsChecked == true)
                        {
                            index = 2;
                            form_label.Content = "FORMULA B*H";
                            myBoxes(true, true, false, false);
                        }
                        if (Circle_radio.IsChecked == true)
                        {
                            index = 3;
                            form_label.Content = "FORMULA π*R^2";
                            myBoxes(false, false, true, false);
                        }
                        if (Polygon_radioy.IsChecked == true)
                        {
                            index = 4;
                            form_label.Content = "FORMULA (N*S*A)/2";
                            myBoxes(true, false, true, true);

                        }
                    });


                }
                Thread.Sleep(1000);

            }).Start();

        }

    }
}
