using System.Globalization;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Localization;


namespace Pigeonlabs.Sitefinity
{
    /// <summary>
    /// helper for sitefinity localization
    /// ideal for api l10n
    /// </summary>
    public class L10n
    {
        public string Lang { get; } = "";

        public CultureInfo Culture { get; } = null;

        public CultureInfo CultureDefault
        {
            get
            {
                return Res.SiteCultures[0];
            }
        }

        /// <param name="lang">lang could be from Headers?.AcceptLanguage or from qry param</param>
        public L10n(string lang = "")
        {
            this.Lang = lang;
            try
            {
                this.Culture = new CultureInfo(lang);
            }
            catch { }
        }

        /// <summary>
        /// wrapper around Telerik.Sitefinity.Localization.Res.TryGet() method
        /// </summary>
        public string GetRes(string classId, string key, string defaultValue = "")
        {
            string res = "";
            if (!Res.TryGet(classId, key, Culture, out res))
            {
                res = defaultValue;
            }
            return res;
        }

        /// <summary>
        /// wrapper around Telerik.Sitefinity.Model.DataExtensions.GetString() method
        /// </summary>
        public string DynString(DynamicContent source, string fieldName, string defaultValue = "")
        {
            var lstring = source.GetString(fieldName);
            string res = DynString(lstring, defaultValue);
            return res;
        }

        /// <summary>
        /// look inside Lstring object
        /// </summary>
        public string DynString(Lstring lstring, string defaultValue = "")
        {
            string res = "";

            //check for right value
            lstring.TryGetValue(out res, Culture);

            //fallback on first culture available
            if (res == null)
                lstring.TryGetValue(out res, CultureDefault);

            //fallback on default value
            if (res == null)
                res = defaultValue;

            return res;
        }
    }
}
