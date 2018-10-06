using System;
using System.Linq;
using System.Reflection;

namespace Runerne.Instantiation
{
    public class StaticInstanceProvider : IInstanceProvider
    {
        public Type _type;
        public Type DefinedType
        {
            get
            {
                if (_definedType == null)
                    _definedType = GetDefinedType();
                return _definedType;
            }
        }
        public string MemberName { get; }

        private object _instance;
        private Type _definedType = null;

        public StaticInstanceProvider(Type type, string memberName)
        {
            _type = type;
            MemberName = memberName;
        }

        public object GetInstance()
        {
            if (_instance == null)
                _instance = CreateInstance();
            return _instance;
        }

        private Type GetDefinedType()
        {
            var info = GetMemberInfo();
            {
                var fieldInfo = info as FieldInfo;
                if (fieldInfo != null)
                    return fieldInfo.FieldType;
            }

            {
                var propertyInfo = info as PropertyInfo;
                if (propertyInfo != null)
                    return propertyInfo.PropertyType;
            }
            return null;
        }

        private object CreateInstance()
        {
            var info = GetMemberInfo();
            {
                var fieldInfo = info as FieldInfo;
                if (fieldInfo != null)
                    return fieldInfo.GetValue(null);
            }

            {
                var propertyInfo = info as PropertyInfo;
                if (propertyInfo != null)
                    return propertyInfo.GetValue(null);
            }
            return null;
        }

        private MemberInfo GetMemberInfo()
        {
            var fieldInfo = _type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(o => o.Name == MemberName).SingleOrDefault();
            if (fieldInfo != null)
                return fieldInfo;

            var propertyInfo = _type.GetProperties(BindingFlags.Static | BindingFlags.Public).Where(o => o.Name == MemberName).SingleOrDefault();
            if (propertyInfo != null)
                return propertyInfo;

            throw new RIException($"No public static field by the name '{MemberName}' was found in the type named: '{DefinedType.FullName}'.");
        }
    }
}
