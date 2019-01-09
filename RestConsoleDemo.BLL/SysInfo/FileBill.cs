using RestConsoleDemo.BLL.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
     public static class FileBill
    {

         public static string UpLoadFile(Stream stream,string DirName, string No)
         {
             string str = string.Empty;
             string filename = string.Empty;
             try
             {
                 if (string.IsNullOrEmpty(DirName))
                 {
                     throw new Exception("文件夹不能为空!");
                 }
                 if (string.IsNullOrEmpty(No))
                 {
                     throw new Exception("单号不能为空!");
                 }
             string FilePath = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
             using (MemoryStream ms = new MemoryStream())
             {

                 stream.CopyTo(ms);
                 ms.Position = 0;
                 Encoding encoding = System.Text.Encoding.UTF8;
                 StreamReader sr = new StreamReader(ms, encoding);
                 var position = 0;
                 //首行
                 var firstline = sr.ReadLine();
                 position += encoding.GetBytes(firstline).Length + 2;
                 var line = sr.ReadLine();
                 string fileInfo = line.ToString();
                 Regex reg = new Regex("filename=\"(.+)\"");
                 Match match = reg.Match(fileInfo);
                 filename = match.Groups[1].Value;
                 while (line != null)
                 {
                     position += encoding.GetBytes(line).Length + 2;
                     if (line == "")
                         break;
                     line = sr.ReadLine();
                 }
                 ms.Position = position;
                 //截除末行
                 ms.SetLength(ms.Length - firstline.Length - 6);
                 var uploadStream = new MemoryStream();
                 ms.CopyTo(uploadStream);
                 uploadStream.Position = 0;
                 string AppDataPath = string.Concat(FilePath, "\\", DirName, "\\", No);
                 if (!Directory.Exists(AppDataPath))
                 {
                     DirectoryInfo floder = Directory.CreateDirectory(AppDataPath);


                 }

                 string filepath = string.Concat(AppDataPath, "\\", filename);


                 using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                 {
                     byte[] buff = uploadStream.ToArray();
                     fs.Write(buff, 0, buff.Length);
                 }

             }
             str = ResponseHelper.ResponseMsg("1", "保存成功", string.Concat("\\", DirName, "\\", No, "\\", filename));
             return str;


             }
             catch (Exception ex)
             {
                 return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
             }
         }
    }
}
