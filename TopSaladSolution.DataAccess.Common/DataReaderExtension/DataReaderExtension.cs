using System.Data.Common;
using System.Reflection;

namespace TopSaladSolution.DataAccess.Common.DataReaderExtension
{
    /// <summary title="Data Reader Extends">
    /// Creator : Chung Thành Phước
    /// Desc    : Auto Mapping object to list
    /// </summary>
    public static class DataReaderExtension
    {

        /// <summary>
        /// Maps the data from the DbDataReader to a list of objects of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to map to.</typeparam>
        /// <param name="dr">The DbDataReader containing the data to map.</param>
        /// <returns>A list of objects of type T mapped from the DbDataReader data, or null if the DbDataReader is null or has no rows.</returns>
        public static List<T> MapToList<T>(this DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }
    }
}
