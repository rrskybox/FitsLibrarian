using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.AppBroadcasting;

namespace FitsLibrarian
{
    internal static class FitsFielder
    {
        //Contains objects for managing lists of fits fields


        private static List<string> CommonHeaderFields = new List<string>();
        private static List<string> EnabledHeaderFields = new List<string>();
        private static List<string> ColumnHeaders = new List<string>();

        public static void ResetFielder()
        {
            //Clear both common and enabled field lists
            CommonHeaderFields.Clear();
            EnabledHeaderFields.Clear();
            ColumnHeaders.Clear();
            LoadEnabledFields();
        }

        public static List<string> GetAllColumnHeaders()
        {
            //Return list of common fields
            CommonHeaderFields = CommonHeaderFields.Distinct().ToList();
            return ColumnHeaders;
        }

        public static void AddCommonField(string field)
        {
            //adds to column header list 
            CommonHeaderFields.Add(field);
            CommonHeaderFields = CommonHeaderFields.Distinct().ToList();
            return;
        }

        public static bool HasEnabledFields()
        {
            //Returns true if at least one field is enabled
            //  false otherwise, which would be the case if this were the very first run
            //  that is, no fields enabled yet
            if (EnabledHeaderFields != null)
                return true;
            else
                return false;
        }

        public static bool HasCommonFields()
        {
            //Returns true if at least one field is enabled
            //  false otherwise, which would be the case if this were the very first run
            //  that is, no fields enabled yet
            if (CommonHeaderFields != null)
                return true;
            else
                return false;
        }

        public static void AddEnabledField(string field)
        {
            //Adds an entry to the enabled field list
            EnabledHeaderFields.Add(field);
            EnabledHeaderFields = EnabledHeaderFields.Distinct().ToList();
            FitsFielder.SaveFielder();
        }

        public static void RemoveEnabledField(string field)
        {
            //Adds an entry to the common field list
            if (IsEnabledField(field))
                EnabledHeaderFields.Remove(field);
            FitsFielder.SaveFielder();
        }

        public static List<string> GetAllCommonFields()
        {
            //Returns all the common fields
            return CommonHeaderFields;
        }

        public static void EnableAllCommonFields()
        {
            //Creates a new list of enabled fields from the common list
            EnabledHeaderFields = CommonHeaderFields;
        }

        public static bool IsCommonField(string field)
        {
            //Returns true if field is in common list
            // otherwise false
            if (CommonHeaderFields.Contains(field))
                return true;
            else
                return false;
        }

        public static bool IsColumnHeader(string field)
        {
            //Returns true if field is in column header list
            // otherwise false
            if (ColumnHeaders.Contains(field))
                return true;
            else
                return false;
        }

        public static bool IsEnabledField(string field)
        {
            //Returns true if field is in enabled list
            // otherwise false
            if (EnabledHeaderFields.Contains(field))
                return true;
            else
                return false;
        }

        public static void AddColumnHeader(string field)
        {
            //adds to column header list 
            ColumnHeaders.Add(field);
            ColumnHeaders = ColumnHeaders.Distinct().ToList();
        }

        public static int GetColumnIndex(string field)
        {
            //Returns list index of field in column list (adds field if not already member)
            //if (ColumnHeaders.IndexOf(field) != -1)
            //    ColumnHeaders.Add(field);
            return ColumnHeaders.IndexOf(field);
        }

        public static void SaveFielder()
        {
            //Saves fielder arrays to settings
            StringCollection strCol = new StringCollection();
            strCol.AddRange(CommonHeaderFields.Distinct().ToArray());
            Properties.Settings.Default.CommonFields = strCol;
            StringCollection strEnb = new StringCollection();
            strEnb.AddRange(EnabledHeaderFields.Distinct().ToArray());
            Properties.Settings.Default.EnabledFields = strEnb;
            Properties.Settings.Default.Save();
            return;
        }

        public static void LoadEnabledFields()
        {
            var enf = Properties.Settings.Default.EnabledFields;
            if (enf != null)
                EnabledHeaderFields = enf.Cast<string>().ToList();
        }

        public static void LoadCommonFields() => CommonHeaderFields = Properties.Settings.Default.CommonFields.Cast<string>().ToList();

    }
}
