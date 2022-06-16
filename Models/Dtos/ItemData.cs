using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WafferAPIs.DAL.Entites;
using WafferAPIs.Models;
using WafferAPIs.Models.Dtos;
using WafferAPIs.Models.Others;

namespace WafferAPIs.DAL.Entities
{
    public class ItemData
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name Should be filled")]
        public string Name { get; set; }

        public string  Color { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Dimensions { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? Weight { get; set; }

        public string PhotoLink { get; set; }

        [Required(ErrorMessage = "Brand Should be filled")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "ModelNumber Should be filled")]
        public string ModelNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Waranty { get; set; }

        public Double SaleRatio { get; set; } = 0;
        public string OtherFeatures { get; set; }

        [Required(ErrorMessage = "SubCategoryId Should be filled")]
        public Guid SubCategoryId{ get; set; }

        [Required(ErrorMessage = "SellerId Should be filled")]
        public Guid SellerId { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedDate { get; set; }

        //Vacuum Cleaners
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? WorkOnBattery { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? WorkOnChargerCable { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasLEDScreen { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Functions { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? Power { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? MotorType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? Capacity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? BatterryInfo { set; get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? BrushesNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? CabelLength { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? RemoteControl { get; set; }

        //Refrigerator
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? FreezerInclude { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? DoorNumbers { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? InDoorWaterDispsnser { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? DoorIceDispsnser { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? WaterFilteratiom { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? LEDLight { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? ControlType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? FreezerCapacity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? ChildLock { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? DoorOpenAlrm { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? EnergyGrade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? MobileConnection { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? MotorWarrenty { get; set; }

        //TV
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ScreenSize { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? DispalyType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Resoultion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? ConnectionToWifi { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasSataliteReciver { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasScreenShring { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasMagicMotion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? ScreenWarrenty { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? IsSmart { get; set; }

        //Mobile 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SIM { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Cameras { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? BoxComponent { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? Momory { get; set; }

        //HairDryer 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? HeatOptions { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? SpeedOptions { get; set; }

        //Washer
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? MaxSpanSpeed { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? NumberOfPrograms { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HasSteamFunctions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasQuickWashFunction { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasDryerFunction { get; set; }

    }
}
