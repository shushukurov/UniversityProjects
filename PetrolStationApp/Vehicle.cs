using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetrolStationApp
{
    ///Class Vehicle with methods with necessary attributes for vehicles and methods to count time///
    public class Vehicle
    {
        //Vehicle method to start timer
        public Vehicle()
        {
            Timer tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(CountDown);
            this.timer = tmr;
            tmr.Start();
        }
        public int waitingTime { get; set; }
        public int pumpingTime { get; set; }

        public Timer timer { get; set; }
        public string type { get; set; }
        public double startFuelAmount { get; set; }
        public double maxFuelAmount { get; set; }
        public double fuelAmount { get; set; }
        //Countdown method to count seconds for waiting in the queue, if not served then leave station
        private void CountDown(object sender, EventArgs e)
        {
            this.waitingTime -= 1000;
            if (this.waitingTime <= 0)
            {
                if (Station.waitingVehicles.Any(x => x.type == this.type))
                {
                    Station.instance.RemoveWaitingVehicle(this);
                    Station.notservedV++;
                }
                this.timer.Stop();
            }
        }
    }
    ///Class PUMP with methods necessary for fuelling process
    public class Pump
    {
        // StartFill method to start timer to calculate feeling process
        public void StartFill()
        {
            Timer tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(FillFuel);
            this.timer = tmr;
            tmr.Start();
        }
        // StopFill method to stop timer
        public void StopFill()
        {
            this.timer.Stop();
        }


        public int Id { get; set; }
        public bool isFree { get; set; }
        public string name { get; set; }
        public Vehicle vehicle { get; set; }
        public string fuelType { get; set; }

        public Timer timer { get; set; }
        //FillFuel method to add value to fuelAmount(1.5) while subtracting time(1 second)
        private void FillFuel(object sender, EventArgs e)
        {
            vehicle.pumpingTime -= 1000;
            this.vehicle.fuelAmount += 1.5;
            Station.instance.Calculate(fuelType);
        }
    }
}
