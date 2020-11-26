using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;


/// <summary>
/// 通用工具包（Util Package）
/// </summary>
public class UtilPackage
{

    /// <summary>
    /// 获取根目录或虚拟目录名称,如 ‘/Website/’
    /// </summary>
    public static string SystemVirtualDirectory
    {
        get
        {
            //AppDomainAppVirtualPath不依赖请求，更保险
            string strVirtualDirectory = HttpRuntime.AppDomainAppVirtualPath;
            //不是根目录是在虚拟目录后加‘/’
            if (strVirtualDirectory != "/")
                strVirtualDirectory += "/";
            return strVirtualDirectory;
        }
    }

    #region 时间戳

    /// <summary>
    /// 转为时间戳。Unix时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数。
    /// </summary>
    /// <param name="dateTime">时间</param>
    /// <returns></returns>
    public static long ToTimeStamp(DateTime dateTime)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
        return (long)(dateTime - startTime).TotalSeconds;
        // return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }

    /// <summary>
    /// 转为时间
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <returns></returns>
    public static System.DateTime ToDateTime(long timeStamp)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
        return startTime.AddSeconds(timeStamp);
    }

    #endregion

    /// <summary>
    /// 四舍五入
    /// </summary>
    /// <param name="num">数值</param>
    /// <param name="places">四舍五入后的小数位数</param>
    /// <returns></returns>
    public static double Round(double num, int places = 0)
    {
        return (double)Math.Round((decimal)num, places, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// 获取最大值
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int MaxInt(params int[] num)
    {
        int a = num[0];
        for (int i = 0; i < num.Length; i++)
        {
            if (a < num[i])
            {
                a = num[i];
            }
        }
        return a;
    }

    /// <summary>
    /// 获取最小值
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int MinInt(params int[] num)
    {
        int a = num[0];
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] < a)
            {
                a = num[i];
            }
        }
        return a;
    }

    /// <summary>
    /// 获取文件大小
    /// </summary>
    /// <param name="fileSize">文件大小。单位：字节（B）</param>
    /// <param name="fileSizeUnit">文件大小单位</param>
    /// <param name="places">结果四舍五入后的小数位数</param>
    /// <returns></returns>
    public static double GetFileSize(long fileSize, FileSizeUnit fileSizeUnit = FileSizeUnit.GB, int places = 3)
    {
        double value = 0;
        int _fileSizeUnit = (int)fileSizeUnit;
        //Bit
        if (_fileSizeUnit == -1)
        {
            value = fileSize * 8;
        }
        //
        else if (fileSize <= 0)
        {
            value = 0;
        }
        else
        {
            value = Round((fileSize / Math.Pow(1024, _fileSizeUnit)), places);
        }

        return value;

    }

    /// <summary>
    /// 获取文件大小
    /// </summary>
    /// <param name="fileSize">文件大小。单位：字节（B）</param>
    /// <param name="places">结果四舍五入后的小数位数</param>
    /// <returns></returns>
    public static string GetFileSizeString(long fileSize, int places = 2)
    {

        string[] sizes = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB", "BB", "NB", "DB", "CB" };
        int k = 1024, i = 0;
        double value = 0;
        if (fileSize <= 0)
        {
            value = 0;
        }
        else
        {
            i = Convert.ToInt32(Math.Floor(Math.Log(fileSize) / Math.Log(k)));
            value = Round((fileSize / Math.Pow(k, i)), places);
        }
        return value + ' ' + sizes[i];

    }

    #region MD5

    /// <summary>
    /// MD5 加密 加密后密文为小写
    /// </summary>
    /// <param name="strByte">待加密的字节</param>
    /// <param name="is16Bit">是否16位</param>
    /// <returns></returns>
    public static string Md5(byte[] strByte, bool is16Bit = false)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] md5Bytes = md5.ComputeHash(strByte);
        string md5Str = is16Bit ? BitConverter.ToString(md5Bytes, 4, 8) : BitConverter.ToString(md5Bytes);
        return md5Str.Replace("-", "").ToLower();
    }
    /// <summary>
    /// MD5 32位加密 加密后密文为小写
    /// </summary>
    /// <param name="str"></param>
    /// <param name="is16Bit">是否16位</param>
    /// <returns></returns>
    public static string Md5(string str, bool is16Bit = false)
    {
        return Md5(System.Text.Encoding.UTF8.GetBytes(str), is16Bit);
    }
    /// <summary>
    ///MD5 16位加密 加密后密文为小写
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Md5To16Bit(string str)
    {
        return Md5(System.Text.Encoding.UTF8.GetBytes(str), true);
    }
    #endregion

    /// <summary>
    /// 随机数字
    /// </summary>
    /// <param name="min">最小数值（包含）</param>
    /// <param name="max">最大数值（包含）</param>
    /// <returns></returns>
    public static int RandomNumber(int min, int max)
    {
        Random r = new Random(Guid.NewGuid().GetHashCode());
        return r.Next(min, max + 1);
    }

    /// <summary>
    /// 实现取k取余法【返回数组的index=0位置是最高位】
    /// </summary>
    /// <param name="num"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static double[] ckqy(double num, int k)
    {
        if (k == 0)
        {
            return new double[] { };
        }

        List<double> y = new List<double>();
        while (num > 0)
        {
            var s = Math.Floor(num / k);
            var _y = num % k;
            if (s == 0)
            {
                y.Add(num);
                num = 0;
            }
            else
            {
                y.Add(_y);
                num = s;
            }
        }

        return y.ToArray().Reverse().ToArray();
    }




    #region 检查sql参数值是否包含敏感字符（反注入）

    private const string SqlKeyWord = @" insert into | delete from | count(| drop table | update | truncate | asc( | mid( | char( | xp_cmdshell | exec master | netlocalgroup administrators | net user | or | and |;";
    /// <summary>
    /// 检查sql参数值是否包含敏感字符（反注入）
    /// </summary>
    /// <param name="value"></param>
    public string CheckSqlParameterValue(string value, string name)
    {
        string[] SqlKeyWords = SqlKeyWord.Split(new char[] { '|' });
        bool isHasKeyWord = false;
        string _keyword = "";
        foreach (string keyword in SqlKeyWords)
        {
            isHasKeyWord = value.Contains(keyword);
            if (isHasKeyWord)
            {
                _keyword = keyword;
                break;
            }
        }

        if (isHasKeyWord)
        {
            throw new Exception(name + "值包含SQL敏感字符：\"" + _keyword + "\"。");
        }

        return value;

    }

    #endregion


    /// <summary>
    /// 类转换器
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public static class ClassConvertor<TIn, TOut>
    {

        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            foreach (var item in typeof(TOut).GetFields())
            {
                MemberExpression field = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, field);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut Convert(TIn tIn)
        {
            return cache(tIn);
        }

    }

    /// <summary>
    /// 对象深拷贝
    /// </summary>
    /// <param name="obj">被复制对象</param>
    /// <returns>新对象</returns>
    public static object CopyOjbect(object obj)
    {
        if (obj == null)
        {
            return null;
        }
        object targetDeepCopyObj;
        Type targetType = obj.GetType();
        //值类型  
        if (targetType.IsValueType == true)
        {
            targetDeepCopyObj = obj;
        }
        //引用类型   
        else
        {
            targetDeepCopyObj = Activator.CreateInstance(targetType);   //创建引用对象   
            MemberInfo[] memberCollection = obj.GetType().GetMembers();

            foreach (MemberInfo member in memberCollection)
            {
                //拷贝字段
                if (member.MemberType == MemberTypes.Field)
                {
                    FieldInfo field = (FieldInfo)member;
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue is ICloneable)
                    {
                        field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                    }
                    else
                    {
                        field.SetValue(targetDeepCopyObj, CopyOjbect(fieldValue));
                    }

                }
                //拷贝属性
                else if (member.MemberType == MemberTypes.Property)
                {
                    PropertyInfo myProperty = (PropertyInfo)member;
                    MethodInfo info = myProperty.GetSetMethod(false);
                    if (info != null)
                    {
                        try
                        {
                            object propertyValue = myProperty.GetValue(obj, null);
                            if (propertyValue is ICloneable)
                            {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue), null);
                            }
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
        }
        return targetDeepCopyObj;
    }
    /// <summary>
    /// 拷贝对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="obj">被复制对象</param>
    /// <returns>新对象</returns>
    public static T Copy<T>(T obj) where T : class
    {
        return CopyOjbect(obj) as T;
    }
    /// <summary>
    /// 获取文件扩展名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFileExtension(string fileName)
    {
        try
        {
            return System.IO.Path.GetExtension(fileName);
        }
        catch { return null; }
    }

    /// <summary>
    /// 更新对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="par"></param>
    /// <returns></returns>
    public static T UdateObject<T>(T obj, Dictionary<string, object> par) where T : class
    {
        Type type = obj.GetType();
        foreach (KeyValuePair<string, object> kvp in par)
        {
            PropertyInfo pInfo = type.GetProperty(kvp.Key);
            if (pInfo != null)
            {
                try
                {
                    var pValue = kvp.Value;
                    if (!(Convert.IsDBNull(pValue) || pValue == null))
                    {
                        object pVal = null;
                        if (pInfo.PropertyType.ToString().Contains("System.Nullable"))
                        {
                            pVal = Convert.ChangeType(pValue, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                        else
                        {
                            pVal = Convert.ChangeType(pValue, pInfo.PropertyType);
                        }
                        pInfo.SetValue(obj, pVal);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("属性[" + pInfo.Name + "]转换出错," + ex.Message, ex);
                }
            }


            FieldInfo fInfo = type.GetField(kvp.Key);
            if (fInfo != null)
            {
                try
                {
                    var fValue = kvp.Value;
                    if (!(Convert.IsDBNull(fValue) || fValue == null))
                    {
                        object fVal = null;
                        if (fInfo.FieldType.ToString().Contains("System.Nullable"))
                        {
                            fVal = Convert.ChangeType(fValue, Nullable.GetUnderlyingType(fInfo.FieldType));
                        }
                        else
                        {
                            fVal = Convert.ChangeType(fValue, fInfo.FieldType);
                        }
                        fInfo.SetValue(obj, fVal);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("字段[" + fInfo.Name + "]转换出错," + ex.Message, ex);
                }
            }
        }



        return obj;
    }

}

#region  enum


/// <summary>
/// 文件大小单位
/// </summary>
public enum FileSizeUnit
{
    /// <summary>
    /// 比特
    /// </summary>
    b = -1,
    /// <summary>
    /// 字节
    /// </summary>
    B = 0,
    /// <summary>
    /// 千字节
    /// </summary>
    KB = 1,
    /// <summary>
    /// 兆
    /// </summary>
    MB = 2,
    /// <summary>
    /// 吉
    /// </summary>
    GB = 3,
    /// <summary>
    /// 太
    /// </summary>
    TB = 4,
    /// <summary>
    /// 拍
    /// </summary>
    PB = 5,
    /// <summary>
    /// 艾
    /// </summary>
    EB = 6,
    /// <summary>
    /// 泽
    /// </summary>
    ZB = 7,
    /// <summary>
    /// 尧
    /// </summary>
    YB = 8,
    /// <summary>
    /// 千亿亿亿字节
    /// </summary>
    BB = 9,
    /// <summary>
    /// 百万亿亿亿字节
    /// </summary>
    NB = 10,
    /// <summary>
    /// 十亿亿亿亿字节
    /// </summary>
    DB = 11,
    /// <summary>
    /// 万亿亿亿亿字节
    /// </summary>
    CB = 12
}


#endregion

