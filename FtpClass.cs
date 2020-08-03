using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
using System.Data;
using System.Net;
using System.Globalization;
using System.Net.Sockets;


namespace HL_塾管理
{
    class FtpClass
    {
        public static readonly string FTPServerIP = "";
        public static readonly string UserName = "";
        public static readonly string Password = "";
        public static readonly string RootDir = "";

        public static FtpStatusCode UploadFileInFTP(string filename)
        {
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse uploadResponse = null;
            FtpWebRequest uploadRequest = null;
            string serverIP;
            string userName;
            string password;
            string uploadurl;

            try
            {
                serverIP = FTPServerIP;  // System.Configuration.ConfigurationManager.AppSettings["FTPServerIP"];
                userName = UserName;     // System.Configuration.ConfigurationManager.AppSettings["UserName"];
                password = Password;     // System.Configuration.ConfigurationManager.AppSettings["Password"];

                uploadurl = "ftp://" + serverIP + RootDir + Path.GetFileName(filename);
                uploadRequest = (FtpWebRequest)WebRequest.Create(uploadurl);
                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                uploadRequest.Proxy = null;
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = userName;
                nc.Password = password;
                uploadRequest.Credentials = nc;
                requestStream = uploadRequest.GetRequestStream();
                fileStream = File.Open(filename, FileMode.Open);

                byte[] buffer = new byte[1024];
                int bytesRead;

                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    requestStream.Write(buffer, 0, bytesRead);
                }

                requestStream.Close();
                uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
                return uploadResponse.StatusCode;
            }
            catch
            {
                //SystemLog.logger(e.InnerException.Message);
            }
            finally
            {
                if (uploadResponse != null)
                {
                    uploadResponse.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (requestStream != null)
                {
                    requestStream.Close();
                }

            }
            return FtpStatusCode.Undefined;
        }

