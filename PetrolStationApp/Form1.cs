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

namespace PetrolStationApp
{
    ///Class Station with all necessary data for methods and classes (e.g vehicle type, fuel type, fuel price, tank amount) and methods to create station itself
    public partial class Station : Form
    {
        private int locationHeight = 28;  //Location of Queuelist
        private int vehicleLabel_Height = 33;
        public static List<Vehicle> waitingVehicles = new List<Vehicle>(); //List of vehicles in the queue

        public static List<Vehicle> pumpingVehicles = new List<Vehicle>(); //List of vehicles in the pumping

        public static List<Pump> pumps = new List<Pump>(); //List of petrol pumps

        private static Random random = new Random(); //Random instantiation

        private static string[] fuelTypes = new string[] { "Diesel", "LPG", "Unleaded" }; //List of fuel types
        private static double[] vehicleMaxFuelAmounts = new double[] { 40, 80, 150 }; //List of tank capacities
        private static string[] vehicleTypes = new string[] { "Car", "Van", "HGV" }; //List of vehicle types
        public static double[] fuelAmounts = new double[] { 0, 0, 0 }; //List of sold fuel amounts
        public static double[] pounds = new double[] { 0, 0, 0 }; //List of sold fuel profit
        private double[] fuelPrices = new double[] { 1.15, 0.7, 1.1 }; //List of fuel prices
        public static int servedV, notservedV;

        //Singleton
        public static Station instance;

        public Station() //Constructor of station class
        {
            InitializeComponent();
            instance = this;
        }

        //Method to load station
        private void Station_Load(object sender, EventArgs e)
        {
            //Loop to create pumps
            for (int i = 1; i <= 9; i++)
            {
                Pump pump = new Pump();
                pump.isFree = true;
                pump.Id = i;
                pump.name = "pump" + i.ToString();
                pumps.Add(pump);
                foreach (Control cnt in Controls)
                {
                    if (cnt is Label)
                    {
                        if (cnt.Name == pump.name)
                        {
                            cnt.Text += "";
                        }
                    }
                }
            }

            GetNewVehicle.Start(); //Get a vehicle with random attributes in random time interval.
            CheckPump.Start(); //Check pumps and assign free pump for the first vehicle in the queue
            SecondCounter.Start(); //Starts timer for fuelling
        }

        //Get new vehicle ticker

        private void GetNewVehicle_Tick(object sender, EventArgs e)
        {
            getNewVehicle();
        }

        //Method to get new vehicle
        public void getNewVehicle()
        {
            GetNewVehicle.Interval = random.Next(1500, 2200);
            Vehicle vehicle = new Vehicle();
            int rt = random.Next(0, 3);
            vehicle.type = vehicleTypes[rt];
            vehicle.startFuelAmount = (int)(random.NextDouble() * vehicleMaxFuelAmounts[rt] / 4);
            vehicle.maxFuelAmount = vehicleMaxFuelAmounts[rt];
            vehicle.waitingTime = random.Next(1000, 2000);
            vehicle.pumpingTime = ((int)((vehicle.maxFuelAmount - vehicle.startFuelAmount) / 1.5) + 1) * 1000;

            waitingVehicles.Add(vehicle);

            Label vehicleLabel = new Label();
            vehicleLabel.Width = 178;
            vehicleLabel.Height = vehicleLabel_Height;
            vehicleLabel.Text = vehicle.type.ToString();
            locationHeight += vehicleLabel_Height;
            vehicleLabel.Location = new Point(28, locationHeight);
            vehicleLabel.Visible = true;
            groupBox1.Controls.Add(vehicleLabel);
            SortOrder();

        }



        //Method to check pumps status
        private void CheckPump_Tick(object sender, EventArgs e)
        {
            List<Pump> freePumps = FindPump();
            
            if (waitingVehicles.Count > 0 && freePumps != null)
            {
                freePumps = Sequence(freePumps);
                foreach (Pump freePump in freePumps)
                {
                    Vehicle vehicle = waitingVehicles.FirstOrDefault();
                    if (vehicle == null) break;
                    string freeFuelType = "";
                    if (vehicle.type == "Car")
                        freeFuelType = fuelTypes[random.Next(0, 3)];
                    if (vehicle.type == "Van")
                        freeFuelType = fuelTypes[random.Next(0, 2)];
                    if (vehicle.type == "HGV")
                        freeFuelType = "Diesel";
                    freePump.fuelType = freeFuelType;
                    GoToPumping(vehicle, freePump);
                }
            }
        }

        //Method to find free pump
        private List<Pump> FindPump()
        {
            List<Pump> freePumps = new List<Pump>();
            freePumps = pumps.OrderByDescending(x => x.Id).Where(x => x.isFree == true).ToList();
            if (freePumps.Count() != 0)
            {
                return freePumps;
            }
            return null;
        }

