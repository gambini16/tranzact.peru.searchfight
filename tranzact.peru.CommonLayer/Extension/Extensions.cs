using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tranzact.peru.CommonLayer.Extension
{
    public static class StringExtensions
    {
        public static T DeserializeJson<T>(this string json)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<T>(json);
        }
    }

    public static class CollectionExtensions
    {
        public static T MaxValue<T>(this IEnumerable<T> source, Func<T, long> func)
        {
            if (source == null)
                throw new ArgumentException();

            using (var en = source.GetEnumerator())
            {
                if (!en.MoveNext())
                    throw new ArgumentException();

                long max = func(en.Current);
                T maxValue = en.Current;

                while (en.MoveNext())
                {
                    var possible = func(en.Current);

                    if (max < possible)
                    {
                        max = possible;
                        maxValue = en.Current;
                    }
                }
                return maxValue;
            }
        }
    }
}
