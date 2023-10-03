using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    public class QueryStringSerializer
    {
        private NameValueCollection nvc;
        public Func<string, string> NameConverter { get; set; }

        public string Serialize(object request)
        {
            nvc = HttpUtility.ParseQueryString(string.Empty);

            if (request is not null)
                Go(request);
            return nvc.ToString();
        }

        private void Go(object obj, string name = null)
        {
            string eName;
            if (obj is null)
                return;

            Type type = obj.GetType();
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
                type = underlyingType;

            if (type.IsInterface)
                return;

            if (type.IsArray)
            {
                if (type.GetArrayRank() > 1)
                    throw new NotSupportedException();

                int i = 0;
                foreach (var item in (Array)obj)
                {
                    eName = GetArrayKey(name, i++);
                    Go(item, eName);
                }
                return;
            }

            if (type.IsEnum)
            {
                nvc.Add(name, GetEnumValue(obj));
                return;
            }

            switch (Type.GetTypeCode(type))
            {

                case TypeCode.Boolean:
                    nvc.Add(name, BooleanToStr((bool)obj));
                    break;
                case TypeCode.Char:
                    nvc.Add(name, CharToStr((char)obj));
                    break;
                case TypeCode.SByte:
                    nvc.Add(name, SByteToStr((sbyte)obj));
                    break;
                case TypeCode.Byte:
                    nvc.Add(name, ByteToStr((byte)obj));
                    break;
                case TypeCode.Int16:
                    nvc.Add(name, Int16ToStr((short)obj));
                    break;
                case TypeCode.UInt16:
                    nvc.Add(name, UInt16ToStr((ushort)obj));
                    break;
                case TypeCode.Int32:
                    nvc.Add(name, Int32ToStr((int)obj));
                    break;
                case TypeCode.UInt32:
                    nvc.Add(name, UInt32ToStr((uint)obj));
                    break;
                case TypeCode.Int64:
                    nvc.Add(name, Int64ToStr((long)obj));
                    break;
                case TypeCode.UInt64:
                    nvc.Add(name, UInt64ToStr((ulong)obj));
                    break;
                case TypeCode.Single:
                    nvc.Add(name, SingleToStr((float)obj));
                    break;
                case TypeCode.Double:
                    nvc.Add(name, DoubleToStr((double)obj));
                    break;
                case TypeCode.Decimal:
                    nvc.Add(name, DecimalToStr((decimal)obj));
                    break;
                case TypeCode.DateTime:
                    nvc.Add(name, DateTimeToStr((DateTime)obj));
                    break;
                case TypeCode.String:
                    nvc.Add(name, (string)obj);
                    break;

                default:
                    switch (type.FullName)
                    {
                        case "System.Guid":
                            nvc.Add(name, GuidToStr((Guid)obj));
                            break;

                        case "System.DateTimeOffset":
                            nvc.Add(name, DateTimeOffsetToStr((DateTimeOffset)obj));
                            break;

                        case "System.TimeSpan":
                            nvc.Add(name, TimeSpanToStr((TimeSpan)obj));
                            break;

                        case "System.DateOnly":
                            nvc.Add(name, DateOnlyToStr((DateOnly)obj));
                            break;

                        case "System.TimeOnly":
                            nvc.Add(name, TimeOnlyToStr((TimeOnly)obj));
                            break;

                        default:
                            if (type.GetCustomAttributes<IsQueryStringAttribute>() is not null)
                            {
                                var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                                foreach ( var prop in props)
                                {
                                    eName = NameConverter(prop.Name);
                                    if (!string.IsNullOrEmpty(name))
                                        eName = $"{name}[{eName}]";
                                    
                                    Go(prop.GetValue(obj), eName);
                                }
                            }
                            break;
                    }
                    break;
            }
        }


        protected virtual string BooleanToStr(bool value) =>
            value ? "true" : "false";
        protected virtual string CharToStr(char value) =>
            value.ToString();
        protected virtual string SByteToStr(sbyte value) => value.ToString();
        protected virtual string ByteToStr(byte value) => value.ToString();
        protected virtual string Int16ToStr(short value) => value.ToString();
        protected virtual string UInt16ToStr(ushort value) => value.ToString();
        protected virtual string Int32ToStr(int value) => value.ToString();
        protected virtual string UInt32ToStr(uint value) => value.ToString();
        protected virtual string Int64ToStr(long value) => value.ToString();
        protected virtual string UInt64ToStr(ulong value) => value.ToString();
        protected virtual string SingleToStr(float value) => value.ToString();
        protected virtual string DoubleToStr(double value) => value.ToString();
        protected virtual string DecimalToStr(decimal value) => value.ToString();
        protected virtual string DateTimeToStr(DateTime value) => value.ToString();
        protected virtual string GuidToStr(Guid value) => value.ToString();
        protected virtual string DateTimeOffsetToStr(DateTimeOffset value) => value.ToString();
        protected virtual string TimeSpanToStr(TimeSpan value) => value.ToString();
        protected virtual string DateOnlyToStr(DateOnly value) => value.ToString("yyyy-MM-dd");
        protected virtual string TimeOnlyToStr(TimeOnly value) => value.ToString();
        protected virtual string GetArrayKey(string name, int key)=> $"{name}[{key}]";

        protected virtual string GetEnumValue(object value)
        {
            var original = value.ToString();
            var enumMemberValue = value
                .GetType()
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;

            return string.IsNullOrEmpty(enumMemberValue) ? original : enumMemberValue;
        }

    }
}
