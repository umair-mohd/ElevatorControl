/*************************************************************************
 * 
 * Umair's CONFIDENTIAL
 * __________________
 * 
 *  [2018] - [2022]  *  All Rights Reserved.
 * 
 * Win App to control multiple elevators in an apartment building.The algorithm should be able to handle 7 floors in a building and have a 2 elevators.
 * 
 * Algorithm should handle following operational scenarios:  
 * Efficient Scheduling of elevator.
 * Elevators should be created on random floors.  
 * Building must have minimum of 7 floors and 2 elevators.
 * User must use the features: Pressing the Up or down button at a floor, selecting a floor inside the elevator, etc.
 * Algorithm should give priority to nearest elevator to serve the passenger.Also algorithm must consider the moving direction of elevator while considering elevator to serve.  
 * Up/Down direction so elevator goes to appropriate floor.  
 * UI should be capable of handle adding / removing passenger from elevator.  
 * Door open/close (don’t move the elevator if door is open).  
 * 
 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Data.OleDb;


namespace ElevatorControl
{
    public partial class Form1 : Form
    {
        //variables

        int y_up = 63;
        int y_down = 376;
        int x_door_left_close = 74;
        int x_door_left_open = 12;
        int x_door_right_close = 139;

        int x_door_right_open = 200;

        bool go_up = false;
        bool go_down = false;

        bool go_up_lift2 = false;
        bool go_down_lift2 = false;

        bool arrived_G = false;
        bool arrived_1 = false;

        string liftName = "";
        int floor = 0;

        //object 
        SpeechSynthesizer reader = new SpeechSynthesizer();



        public Form1()
        {
            InitializeComponent();
            setCombobox();
            int pos1 = getliftPos("Lift1");
            int pos2 = getliftPos("Lift2");

            setPositionImageLift1(pos1);
            setPositionImageLift2(pos2);
        }

        private void setPositionImageLift1(int pos1)
        {
            switch (pos1)
            {
                case 0:
                    display_top.Image = global::ElevatorControl.Properties.Resources.G;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.G;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.G;
                    break;
                case 1:
                    display_top.Image = global::ElevatorControl.Properties.Resources._1;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._1;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._1;
                    break;
                case 2:
                    display_top.Image = global::ElevatorControl.Properties.Resources._2;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._2;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._2;
                    break;
                case 3:
                    display_top.Image = global::ElevatorControl.Properties.Resources._3;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._3;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._3;
                    break;
                case 4:
                    display_top.Image = global::ElevatorControl.Properties.Resources._4;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._4;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._4;
                    break;
                case 5:
                    display_top.Image = global::ElevatorControl.Properties.Resources._5;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._5;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._5;
                    break;
                case 6:
                    display_top.Image = global::ElevatorControl.Properties.Resources._6;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._6;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._6;
                    break;
                case 7:
                    display_top.Image = global::ElevatorControl.Properties.Resources._7;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources._7;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._7;
                    break;
                default:
                    // code block
                    break;
            }
        }
        private void setPositionImageLift2(int pos2)
        {
            switch (pos2)
            {
                case 0:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources.G;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources.G;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.G;
                    break;
                case 1:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._1;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._1;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._1;
                    break;
                case 2:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._2;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._2;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._2;
                    break;
                case 3:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._3;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._3;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._3;
                    break;
                case 4:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._4;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._4;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._4;
                    break;
                case 5:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._5;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._5;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._5;
                    break;
                case 6:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._6;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._6;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._6;
                    break;
                case 7:
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources._7;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources._7;
                    display_panel.Image = global::ElevatorControl.Properties.Resources._7;
                    break;
                default:
                    // code block
                    break;
            }
        }

        //Lift1 Up down
        private void timer_lift_down_Tick(object sender, EventArgs e) //Lift DOWN Animation 
        {

            if (picture_lift.Top <= y_down)
            {
                picture_lift.Top += 1;
            }
            else
            {
                timer_lift_down.Enabled = false;
                setButtonEnable();

                btn_down.BackColor = Color.White;
                btn_up.BackColor = Color.White;
                // btn_G.BackColor = Color.White;

                door_open_down(liftName, floor.ToString()); //When reached door open
                arrived_G = true;

                picture_lift.Image = global::ElevatorControl.Properties.Resources.Inside_of_the_lift;

                int pos1 = getliftPos("Lift1");
                int pos2 = getliftPos("Lift2");

                setPositionImageLift1(pos1);
                setPositionImageLift2(pos2); 
            }
        }

        private void timer_lift_up_Tick(object sender, EventArgs e) //Lift UP Animation
        {
            if (picture_lift.Top >= y_up)
            {
                picture_lift.Top -= 1;
            }
            else
            {
                timer_lift_up.Enabled = false;

                btn_up.BackColor = Color.White;
                setButtonEnable();
                btn_up.BackColor = Color.White;
                btn_down.BackColor = Color.White;

                door_open_up(liftName, floor.ToString()); //When reached door open
                arrived_1 = true;

                picture_lift.Image = global::ElevatorControl.Properties.Resources.Inside_of_the_lift;

                int pos1 = getliftPos("Lift1");
                int pos2 = getliftPos("Lift2");

                setPositionImageLift1(pos1);
                setPositionImageLift2(pos2);
                 
            }
        }

        //Lift2 Up down
        private void lift2_timer_lift_down_Tick(object sender, EventArgs e) //Lift DOWN Animation 
        {

            if (PictureLift2.Top <= y_down)
            {
                PictureLift2.Top += 1;
            }
            else
            {
                lift2_timer_lift_down.Enabled = false;
                setButtonEnable();

                btn_down.BackColor = Color.White;
                btn_up.BackColor = Color.White;
                // btn_G.BackColor = Color.White;

                door_open_down(liftName, floor.ToString()); //When reached door open
                arrived_G = true;

                PictureLift2.Image = global::ElevatorControl.Properties.Resources.Inside_of_the_lift;

                int pos1 = getliftPos("Lift1");
                int pos2 = getliftPos("Lift2");

                setPositionImageLift1(pos1);
                setPositionImageLift2(pos2);
                 
            }
        }

        private void lift2_timer_lift_up_Tick(object sender, EventArgs e) //Lift UP Animation
        {
            if (PictureLift2.Top >= y_up)
            {
                PictureLift2.Top -= 1;
            }
            else
            {
                lift2_timer_lift_up.Enabled = false;

                btn_up.BackColor = Color.White;
                setButtonEnable();
                btn_up.BackColor = Color.White;
                btn_down.BackColor = Color.White;

                door_open_up(liftName, floor.ToString()); //When reached door open
                arrived_1 = true;

                PictureLift2.Image = global::ElevatorControl.Properties.Resources.Inside_of_the_lift;

                int pos1 = getliftPos("Lift1");
                int pos2 = getliftPos("Lift2");

                setPositionImageLift1(pos1);
                setPositionImageLift2(pos2);
                 
            }
        }


        //Lift 1 Animation 
        private void door_open_down_Tick(object sender, EventArgs e) //lift1 - door open down  animation
        {
            if (door_left_down.Left >= x_door_left_open && door_right_down.Left <= x_door_right_open)
            {
                door_left_down.Left -= 1;
                door_right_down.Left += 1;
            }
            else
            {

                timer_door_open_down.Enabled = false;

            }
        }

        private void door_close_down_Tick(object sender, EventArgs e) //lift1 - door close down animation
        {
            if (door_left_down.Left <= x_door_left_close && door_right_down.Left >= x_door_right_close)
            {
                door_left_down.Left += 1;
                door_right_down.Left -= 1;
            }
            else
            {
                timer_door_close_down.Enabled = false;


                if (go_up == true)
                {
                    picture_lift.Image = global::ElevatorControl.Properties.Resources.lift_transparent;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.up;
                    display_top.Image = global::ElevatorControl.Properties.Resources.up;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.up;

                    reader.Speak("Go ing up");

                    timer_lift_up.Enabled = true;
                    go_up = false;
                }
            }
        }

        private void timer_door_open_up_Tick(object sender, EventArgs e) //lift1 - door open up animation
        {
            if (door_left_down.Left <= x_door_left_close && door_right_down.Left >= x_door_right_close)
            {
                door_left_down.Left += 1;
                door_right_down.Left -= 1;
            }
            else
            {
                timer_door_close_down.Enabled = false;


                if (go_up == true)
                {
                    picture_lift.Image = global::ElevatorControl.Properties.Resources.lift_transparent;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.up;
                    display_top.Image = global::ElevatorControl.Properties.Resources.up;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.up;

                    reader.Speak("Go ing up");

                    timer_lift_up.Enabled = true;
                    go_up = false;
                }
            }
        }

        private void timer_door_close_up_Tick(object sender, EventArgs e) //lift1 - door close up animation
        {
            if (door_left_up.Left <= x_door_left_close && door_right_up.Left >= x_door_right_close)
            {
                door_left_up.Left += 1;
                door_right_up.Left -= 1;
            }
            else
            {
                timer_door_close_up.Enabled = false;


                if (go_down == true)
                {
                    picture_lift.Image = global::ElevatorControl.Properties.Resources.lift_transparent;

                    display_panel.Image = global::ElevatorControl.Properties.Resources.down;
                    display_top.Image = global::ElevatorControl.Properties.Resources.down;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.down;


                    reader.Speak("Go ing  down");

                    timer_lift_down.Enabled = true;
                    go_down = false;
                }
            }
        }

        //Lift 2 Animation 
        private void Lift2_door_open_down_Tick(object sender, EventArgs e) //lift 2- door open down  animation
        {
            if (door_left_down_l2.Left >= x_door_left_open && door_right_down_l2.Left <= x_door_right_open)
            {
                door_left_down_l2.Left -= 1;
                door_right_down_l2.Left += 1;
            }
            else
            {

                Lift2_timer_door_open_down.Enabled = false;

            }
        }

        private void Lift2_door_close_down_Tick(object sender, EventArgs e) //lift 2- door close down animation
        {
            if (door_left_down_l2.Left <= x_door_left_close && door_right_down_l2.Left >= x_door_right_close)
            {
                door_left_down_l2.Left += 1;
                door_right_down_l2.Left -= 1;
            }
            else
            {
                Lift2_timer_door_close_down.Enabled = false;


                if (go_up_lift2 == true)
                {
                    PictureLift2.Image = global::ElevatorControl.Properties.Resources.lift_transparent;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.up;
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources.up;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources.up;

                    reader.Speak("Go ing up");

                    lift2_timer_lift_up.Enabled = true;
                    go_up_lift2 = false;
                }
            }
        }

        private void Lift2_timer_door_open_up_Tick(object sender, EventArgs e) //lift 2- door open up animation
        {
            if (door_left_down_l2.Left <= x_door_left_close && door_right_down_l2.Left >= x_door_right_close)
            {
                door_left_down_l2.Left += 1;
                door_right_down_l2.Left -= 1;
            }
            else
            {
                Lift2_timer_door_close_down.Enabled = false;


                if (go_up_lift2 == true)
                {
                    PictureLift2.Image = global::ElevatorControl.Properties.Resources.lift_transparent;
                    display_panel.Image = global::ElevatorControl.Properties.Resources.up;
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources.up;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources.up;

                    reader.Speak("Go ing up");

                    lift2_timer_lift_up.Enabled = true;
                    go_up_lift2 = false;
                }
            }
        }

        private void Lift2_timer_door_close_up_Tick(object sender, EventArgs e) //lift 2- door close animation
        {
            if (door_left_up_l2.Left <= x_door_left_close && door_right_up_l2.Left >= x_door_right_close)
            {
                door_left_up_l2.Left += 1;
                door_right_up_l2.Left -= 1;
            }
            else
            {
                Lift2_timer_door_close_up.Enabled = false;


                if (go_down_lift2 == true)
                {
                    PictureLift2.Image = global::ElevatorControl.Properties.Resources.lift_transparent;

                    display_panel.Image = global::ElevatorControl.Properties.Resources.down;
                    display_top_l2.Image = global::ElevatorControl.Properties.Resources.down;
                    display_bottom_l2.Image = global::ElevatorControl.Properties.Resources.down;


                    reader.Speak("Go ing  down");

                    lift2_timer_lift_down.Enabled = true;
                    go_down_lift2 = false;
                }

            }
        }

        // log buttons 
        private void btn_clearlog_Click(object sender, EventArgs e)
        {
            database_listbox.Items.Clear();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void door_close_down(int floor, string liftName)
        {
            reader.Speak("doors closing " + liftName);
            insertdata("Doors Closing " + liftName);

            //pos = getliftPos(rb);

            btn_1.BackColor = Color.Red;



            insertlift(liftName, floor.ToString());

            if (liftName == "Lift1")
            {
                timer_door_close_down.Enabled = true;
                timer_door_open_down.Enabled = false;
                setPositionImageLift1(floor);

            }
            else if (liftName == "Lift2")
            {
                Lift2_timer_door_close_down.Enabled = true;
                Lift2_timer_door_open_down.Enabled = false;
                setPositionImageLift2(floor);
            }
        }

        private void door_open_down(string liftName, string floor)
        {
            reader.Speak("floor" + floor + " for " + liftName + ", doors open ing");
            insertdata("Doors Opening " + liftName + " floor " + floor);

            if (floor == "G")
                floor = "0";
            int f = Convert.ToInt32(floor);

            insertlift(liftName, floor);

            if (liftName == "Lift1")
            {
                timer_door_close_down.Enabled = false;
                timer_door_open_down.Enabled = true;
                setPositionImageLift1(f);

            }
            else if (liftName == "Lift2")
            {
                Lift2_timer_door_close_down.Enabled = false;
                Lift2_timer_door_open_down.Enabled = true;
                setPositionImageLift2(f);
            }

        }

        private void door_close_up(int floor, string liftName)
        {
            reader.Speak("doors closing " + liftName);
            insertdata("Doors Closing " + liftName);


            if (liftName == "Lift1")
            {
                timer_door_close_up.Enabled = true;
                timer_door_open_up.Enabled = false;

            }
            else if (liftName == "Lift2")
            {
                Lift2_timer_door_close_up.Enabled = true;
                Lift2_timer_door_open_up.Enabled = false;
            }


        }

        private void door_open_up(string liftName, string floor)
        {
            reader.Speak("floor" + floor + " for " + liftName + ", doors open ing");
            insertdata("Doors Opening lift " + liftName + " floor " + floor);
            //insertdata("Doors Opening @First Floor");


            if (floor == "G")
                floor = "0";
            int f = Convert.ToInt32(floor);

            string rb = null;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            insertlift(liftName, floor);

            if (rb == "Lift1")
            {
                timer_door_close_up.Enabled = false;
                timer_door_open_up.Enabled = true;
                setPositionImageLift1(f);

            }
            else if (rb == "Lift2")
            {
                Lift2_timer_door_close_up.Enabled = false;
                Lift2_timer_door_open_up.Enabled = true;
                setPositionImageLift2(f);
            }
        }

        private void going_up(int floor, string liftName)
        {


            int pos1 = getliftPos("Lift1");
            int pos2 = getliftPos("Lift2");

            setButtonDisabled();
            door_close_down(floor, liftName);
            arrived_G = false;

            insertdata(liftName + " Going Up on floor " + floor);

        }



        private void going_down(int floor, string liftName)
        {

            int pos1 = getliftPos("Lift1");
            int pos2 = getliftPos("Lift2");

            setButtonDisabled();

            door_close_up(floor, liftName);
            arrived_1 = false;
            insertdata(liftName + " Going Down on floor " + floor);


        }
        //set button toggle
        private void setButtonDisabled()
        {
            btn_G.Enabled = false;
            btn_1.Enabled = false;
            btn_2.Enabled = false;
            btn_3.Enabled = false;
            btn_4.Enabled = false;
            btn_5.Enabled = false;
            btn_6.Enabled = false;
            btn_7.Enabled = false;
            btn_down.Enabled = false;
            btn_up.Enabled = false;
            btn_close.Enabled = false;
            btn_open.Enabled = false;
            cb_floor.Enabled = false;
        }
        private void setButtonEnable()
        {
            btn_G.Enabled = true;
            btn_1.Enabled = true;
            btn_2.Enabled = true;
            btn_3.Enabled = true;
            btn_4.Enabled = true;
            btn_5.Enabled = true;
            btn_6.Enabled = true;
            btn_7.Enabled = true;
            btn_down.Enabled = true;
            btn_up.Enabled = true;
            btn_close.Enabled = true;
            btn_open.Enabled = true;
            cb_floor.Enabled = true;
        }
        //Up-down Panel

        private void btn_down_Click(object sender, EventArgs e)
        {
            btn_up.BackColor = Color.Red;

            string f = cb_floor.Text.ToString();
            if (f == "G")
                f = "0";
            floor = Convert.ToInt32(f);

            int pos1 = getliftPos("Lift1");
            int pos2 = getliftPos("Lift2");

            int lift1diff = Math.Abs(floor - pos1);
            int lift2diff = Math.Abs(floor - pos2);

            if (lift1diff < lift2diff)
            {
                liftName = "Lift1";
                if (pos1 < floor)
                {
                    go_up = true;
                    going_up(floor, liftName);
                }
                else if (pos1 > floor)
                {
                    go_down = true;
                    going_down(floor, liftName);
                }
            }

            else if (lift1diff > lift2diff)
            {
                liftName = "Lift2";
                if (pos2 < floor)
                {
                    go_up_lift2 = true;
                    going_up(floor, liftName);
                }
                else if (pos2 > floor)
                {
                    go_down_lift2 = true;
                    going_down(floor, liftName);
                }
            }
            else
            {
                liftName = "Lift1";
                if (pos2 < floor)
                {
                    go_up = true;
                    going_up(floor, liftName);
                }
                else if (pos2 > floor)
                {
                    go_up = true;
                    going_down(floor, liftName);
                }
            }

        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            liftName = null;
            btn_down.BackColor = Color.Red;

            string f = cb_floor.Text.ToString();
            if (f == "G")
                f = "0";
            floor = Convert.ToInt32(f);

            int pos1 = getliftPos("Lift1");
            int pos2 = getliftPos("Lift2");

            int lift1diff = Math.Abs(floor - pos1);
            int lift2diff = Math.Abs(floor - pos2);

            if (lift1diff < lift2diff)
            {
                liftName = "Lift1";
                if (pos1 <= floor)
                {
                    go_up = true;
                    going_up(floor, liftName);

                }
                else if (pos1 > floor)
                {
                    go_down = true;
                    going_down(floor, liftName);
                }
            }

            else if (lift1diff > lift2diff)
            {
                liftName = "Lift2";
                if (pos2 <= floor)
                {
                    go_up_lift2 = true;
                    going_up(floor, liftName);
                }
                else if (pos2 > floor)
                {
                    go_down_lift2 = true;
                    going_down(floor, liftName);
                }
            }
            else
            {
                liftName = "Lift1";
                if (pos2 < floor)
                {
                    go_up = true;
                    going_up(floor, liftName);
                }
                else if (pos2 > floor)
                {
                    go_down = true;
                    going_down(floor, liftName);
                }
            }
        }

        //panel buttons

        private void btn_7_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 7;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_7.BackColor = Color.Red;

            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_6_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 6;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_6.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_5_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 5;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_5.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_4_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 4;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_4.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_3_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 3;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_3.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_2_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 2;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_2.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }
        private void btn_1_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 1;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;

            pos = getliftPos(rb);

            btn_1.BackColor = Color.Red;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;

                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }

        private void btn_G_Click(object sender, EventArgs e)
        {
            string rb = null;
            int pos = 0, floor = 0;

            if (Lift1.Checked == true)
                rb = Lift1.Text;
            else if (Lift2.Checked == true)
                rb = Lift2.Text;
            pos = getliftPos(rb);
            btn_G.BackColor = Color.OrangeRed;
            if (pos < floor)
            {
                if (rb == "Lift1")
                    go_up = true;
                else if (rb == "Lift2")
                    go_up_lift2 = true;
                going_up(floor, rb);

            }
            else if (pos > floor)
            {
                if (rb == "Lift1")
                    go_down = true;
                else if (rb == "Lift2")
                    go_down_lift2 = true;
                going_down(floor, rb);
            }
            insertlift(rb, floor.ToString());
        }

        private void btn_close_Click(object sender, EventArgs e)
        {

            int pos = 0;
            string rb = null;

            if (Lift1.Checked == true)
            {
                pos = getliftPos("Lift1");
                rb = Lift1.Text;
            }
            else if (Lift2.Checked == true)
            {
                pos = getliftPos("Lift2");
                rb = Lift2.Text;
            }

            string f = cb_floor.Text.ToString();
            if (f == "G")
                f = "0";
            floor = Convert.ToInt32(f);

            if (pos > floor)
                door_close_down(floor, liftName);
            else if (pos < floor)
                door_close_up(floor, liftName);
            else
            {
                door_close_down(floor, liftName);
                door_close_up(floor, liftName);
            }
            cb_floor.Text = pos.ToString();

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            int pos = 0;

            string rb = null;

            if (Lift1.Checked == true)
            {
                pos = getliftPos("Lift1");
                rb = Lift1.Text;
            }
            else if (Lift2.Checked == true)
            {
                pos = getliftPos("Lift2");
                rb = Lift2.Text;
            }
            cb_floor.Text = pos.ToString();
            door_open_up(rb, pos.ToString());

        }

        private void btn_alarm_Click(object sender, EventArgs e)
        {
            btn_alarm.BackColor = Color.Green;
            reader.Speak("Emergency Stop. Please exit carefully.");
            insertdata("Emergency Stop!");
            timer_lift_down.Enabled = false;
            timer_lift_up.Enabled = false;
            timer_door_open_down.Enabled = true;
            timer_door_open_up.Enabled = true;
            display_panel.Image = global::ElevatorControl.Properties.Resources.alarmbellbutton;
            display_top.Image = global::ElevatorControl.Properties.Resources.alarmbellbutton;
            display_bottom.Image = global::ElevatorControl.Properties.Resources.alarmbellbutton;

        }



        //Database Code

        //Database Variables 
        private bool filled;
        public DataSet ds = new DataSet();



        private void btn_displaylog_Click(object sender, EventArgs e)
        {
            ds.Clear();
            try
            {
                string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ElevatorDatabase.accdb;";
                string dbcommand = "Select * from Actions;";
                OleDbConnection conn = new OleDbConnection(dbconnection);
                OleDbCommand comm = new OleDbCommand(dbcommand, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(comm);

                //cnn.Open();
                conn.Open();
                //MessageBox.Show("Connection Open ! ");
                while (filled == false)
                {
                    adapter.Fill(ds);
                    filled = true;
                }
                filled = false;
                //cnn.Close();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
                string message = "Error in connection to datasource";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }

            database_listbox.Items.Clear();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                database_listbox.Items.Add(row["Date"] + "\t\t" + row["Time"] + "\t\t" + row["Action"]);
            }

        }

        private void insertdata(string action)
        {
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source = ElevatorDatabase.accdb;";
            string dbcommand = "insert into [Actions] ([Date],[Time],[Action]) values (@date, @time, @action)";
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();


            database_listbox.Items.Add(date + "\t\t" + time + "\t\t" + action);



            OleDbConnection conn_db = new OleDbConnection(dbconnection);
            OleDbCommand comm_insert = new OleDbCommand(dbcommand, conn_db);
            OleDbDataAdapter adapter_insert = new OleDbDataAdapter(comm_insert);
            comm_insert.Parameters.AddWithValue("@date", date);
            comm_insert.Parameters.AddWithValue("@time", time);
            comm_insert.Parameters.AddWithValue("@action", action);




            conn_db.Open();

            comm_insert.ExecuteNonQuery();

            conn_db.Close();
        }

        private void Lift1_CheckedChanged(object sender, EventArgs e)
        {
            Lift1.Checked = !Lift2.Checked;
            int pos = getliftPos("Lift1");
            setPositionImageLift1(pos);
        }

        private void Lift2_CheckedChanged(object sender, EventArgs e)
        {
            Lift2.Checked = !Lift1.Checked;
            int pos = getliftPos("Lift2");
            setPositionImageLift2(pos);
        }

        private int getliftPos(string liftName)
        {
            try
            {
                string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ElevatorDatabase.accdb;";
                string dbcommand = "Select liftPos from LiftPosition where liftName ='" + liftName + "';";
                OleDbConnection conn = new OleDbConnection(dbconnection);
                OleDbCommand comm = new OleDbCommand(dbcommand, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(comm);

                conn.Open();
                //MessageBox.Show("Connection Open ! ");
                while (filled == false)
                {
                    adapter.Fill(ds);
                    filled = true;
                }
                filled = false;
                //cnn.Close();
                conn.Close();


            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
                string message = "Error in connection to datasource";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }

            int s = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                s = Convert.ToInt32(row.ItemArray[0]);
            }
            return s;
        }

        private void insertlift(string liftName, string liftpos)
        {
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source = ElevatorDatabase.accdb;";
            string dbcommand = "update [LiftPosition] set Liftpos = '" + liftpos + "' , dateModified = '" + date + "', timeModified = '" + time + "' where LiftName = '" + liftName + "';";

            OleDbConnection conn_db = new OleDbConnection(dbconnection);
            OleDbCommand comm_insert = new OleDbCommand(dbcommand, conn_db);
            OleDbDataAdapter adapter_insert = new OleDbDataAdapter(comm_insert);
            comm_insert.Parameters.AddWithValue("@LiftPos", liftpos);
            comm_insert.Parameters.AddWithValue("@dateModified", date);
            comm_insert.Parameters.AddWithValue("@timeModified", time);

            conn_db.Open();
            comm_insert.ExecuteNonQuery();
            conn_db.Close();
        }

        private void cb_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCombobox();
        }
        private void setCombobox()
        {
            if (cb_floor.Text == "G")
            {
                btn_up.Enabled = false;
                btn_down.Enabled = true;
            }
            else if (cb_floor.Text == "7")
            {
                btn_up.Enabled = true;
                btn_down.Enabled = false;

            }
            else
            {
                btn_up.Enabled = true;
                btn_down.Enabled = true;
            }
        }
    }
}