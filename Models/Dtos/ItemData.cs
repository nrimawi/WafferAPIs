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

        #region Genral Features
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name Should be filled")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price Should be filled")]
        public double Price { get; set; }

        public string Color { get; set; }

        public string Dimensions { get; set; }


        public double? Weight { get; set; }

        public string PhotoLink { get; set; }

        [Required(ErrorMessage = "Brand Should be filled")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "ModelNumber Should be filled")]
        public string ModelNumber { get; set; }

        public int? Warranty { get; set; }
        [Required(ErrorMessage = "Description Should be filled")]

        public string Description { get; set; }

        public Double SaleRatio { get; set; } = 0;
        public string OtherFeatures { get; set; }

        [Required(ErrorMessage = "SubCategoryId Should be filled")]
        public Guid SubCategoryId { get; set; }

        [Required(ErrorMessage = "SellerId Should be filled")]
        public Guid SellerId { get; set; }
        #endregion


        #region Vacuum Cleaners
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? WorkOnBattery { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? WorkOnChargerCable { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasLedScreen { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Functions { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? Power { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? MotorType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public double? Capacity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? BatteryInfo { set; get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? BrushesNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? CableLength { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasRemoteControl { get; set; }
        #endregion

        #region Refrigerator
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? FreezerInclude { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? DoorNumbers { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? InDoorWaterDispsnser { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? InDoorIceDispsnser { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? WaterFilteration{ get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? LedLight { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? ControlType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? FreezerCapacity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? ChildLock { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasAlarm { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? EnergyGrade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? MobileConnection { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? MotorWarranty { get; set; }
        #endregion

        #region TV
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ScreenSize { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? DispalyType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Resolution { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? ConnectionToWifi { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasSatelliteReceiver { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? HasScreenShring { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? SupportMagicMotion { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? ScreenWarrenty { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public bool? IsSmart { get; set; }
        #endregion

        #region Mobile
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SIM { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cameras { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? BoxComponents { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? Momory { get; set; }
        #endregion

        #region HairDryer
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? HeatOptions { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? SpeedOptions { get; set; }
        #endregion

        #region Washer
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
        #endregion

    }
}
