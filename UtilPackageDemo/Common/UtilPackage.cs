using System;
using System.Collections.Generic;
using System.Linq;
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
    public static string GetFileSizeString(long fileSize, int places = 2) {
        
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
    /// <param name="len">密文字符串长度。16/32</param>
    /// <returns></returns>
    public static string Md5(byte[] strByte,int len=32)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] md5Bytes = md5.ComputeHash(strByte);
        if (len == 16)
        {
            //将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
            string md5_16Str = BitConverter.ToString(md5Bytes, 4, 8);
            return md5_16Str.Replace("-", "").ToLower();
        }
        else
        {
            string md5Str = "";
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                md5Str += md5Bytes[i].ToString("x");
            }
            return md5Str;
        }
    }
    /// <summary>
    /// MD5 32位加密 加密后密文为小写
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Md5(string str)
    {
        return Md5(System.Text.Encoding.UTF8.GetBytes(str), 32);
    }
    /// <summary>
    ///MD5 16位加密 加密后密文为小写
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Md5To16Bit(string str)
    {
        return Md5(System.Text.Encoding.UTF8.GetBytes(str), 16);
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
        if (k == 0) {
            return new double[] { };
        }

        List<double> y = new List<double>();
        while (num > 0)
        {
            var s = Math.Floor(num / k);
            var _y = num % k;
            //console.log(num, s, _y);
            //console.log(num + " ÷ " + k + " ＝ " + s + "···" + _y);
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



}

/// <summary>
/// 文件大小单位
/// </summary>
public enum FileSizeUnit {
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


