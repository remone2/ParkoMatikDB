using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ParkoMatikDB.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public int PassID { get; set; }
        public virtual Pass Pass { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public bool Parked { get; set; }
    }

    public class Pass
    {
        public int ID { get; set; }
        public string Purchaser { get; set; }
        public bool Premium { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }

    public class ParkingSpot
    {
        public int ID { get; set; }
        public bool Occupied { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }

    public class Reservation
    {
        public int ID { get; set; }
        public int ParkingSpotID { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        public int VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class ParkingContext : DbContext
    {
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }
        public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }

    public class ParkingHelper
    {
        private ParkingContext parkingContext;
        public ParkingHelper(ParkingContext context)
        {
            this.parkingContext = context;
        }

        public Pass CreatePass(string purchaser, bool premium, int capacity)
        {
            Pass newPass = new Pass();

            if (purchaser.Length < 3 || purchaser.Length > 20)
            {
                throw new Exception("between 3 and 20");
            }
            else
            {
                newPass.Purchaser = purchaser;
            }
            
            newPass.Premium = premium;

            if (capacity <= 0)
            {
                throw new Exception("capacity greater than 0");
            }
            else
            {
                newPass.Capacity = capacity;
            }
            
            parkingContext.Passes.Add(newPass);
            parkingContext.SaveChanges();

            return newPass;
        }

        public ParkingSpot CreateParkingSpot()
        {
            ParkingSpot newSpot = new ParkingSpot();
            newSpot.Occupied = false;

            parkingContext.ParkingSpots.Add(newSpot);
            return newSpot;
        }
    }
}



