using System;
using System.Text.Json.Serialization;

namespace WafferAPIs.Models.Others
{
    public class SubCategoryFeature
    {


        public string CodeName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NameToShow { get; set; }

        public string Type { get; set; }

        public SubCategoryFeature()
        {
            NameToShow = null;

        }
        public SubCategoryFeature(string codeName, string type)
        {
            Type = type;

            if (codeName.Length != 0)
            {
                if (codeName.Length < 1)
                    codeName = Char.ToLower(codeName[0]) + "";
                else
                    codeName = Char.ToLower(codeName[0]) + codeName.Substring(1, codeName.Length - 1);

                CodeName = codeName;

                if (codeName.Length < 1)
                    codeName = Char.ToUpper(codeName[0]) + "";
                else
                    codeName = Char.ToUpper(codeName[0]) + codeName.Substring(1, codeName.Length - 1);


                NameToShow = System.Text.RegularExpressions.Regex.Replace(codeName, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
            }
            else
                throw new Exception("Error at inserting feature");
        }
    }


}
