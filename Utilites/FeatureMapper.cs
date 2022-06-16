using System.Collections.Generic;
using WafferAPIs.Models.Others;

namespace WafferAPIs.Utilites
{
    public class FeatureMapper
    {
        public List<SubCategoryFeature> ToDto(string text)
        {

            try
            {

                List<SubCategoryFeature> res = new List<SubCategoryFeature>();

                foreach (string feature in text.Split(','))
                {
                    int index = 0;
                    string codeName = "";
                    string type = "";

                    foreach (string featureElement in feature.Split(':'))
                    {

                        if (index == 0)
                            codeName=featureElement;

                        else 
                            type= featureElement;

                        index++;
                    }
                    SubCategoryFeature subCategoryFeature = new SubCategoryFeature(codeName, type);
                    res.Add(subCategoryFeature);

                }
                return res;
            }
            catch
            {
                throw;
            }

        }

        public string ToEntity(List<SubCategoryFeature> features)
        {
            string res = "";

            try
            {
                int index = 0;
                foreach (SubCategoryFeature feature in features)
                {
                    if (index == 0)
                        res = feature.CodeName + ":" + feature.Type;
                    else
                        res = res + "," + feature.CodeName + ":" + feature.Type;


                    index++;
                }
                return res;

            }
            catch { throw; }
        }
    }
}
