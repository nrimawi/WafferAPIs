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

        public bool pending { get; set; }= false;
        public string Name { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Dimensions { get; set; }

        public string Description { get; set; }
        public double Weight { get; set; }

        
        public string PhotoLink { get; set; }

        public string Brand { get; set; }

        public string ModelNumber { get; set; }

        public int Warranty { get; set; }
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


        public bool? HasLedScreen { get; set; }


        public string? Functions { get; set; }


        public int? Power { get; set; }


        public string? MotorType { get; set; }


        public double? Capacity { get; set; }


        public string? BatteryInfo { set; get; }


        public int? BrushesNumber { get; set; }


        public int? CableLength { get; set; }


        public bool? HasRemoteControl { get; set; }

        //Refrigerator
        public bool? FreezerInclude { get; set; }


        public int? DoorNumbers { get; set; }


        public bool? InDoorWaterDispsnser { get; set; }


        public bool? InDoorIceDispsnser { get; set; }


        public bool? WaterFilteration { get; set; }


        public bool? LedLight { get; set; }


        public string? ControlType { get; set; }


        public int? FreezerCapacity { get; set; }


        public bool? ChildLock { get; set; }


        public bool? HasAlarm { get; set; }


        public string? EnergyGrade { get; set; }


        public bool? MobileConnection { get; set; }


        public int? MotorWarranty { get; set; }
       
        //TV
        public double? ScreenSize { get; set; }


        public string? DispalyType { get; set; }


        public string? Resolution { get; set; }


        public bool? ConnectionToWifi { get; set; }


        public bool? HasSatelliteReceiver { get; set; }


        public bool? HasScreenShring { get; set; }


        public bool? SupportMagicMotion { get; set; }


        public int? ScreenWarrenty { get; set; }


        public bool? IsSmart { get; set; }

        //Mobile 
        public string? SIM { get; set; }


        public string? Cameras { get; set; }


        public string? BoxComponents { get; set; }

        public int? Memory { get; set; }

        //HairDryer 
        public int? HeatOptions { get; set; }


        public int? SpeedOptions { get; set; }

        //Washer
        public int? MaxSpanSpeed { get; set; }


        public int? NumberOfPrograms { get; set; }


        public bool? HasSteamFunctions { get; set; }


        public bool? HasQuickWashFunction { get; set; }


        public bool? HasDryerFunction { get; set; }

    }
}