        // Logic of queue system
        List<Pump> Sequence(List<Pump> freePumps)
        {
            List<Pump> rp = new List<Pump>();
            foreach (Pump pump in freePumps)
            {
                int id = pump.Id;
                switch (id)
                {
                    case 2: {if (!pumps[0].isFree) rp.Add(pump); }break;
                    case 3: {if (!pumps[0].isFree || !pumps[1].isFree) rp.Add(pump); } break;
                    case 5: { if (!pumps[3].isFree) rp.Add(pump); } break;
                    case 6: { if (!pumps[3].isFree || !pumps[4].isFree) rp.Add(pump); } break;
                    case 8: { if (!pumps[6].isFree) rp.Add(pump); } break;
                    case 9: { if (!pumps[6].isFree || !pumps[7].isFree) rp.Add(pump); } break;
                }
            }
            foreach (Pump p in rp)
                freePumps.Remove(p);
            return freePumps;
        }

        //Method to assign pump to vehicle
        private void GoToPumping(Vehicle vehicle, Pump pump)
        {
            pump.isFree = false;
            pump.vehicle = vehicle;
            pump.StartFill();
            SortPumps(pump);
            pumpingVehicles.Add(vehicle);
            RemoveWaitingVehicle(vehicle);
        }
        //Method to remove waiting vehicle from the queue if time is up
        public void RemoveWaitingVehicle(Vehicle vehicle)
        {
            waitingVehicles.Remove(vehicle);
            foreach (Control gb in groupBox1.Controls)
            {
                if (gb.Text == vehicle.type)
                {
                    locationHeight -= vehicleLabel_Height;
                    groupBox1.Controls.Remove(gb);
                    SortOrder();
                    break;
                }
            }
            n_serverd.Text = notservedV.ToString();
        }

        // Method to update counters
        public void Calculate(string type)
        {
            switch (type)
            {
                case "Diesel":
                    {
                        fuelAmounts[0] += 1.5;
                        pounds[0] = fuelAmounts[0] * fuelPrices[0];

                        diesel_l.Text = fuelAmounts[0].ToString();
                        diesel_p.Text = pounds[0].ToString() + " P";
                    }
                    break;
                case "LPG":
                    {
                        fuelAmounts[1] += 1.5;
                        pounds[1] = fuelAmounts[1] * fuelPrices[1];

                        lpg_l.Text = fuelAmounts[1].ToString();
                        lpg_p.Text = pounds[1].ToString() + " P";
                    }
                    break;
                case "Unleaded":
                    {
                        fuelAmounts[2] += 1.5;
                        pounds[2] = fuelAmounts[2] * fuelPrices[2];

                        unleaded_l.Text = fuelAmounts[2].ToString();
                        unleaded_p.Text = pounds[2].ToString() + " P";
                    }
                    break;
            }
            total_l.Text = (fuelAmounts.Sum()).ToString();
            total_p.Text = (pounds.Sum()).ToString() + " P";
            onePercent.Text = (pounds.Sum() / 100).ToString() + " P";
        }

        //Method to sort vehicles
        private void SortOrder()
        {
            int locy = 28;
            foreach (Label label in groupBox1.Controls)
            {
                label.Location = new Point(28, locy);
                locy += vehicleLabel_Height;
            }
        }


        //Method to sort pumps
        private void SortPumps(Pump pump)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    if (control.Name == pump.name)
                    {
                        control.Text = pump.isFree == true ? "Free" : pump.vehicle.type;
                    }
                }
            }
        }

        //Method for the ticker of second counter
        private void SecondCounter_Tick(object sender, EventArgs e)
        {
            foreach (var pump in pumps)
            {
                if (!pump.isFree)
                {
                    if ((pump.vehicle.pumpingTime - 1000) <= 0)
                    {
                        pump.vehicle.fuelAmount = pump.vehicle.maxFuelAmount - pump.vehicle.startFuelAmount;
                    }
                    if (pump.vehicle.pumpingTime <= 0)
                    {
                        AddServedVehicle(pump);
                        pump.vehicle.fuelAmount = 0;
                        ChangePumpText(pump);
                        pump.vehicle = null;
                        pump.isFree = true;
                        pumpingVehicles.Remove(pump.vehicle);
                        pump.StopFill();
                        SortPumps(pump);
                    }
                    else
                    {
                        ChangePumpText(pump);
                    }
                }
            }
        }
        // Updating pumps text
        void ChangePumpText(Pump pump)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Name == pump.name)
                {
                    foreach (Control ct in this.Controls)
                    {
                        if (ct is Label && ct.Name == control.Name + "Screen")
                        {
                            ct.Text = pump.vehicle.fuelAmount + "L";
                        }
                    }
                    foreach (Control ct in this.Controls)
                    {
                        if (ct is Label && ct.Name == control.Name + "Pound")
                        {
                            ct.Text = pump.vehicle.fuelAmount * fuelPrices[Array.IndexOf(fuelTypes, pump.fuelType)] + " P";
                        }
                    }
                }
            }
        }
        // Method to create and update counter of served vehicles
        private void AddServedVehicle(Pump p)
        {
            servedV++;
            served.Text = servedV.ToString();
            string info = served.Text + " " + p.vehicle.type +
                       "\n Fuel type : " + p.fuelType +
                       "\n Fuel dispensed : " + (p.vehicle.fuelAmount) +
                       "\n Pump : " + p.Id + "\n\n";
            richTextBox1.Text = info + richTextBox1.Text;
        }
    }

}
