namespace ff.words.data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class SearchCriteriaBuilder
    {
        public static List<DynamicSearchModel> GetSearchCriteriaModel(string source)
        {
            source = source.Replace("contains", string.Empty);
            source = Regex.Replace(source, "[^0-9a-zA-Z ,]+", string.Empty);

            string[] stringSeparators = new string[] { "and" };
            string[] results = source.Split(stringSeparators, StringSplitOptions.None);

            List<DynamicSearchModel> dynamicSearchList = new List<DynamicSearchModel>();

            foreach (var result in results)
            {
                DynamicSearchModel searchModel = new DynamicSearchModel();
                var searchCriteria = result.Split(',');
                if (searchCriteria.Length > 1)
                {
                    var searchData = searchCriteria[1].Trim();
                    if (!string.IsNullOrEmpty(searchData))
                    {
                        var originalString = searchCriteria[0].Trim();
                        searchModel.PropertyName = originalString.First().ToString().ToUpper() + originalString.Substring(1);
                        searchModel.SearchData = searchData;

                        dynamicSearchList.Add(searchModel);
                    }
                }
            }

            return dynamicSearchList;
        }
    }
}