        /// <summary>
        /// 检测目录是否存在，不存在就创建
        /// 只支持更目录开始两级目录
        /// </summary>
        /// <param name="RootDir"></param>      根目录
        /// <param name="pFtpUserID"></param>
        /// <param name="pFtpPW"></param>
        /// <param name="FileSource"></param>   第一级目录
        /// <param name="FileCategory"></param> 第二级目录
        /// <returns></returns>
        public static bool CreateDirectory(string RootDir, string pFtpUserID, string pFtpPW, string FileSource, string FileCategory)
        {
            //检测目录是否存在
            Uri uri = new Uri(RootDir + "/" + FileSource + "/");
            if (!DirectoryIsExist(uri, pFtpUserID, pFtpPW))
            {
                //创建目录
                uri = new Uri(RootDir + "/" + FileSource);
                if (CreateDirectory(uri, pFtpUserID, pFtpPW))
                {
                    //检测下一级目录是否存在
                    uri = new Uri(RootDir + "/" + FileSource + "/" + FileCategory + "/");
                    if (!DirectoryIsExist(uri, pFtpUserID, pFtpPW))
                    {
                        //创建目录
                        uri = new Uri(RootDir + "/" + FileSource + "/" + FileCategory);
                        if (CreateDirectory(uri, pFtpUserID, pFtpPW))
                        {
                            return true;
                        }
                        else 
                        { 
                            return false; 
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else 
                { 
                    return false; 
                }
            }
            else
            {
                //检测下一级目录是否存在
                uri = new Uri(RootDir + "/" + FileSource + "/" + FileCategory + "/");
                if (!DirectoryIsExist(uri, pFtpUserID, pFtpPW))
                {
                    //创建目录
                    uri = new Uri(RootDir + "/" + FileSource + "/" + FileCategory);
                    if (CreateDirectory(uri, pFtpUserID, pFtpPW))
                    {
                        return true;
                    }
                    else 
                    {
                        return false; 
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 检测目录是否存在，不存在就创建
        /// 只支持更目录开始一级目录
        /// </summary>
        /// <param name="RootDir"></param>      根目录
        /// <param name="pFtpUserID"></param>
        /// <param name="pFtpPW"></param>
        /// <param name="FileSource"></param>   第一级目录
        /// <returns></returns>
        public static bool CreateDirectory(string RootDir, string pFtpUserID, string pFtpPW, string FileSource)
        {
            //检测目录是否存在
            Uri uri = new Uri(RootDir + "/" + FileSource + "/");
            if (!DirectoryIsExist(uri, pFtpUserID, pFtpPW))
            {
                //创建目录
                uri = new Uri(RootDir + "/" + FileSource);
                if (CreateDirectory(uri, pFtpUserID, pFtpPW))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// ftp创建目录(创建文件夹)
        /// </summary>
        /// <param name="IP">IP服务地址</param>
        /// <param name="UserName">登陆账号</param>
        /// <param name="UserPass">密码</param>
        /// <param name="FileSource"></param>
        /// <param name="FileCategory"></param>
        /// <returns></returns>
        public static bool CreateDirectory(Uri IP, string UserName, string UserPass)
        {
            try
            {
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(IP);
                FTP.Credentials = new NetworkCredential(UserName, UserPass);
                FTP.Proxy = null;
                FTP.KeepAlive = false;
                FTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FTP.UseBinary = true;
                FtpWebResponse response = FTP.GetResponse() as FtpWebResponse;
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="pFtpServerIP"></param>
        /// <param name="pFtpUserID"></param>
        /// <param name="pFtpPW"></param>
        /// <returns>false不存在，true存在</returns>
        public static bool DirectoryIsExist(Uri pFtpServerIP, string pFtpUserID, string pFtpPW)
        {
            string[] value = GetFileList(pFtpServerIP, pFtpUserID, pFtpPW);
            if (value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string[] GetFileList(Uri pFtpServerIP, string pFtpUserID, string pFtpPW)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(pFtpServerIP);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFtpUserID, pFtpPW);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch
            {
                return null;
            }
        }



        public static int UploadFtp(string filename ,string SumDir = "")
        {
            FtpWebRequest reqFTP = null;
            string serverIP;
            string userName;
            string password;
            string dir;
            string url;

            try
            {
                FileInfo fileInf = new FileInfo(filename);
                serverIP = FTPServerIP;  // System.Configuration.ConfigurationManager.AppSettings["FTPServerIP"];
                userName = UserName;     // System.Configuration.ConfigurationManager.AppSettings["UserName"];
                password = Password;     // System.Configuration.ConfigurationManager.AppSettings["Password"];

                dir = "ftp://" + serverIP + RootDir ;

                if (SumDir.Split('/').Count() == 1)
                {
                    CreateDirectory(dir, userName, password, SumDir);
                }

                if (SumDir.Split('/').Count() == 3)
                {
                    CreateDirectory(dir, userName, password, SumDir.Split('/')[0]);
                    CreateDirectory(dir + SumDir.Split('/')[0], userName, password, SumDir.Split('/')[1], SumDir.Split('/')[2]);
                }


                url = dir + SumDir + "/" + Path.GetFileName(filename);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(userName, password);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInf.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                FileStream fs = fileInf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                Stream strm = reqFTP.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {

                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
                return 0;
            }
            catch
            {
                if (reqFTP != null)
                {
                    reqFTP.Abort();
                }

                return -2;
            }
        }


        public static int DownloadFtp(string filename ,string SumDir = "")
        {
            FtpWebRequest reqFTP;
            string serverIP;
            string userName;
            string password;
            string url;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;


            try
            {
                serverIP = FTPServerIP;  // System.Configuration.ConfigurationManager.AppSettings["FTPServerIP"];
                userName = UserName;     // System.Configuration.ConfigurationManager.AppSettings["UserName"];
                password = Password;     // System.Configuration.ConfigurationManager.AppSettings["Password"];

                if (!SumDir.Equals(""))
                {
                    SumDir = SumDir + "/";
                }

                url = "ftp://" + serverIP + RootDir + SumDir + Path.GetFileName(filename);

                outputStream = new FileStream(filename, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(userName, password);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                return 0;
            }
            catch
            {
                //SystemLog.logger(ex.InnerException.Message);
                return -2;
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                if (outputStream != null)
                {
                    outputStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }

        }

    }
}
