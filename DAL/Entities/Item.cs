using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WafferAPIs.DAL.Entites;
using WafferAPIs.Models.Others;

namespace WafferAPIs.DAL.Entities
{
    public class Item
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Dimensions { get; set; }

        public double Weight { get; set; }


        public string PhotoLink { get; set; }

        public string Brand { get; set; }

        public string ModelNumber { get; set; }

        public int Waranty { get; set; }
        public Double SaleRatio { get; set; }

        public string OtherFeatures { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }

        public Boolean Status { get; set; }

        //Vacuum Cleaners

        public bool? WorkOnBattery { get; set; }


        public bool? WorkOnChargerCable { get; set; }


        public bool? HasLEDScreen { get; set; }


        public string? Functions { get; set; }


        public int? Power { get; set; }


        public string? MotorType { get; set; }


        public int? Capacity { get; set; }


        public string? BatterryInfo { set; get; }


        public int? BrushesNumber { get; set; }


        public int? CabelLength { get; set; }


        public bool? RemoteControl { get; set; }

        //Refrigerator
        public bool? FreezerInclude { get; set; }


        public int? DoorNumbers { get; set; }


        public bool? InDoorWaterDispsnser { get; set; }


        public bool? DoorIceDispsnser { get; set; }


        public bool? WaterFilteratiom { get; set; }


        public bool? LEDLight { get; set; }


        public string? ControlType { get; set; }


        public int? FreezerCapacity { get; set; }


        public bool? ChildLock { get; set; }


        public bool? DoorOpenAlrm { get; set; }


        public string? EnergyGrade { get; set; }


        public bool? MobileConnection { get; set; }


        public int? MotorWarrenty { get; set; }
        //TV


        public int? ScreenSize { get; set; }


        public string? DispalyType { get; set; }


        public string? Resoultion { get; set; }


        public bool? ConnectionToWifi { get; set; }


        public bool? HasSataliteReciver { get; set; }


        public bool? HasScreenShring { get; set; }


        public bool? HasMagicMotion { get; set; }


        public int? ScreenWarrenty { get; set; }


        public bool? IsSmart { get; set; }

        //Mobile 


        public string? SIM { get; set; }


        public string? Cameras { get; set; }


        public string? BoxComponent { get; set; }

        public int? Momory { get; set; }

        //HairDryer 

        public int? HeatOptions { get; set; }


        public int? SpeedOptions { get; set; }

        //Washer

        public int? MaxSpanSpeed { get; set; }


        public string? NumberOfPrograms { get; set; }


        public string? HasSteamFunctions { get; set; }


        public bool? HasQuickWashFunction { get; set; }


        public bool? HasDryerFunction { get; set; }

    }
}
