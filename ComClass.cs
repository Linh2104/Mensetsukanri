//    HL株式会社社用管理システム  
//
//機能：   共通インフォクラス
//作成者：hu
//作成日：
//現在バージョン：0.0.0.1
//最後更新日：2018-06-07
//更新履歴： [理由]                [更新者]     [更新日]    [更新後バージョン]  
//          新規作成　                hu                        0.0.0.1
//          DB接続追加             muhuaizhi    2018-06-07      
//--------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace HL_塾管理
{
    class ComClass
    {
        //会議室(予約登録・一覧)画面WindowSize
        public static System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, 1256, 745);

        /// <summary>
        /// 文字列が全角カタカナかどうかを判定します
        /// </summary>
        /// <param name="target">対象の文字列</param>
        /// <returns>文字列が全角カタカナの場合はtrue、それ以外はfalse</returns>
        public static bool IsFullKatakana(string target)
        {
            return new Regex("^\\p{IsKatakana}+$").IsMatch(target);
        }

        public static int IsKinngaku(String str)
        {
            int i = 0;
            if (str != null && !str.Equals(""))
            {
                if (str.Substring(0, 1).Equals("\\"))
                {
                    str = str.Remove(0, 1);
                    str = str.TrimStart('0');

                    if (str.Equals(""))
                    {
                        return 0;
                    }
                }



                if (str.Substring(0, 1).Equals("-"))
                {
                    str = str.Remove(0, 1);
                }

                char[] sss = str.ToCharArray();

                Array.Reverse(sss);

                if (str.IndexOf(',') >= 0)
                {

                    for (int j = 0; j < str.Count(); j++)
                    {
                        if (((j + 1) % 4) == 0)
                        {
                            if (sss[j] != ',')
                            {
                                i = -1;
                                return i;
                            }
                        }
                        else
                        {
                            if (sss[j] < '0' || sss[j] > '9')
                            {
                                i = -1;
                                return i;
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < str.Count(); j++)
                    {
                        if (sss[j] < '0' || sss[j] > '9')
                        {
                            i = -1;
                            return i;
                        }
                    }
                }
            }
            else
            {
                i = -1;
            }
            return i;
        }

        public static String numToKinngake(String str)
        {
            String sRet = "";
            bool maiFlag = false;

            if (str != null && !str.Equals(""))
            {
                String sTmp = "";

                if (str.Substring(0, 1).Equals("\\"))
                {
                    sTmp = str.Remove(0, 1).Replace(",", "");
                }
                else
                {
                    sTmp = str.Replace(",", "");
                }

                int iOut = 0;
                if (!int.TryParse(sTmp, out iOut))
                {
                    return "";
                }

                sTmp = sTmp.Trim();

                if (sTmp.Equals(""))
                {
                    return "";
                }

                if (sTmp.Substring(0, 1).Equals("-"))
                {
                    sTmp = sTmp.Remove(0, 1);

                    if (sTmp.Equals(""))
                    {
                        return "";
                    }
                    maiFlag = true;
                }

                char[] sss = sTmp.ToCharArray();

                Array.Reverse(sss);

                if (sTmp.IndexOf(',') < 0)
                {
                    for (int j = 0; j < sTmp.Count(); j++)
                    {
                        if ((j % 3) == 0 && j != 0)
                        {
                            sRet = sRet + ',';
                        }

                        sRet = sRet + sss[j];
                    }

                    if (maiFlag)
                    {
                        sRet = sRet + '-';
                    }

                    sRet = sRet + '\\';

                    char[] aaa = sRet.ToCharArray();
                    Array.Reverse(aaa);

                    sRet = new string(aaa);
                }
                else
                {
                    return str;
                }
            }

            return sRet;
        }

        public static Int64 KinngageToNum(String str)
        {
            Int64 iRet = 0;
            String sRet = "";

            if (str == null || str.Equals(""))
            {
                return iRet;
            }

            if (str.Substring(0, 1).Equals("\\"))
            {
                str = str.Remove(0, 1);
            }

            char[] sss = str.ToCharArray();

            for (int j = 0; j < str.Count(); j++)
            {
                if (sss[j] != ',' && sss[j] != '，')
                {
                    sRet = sRet + sss[j];
                }
            }

            if (sRet.Equals(""))
            {
                return iRet;
            }

            try
            {
                iRet = Int64.Parse(sRet);
            }
            catch
            {
                return iRet;
            }

            return iRet;
        }

        public static String KinngakuAdd(String str1, String str2, String str3)
        {
            return KinngakuAdd(KinngakuAdd(str1, str2), str3);
        }

        public static String KinngakuAdd(String str1, String str2)
        {
            String sRet = "";

            if (str1 == null || str1.Equals(""))
            {
                if (str2 != null && !str2.Equals(""))
                {
                    sRet = KinngageToNum(str2).ToString();
                }
            }
            else if (str2 == null || str2.Equals(""))
            {
                sRet = KinngageToNum(str1).ToString();
            }
            else
            {
                sRet = (KinngageToNum(str1) + KinngageToNum(str2)).ToString();
            }

            return sRet;
        }

        public static String KinngakuSub(String str1, String str2)
        {
            String sRet = "";

            if (str1 == null || str1.Equals(""))
            {
                if (str2 != null && !str2.Equals(""))
                {
                    sRet = (0 - KinngageToNum(str2)).ToString();
                }
            }
            else if (str2 == null || str2.Equals(""))
            {
                sRet = KinngageToNum(str1).ToString();
            }
            else
            {
                sRet = (KinngageToNum(str1) - KinngageToNum(str2)).ToString();
            }

            return sRet;
        }

        public static String Kinngaku乗算(String str1, double f2)
        {
            String sRet = "";
            double d1 = f2;

            if (str1 == null || str1.Equals(""))
            {
                sRet = "0";
            }
            else
            {
                sRet = Math.Floor((KinngageToNum(str1) * d1)).ToString();
            }

            return sRet;
        }

        public static String Kinngaku除算(String str1, int f2)
        {
            String sRet = "";

            if (str1 == null || str1.Equals(""))
            {
                sRet = "0";
            }
            else
            {
                sRet = Math.Floor((double)(KinngageToNum(str1) / f2)).ToString();
            }

            return sRet;
        }

        public static bool CheckKinngaku(TextBox sender,object strOut = null)
        {
            String line = ((TextBox)sender).Text;

            if (IsKinngaku(line) >= 0)
            {
                line = KinngageToNum(line).ToString();
            }

            line = line.TrimStart('0');

            if (!line.Equals(""))
            {
                line = numToKinngake(line);

                if (ComClass.IsKinngaku(line) == -1)
                {
                    if (strOut == null)
                    {
                        MessageBox.Show("数字以外を入力しないでください。");
                    }
                    else
                    {
                        ((ToolStripStatusLabel)strOut).Text = "数字以外を入力しないでください。";
                    }
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                    return false;
                }
                else
                {
                    ((TextBox)sender).Text = line;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
            }
            else
            {
                ((TextBox)sender).Text = "\\0";
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }

            return true;
        }

        public static String Kinngeku下一桁切り捨て(String str1)
        {
            return Kinngaku乗算(Kinngaku除算(str1, 10), 10);
        }

        public static bool Getテンプレートファイル(SqlConnection conn, string 会社, ref string ファイル名)
        {
            string sql = string.Format(@"select * from HL_JIMU_テンプレート where 会社 =N'{0}' and テンプレートファイル名 = '{1}'",
            会社, ファイル名);

            SqlCommand com = new SqlCommand(sql, conn);

            SqlDataReader reader = com.ExecuteReader();

            string tmpDir = @"C:\Windows\Temp\";
            ファイル名 = tmpDir + ファイル名;

            if (reader.Read())
            {
                try
                {
                    File.WriteAllBytes(ファイル名, (byte[])reader["テンプレートファイル"]);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            reader.Close();

            return true;

        }

        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgSource, new System.Drawing.Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            //EncoderParameters encoderParams = new EncoderParameters();
            //long[] quality = new long[1];
            //quality[0] = 100;

            //EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //encoderParams.Param[0] = encoderParam;

            imgSource.Dispose();

            return outBmp;
        }

        public static String getStrIP()
        {
            StreamReader sr = new StreamReader("C:\\ProgramData\\TEMP\\IP.txt", Encoding.Default);
            String line = sr.ReadLine();
            sr.Close();

            return line;
        }

        //muhuaizhi add 2018-06-07 start
        /// <summary>
        /// DB接続先
        /// </summary>
        //4階
        //public const string connectionString = "Integrated Security=FALSE; Server=60.73.117.22; Database=oa; Uid=sa; Pwd=hl5566; Connection Timeout=60";
        //6階
        //public const string connectionString = "Integrated Security=FALSE; Server=192.168.3.110; Database=oa; Uid=sa; Pwd=hl5566; Connection Timeout=60";
        public const string connectionString = "Data Source=DESKTOP-R5038PT\\SQLEXPRESS01;Initial Catalog=oa;Integrated Security=True";

        //旧：connectionString = "Data Source=localhost;Integrated Security=True;"; 
        //muhuaizhi add 2018-06-07 end

        //muhuaizhi add 20190205 start
        class 共通PrintDocument : System.Drawing.Printing.PrintDocument
        {            
            public System.Data.DataTable m_dt表の内容 = new System.Data.DataTable();
            /// <summary>
            /// 画面上gridviewの各カラムの広さ
            /// </summary>
            public List<int> m_lstカラム広さ = new List<int>();
            /// <summary>
            /// 画面上gridviewの広さ
            /// </summary>
            public int m_width = 0;
        }
        /// <summary>
        /// 印刷関数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="タイトル"></param>
        /// <param name="lst広さ"></param>
        /// <returns></returns>
        public static bool PrintOut(System.Data.DataTable dt, string タイトル, List<int> lst広さ)
        {
            共通PrintDocument pd = new 共通PrintDocument();

            pd.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;

            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 595, 842);//595×842 1240×1754 2479×3508

            int tw = 0;
            for (int a = 0; a < lst広さ.Count; a++)
            {
                tw += lst広さ[a];
            }

            pd.m_width = tw;

            if (tw > (820))
            {
                pd.DefaultPageSettings.Landscape = true;//紙横
            }
            else
            {
                pd.DefaultPageSettings.Landscape = false;//紙縦
            }

            pd.m_dt表の内容 = dt;
            pd.DocumentName = タイトル;
            pd.m_lstカラム広さ = lst広さ;

            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(MyPrintDocument_PrintPage);

            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.AllowCurrentPage = true;
            //PrintDialog1.PrinterSettings.PrinterName = "SHARP MX-2311FN SPDL2-c"; delete muhuaizhi 20190419
            PrintDialog1.PrinterSettings.PrinterName = "FUJI XEROX DocuCentre-VII C3373";   //add muhuaizhi 20190419

            PrintDialog1.Document = pd;
            PrintDialog1.Document.DefaultPageSettings.Color = false;
            DialogResult result = PrintDialog1.ShowDialog();

            if (result == DialogResult.OK)
            { 
                pd.Print();
            }                

            return true;
        }

        public static Image BufferToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])Buffer.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);
                //設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                //建立副本
                oBitmap = new Bitmap(oImage);
            }
            catch
            {
                throw;
            }
            //return oImage;
            return oBitmap;
        }

        static System.Drawing.Font printFont_タイトル;
        static System.Drawing.Font printFont_内容;
        static System.Drawing.Font printFont_カラムヘッド;
        static float 改行差 = 10f;
        static int 一個漢字 = 11;
        static int その他 = 5;

        /// <summary>
        /// 画面上表のサイズとA4実際サイズ合わせため印刷文字サイズを決める
        /// </summary>
        /// <param name="A4_W"></param>
        /// <param name="gridview_W"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        static float ComforFont(float A4_W,ref float gridview_W,int time = 0)
        {
            if(gridview_W < A4_W)
            {
                return (7 - time);//文字サイズ　7　から縮小
            }
            else
            {
                gridview_W = (gridview_W/10) * 8;//毎回 8/10 の比率で縮小
                time += 1;
                return  ComforFont(A4_W,ref gridview_W, time);
            }            
        }

        /// <summary>
        /// 印刷イベント事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Data.DataTable dt = ((共通PrintDocument)sender).m_dt表の内容;
            List<int> lst = ((共通PrintDocument)sender).m_lstカラム広さ;

            printFont_タイトル = new System.Drawing.Font("宋体", 9, System.Drawing.FontStyle.Bold);
            
            var printColor = System.Drawing.Brushes.Black;
            var pointY = 10f;
            var pointX = 10f;
            float x偏位値 = 1;
            float y偏位値 = 6;
            float セール高さ = 25f;
            float Y差値 = 20f;
            float 内容_Font = 0;

            float tw = ((共通PrintDocument)sender).m_width;               

            if (((共通PrintDocument)sender).DefaultPageSettings.Landscape)
            {
                内容_Font = ComforFont(1170,ref tw);
                pointX = ((1170 - tw) / 2);
            }
            else
            {
                内容_Font = ComforFont(830, ref tw);
                pointX = ((830 - tw) / 2);
            }

            printFont_内容 = new System.Drawing.Font("宋体", 内容_Font, System.Drawing.FontStyle.Regular);
            printFont_カラムヘッド = new System.Drawing.Font("宋体", 内容_Font, System.Drawing.FontStyle.Bold);

            for (int f = 7; f > Convert.ToInt32(内容_Font); f--)//Fontの変更比率により各カラム広さを縮小
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    lst[i] = (lst[i]/10) * 8;
                }
            }

            e.Graphics.DrawString(((共通PrintDocument)sender).DocumentName, printFont_タイトル, printColor, pointX, pointY);

            pointY += Y差値;

            Pen p = new Pen(Color.Black, 1f);

            for (int a = -1; a < dt.Rows.Count; a++)//一行づづ印刷
            {
                int 最大桁数index = 0;
                int セール長 = -1;
                double 折り返し行数 = -1;

                if(a != -1)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {   
                        double CellString長さ = string長さ(dt.Rows[a][c].ToString());
                        double 折り返し行数temp = 0;

                        if (CellString長さ > lst[c])
                        {
                            折り返し行数temp = CellString長さ / Convert.ToDouble(lst[c]);

                            if(折り返し行数temp > 折り返し行数)//一行の中で印刷長さ一番長いのセールを探し出す
                            {
                                折り返し行数 = 折り返し行数temp;
                                最大桁数index = c;
                                セール長 = lst[c];
                            }
                        }
                    }
                }

                if (折り返し行数 != -1)
                {
                    Set_Y(dt.Rows[a][最大桁数index].ToString(), ref セール高さ, セール長);
                }

                for (int b = 0; b < dt.Columns.Count; b++)
                {
                    if (a == -1)
                    {
                        SolidBrush bru = new SolidBrush(Color.LightGray);
                        e.Graphics.FillRectangle(bru, pointX, pointY, lst[b], セール高さ);
                        e.Graphics.DrawRectangle(p, pointX, pointY, lst[b], セール高さ);
                        e.Graphics.DrawString(dt.Columns[b].ColumnName, printFont_カラムヘッド, printColor, pointX + x偏位値, pointY + y偏位値);
                        pointX += lst[b];
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(p, pointX, pointY, lst[b], セール高さ);

                        if (string長さ(dt.Rows[a][b].ToString()) > lst[b])
                        {                               
                            DrawString改行(dt.Rows[a][b].ToString(),ref e , (pointX + x偏位値), (pointY + y偏位値), lst[b]);
                        }
                        else
                        {                                
                            e.Graphics.DrawString(dt.Rows[a][b].ToString(), printFont_内容, printColor, pointX + x偏位値, pointY + y偏位値);
                        }
                            
                        pointX += lst[b];
                    }
                }

                pointY += セール高さ;

                if (((共通PrintDocument)sender).DefaultPageSettings.Landscape)
                {
                    pointX = ((1170 - tw) / 2);
                }
                else
                {
                    pointX = ((830 - tw) / 2);
                }
            }
        }                

        static void Set_Y(string a,ref float Y,int セール長)
        {
            int index = 0;

            if(string長さ(a) < セール長)
            {
                return;
            }
            else
            {
                総合長さ(a, ref index, セール長);

                a = a.Remove(0, index);

                Y += 改行差;

                Set_Y(a, ref Y, セール長);
            }            
        }

        static void DrawString改行(string a,ref System.Drawing.Printing.PrintPageEventArgs e,float X,  float Y,int カラム広さ)
        {
            printFont_内容 = new System.Drawing.Font("宋体", 7, System.Drawing.FontStyle.Regular);
            var printColor = System.Drawing.Brushes.Black;
            string b;
            int index = 0;

            if (!総合長さ(a,ref index, カラム広さ))
            {
                b = a.Substring(0, index);
                e.Graphics.DrawString(b, printFont_内容, printColor, X, Y);

                a = a.Remove(0, index);

                Y += 改行差;
                DrawString改行(a, ref e, X, Y, カラム広さ);
            }
            else
            {
                e.Graphics.DrawString(a, printFont_内容, printColor, X, Y);
                return;
            }

        }
        
        static int string長さ(string a)
        {
            int i = 0;
            foreach(char c in a)
            {
                if(IsKanji(c))
                {
                    i += 一個漢字;
                }
                else if(全角チャック(c.ToString()))
                {
                    i += 一個漢字;
                }
                else
                {
                    i += その他;
                }
            }

            return i;
        }

        static bool 総合長さ(string a, ref int index,int LimtLength)
        {
            int length = 0;
            index = 0;
            foreach (char c in a)
            {
                if(IsKanji(c))
                {
                    length += 一個漢字;
                }
                else if (全角チャック(c.ToString()))
                {
                    length += 一個漢字;
                }
                else
                {
                    length += その他;
                }

                if(length > LimtLength)
                {
                    return false;
                }

                index++;
            }

            return true;
        }

        public static bool IsKanji(char c)
        {
            //CJK統合漢字、CJK互換漢字、CJK統合漢字拡張Aの範囲にあるか調べる
            return ('\u4E00' <= c && c <= '\u9FCF')
                || ('\uF900' <= c && c <= '\uFAFF')
                || ('\u3400' <= c && c <= '\u4DBF');
        }

        static bool 全角チャック(string s)
        {
            Encoding Enc = Encoding.GetEncoding("Shift-JIS");

            if (Enc.GetByteCount(s) == s.Length * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WordReplace(string findtext, string replacetext, bool isAll, _Document document)
        {
            Selection selection = null;

            try
            {
                object dummy = System.Reflection.Missing.Value;
                selection = document.Application.Selection;
                object thisTrue = true;
                object findText = (object)findtext;
                object replaceText = (object)replacetext;

                object thisContinue = WdFindWrap.wdFindContinue;
                object all;

                if (isAll)
                    all = WdReplace.wdReplaceAll;
                else
                    all = dummy;

                selection.Find.Execute(ref findText, ref dummy, ref dummy, ref dummy, ref dummy, ref dummy, ref thisTrue, ref thisContinue, ref dummy, ref replaceText, ref all, ref dummy, ref dummy, ref dummy, ref dummy);

                return selection.Find.Found;
            }
            catch
            {
                return false;
            }
        }
        //muhuaizhi add 20190205 end
    }

    /**
* 祝日かどうか判定できるDateTime拡張クラス
* usage: $holidayDateTime->holiday();
*/
    class HolidayDateTime
    {
        /** 祝日一覧 */
        // 種別：
        //   fixed=日付固定
        //   happy=指定の週の月曜日
        //   spring=春分の日専用
        //   autumn=秋分の日専用
        // 種別, 月, 日or週, 開始年, 終了年, 祝日名
        private static string[,] holidays = new string[,]{        
        {"fixed",   "1",  "1", "1949", "9999", "元日"},
        {"happy",   "1",  "12", "2000", "9999", "成人の日"},
        {"fixed",   "2", "11", "1967", "9999", "建国記念の日"},
        {"spring",  "3",  "20", "1949", "9999", "春分の日"},
        {"fixed",   "4", "29", "2007", "9999", "昭和の日"},
        {"fixed",   "5",  "3", "1949", "9999", "憲法記念日"},
        {"fixed",   "5",  "4", "2007", "9999", "みどりの日"},
        {"fixed",   "5",  "5", "1949", "9999", "こどもの日"},
        {"happy",   "7",  "3", "2003", "9999", "海の日"},
        {"fixed",   "8", "11", "2016", "9999", "山の日"},
        {"autumn",  "9",  "22", "1948", "9999", "秋分の日"},
        {"happy",   "9",  "3", "2003", "9999", "敬老の日"},
        {"happy",  "10",  "2", "2000", "9999", "体育の日"},
        {"fixed",  "11",  "3", "1948", "9999", "文化の日"},
        {"fixed",  "11", "23", "1948", "9999", "勤労感謝の日"},
        {"fixed",  "12", "23", "1989", "9999", "天皇誕生日"}
        };

        public static int DaysInMonthJP(int Year, int Month)
        {
            int Days = DateTime.DaysInMonth(Year, Month);

            switch (Month)
            {
                case 1: { Days -= 2; break; }
                case 2: { Days -= 1; break; }
                case 3: { Days -= 1; break; }
                case 4: { Days -= 1; break; }
                case 5: { Days -= 3; break; }
                case 6: { Days -= 0; break; }
                case 7: { Days -= 1; break; }
                case 8: { Days -= 1; break; }
                case 9: { Days -= 2; break; }
                case 10: { Days -= 1; break; }
                case 11: { Days -= 2; break; }
                case 12: { Days -= 1; break; }
            }

            Days -= Get土日(Year, Month).Count();

            return Days;

        }

        public static int DaysInMonthJP(int Year, int Month, string 開始日, string 終了日)
        {
            DateTime Dt開始日;
            DateTime Dt終了日;

            if (
                (Year != int.Parse(開始日.Split('/')[0]))
                ||
                (Month != int.Parse(開始日.Split('/')[1]))
                )
            {
                Dt開始日 = new DateTime(Year, Month, 1);
            }
            else
            {
                Dt開始日 = new DateTime(int.Parse(開始日.Split('/')[0]), int.Parse(開始日.Split('/')[1]), int.Parse(開始日.Split('/')[2]));
            }

            if (終了日.Trim().Equals("-"))
            {
                Dt終了日 = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            }
            else if (
                (Year != int.Parse(終了日.Split('/')[0]))
                ||
                (Month != int.Parse(終了日.Split('/')[1]))
                )
            {
                Dt終了日 = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            }
            else
            {
                Dt終了日 = new DateTime(int.Parse(終了日.Split('/')[0]), int.Parse(終了日.Split('/')[1]), int.Parse(終了日.Split('/')[2]));
            }

            int days = (Dt終了日 - Dt開始日).Days + 1;

            for (DateTime d = Dt開始日; DateTime.Compare(d, Dt終了日) != 0; d = d.AddDays(1))
            {
                if (
                    (d.DayOfWeek == DayOfWeek.Sunday)
                    ||
                    (d.DayOfWeek == DayOfWeek.Saturday)
                    )
                {
                    days--;
                }

                for (int i = 0; i < holidays.GetLength(0); i++)
                {
                    DateTime Dtholiday = new DateTime(Year, int.Parse(holidays[i, 1]), int.Parse(holidays[i, 2]));
                    if (DateTime.Compare(d, Dtholiday) == 0)
                    {
                        days--;
                        break;
                    }
                }
            }

            return days;

        }

        static DateTime[] Get土日(int y, int m)
        {

            List<DateTime> days = new List<DateTime>();

            for (DateTime d = new DateTime(y, m, 1);
                                    d.Month == m; d = d.AddDays(1))
            {
                if (
                    (d.DayOfWeek == DayOfWeek.Sunday)
                    ||
                    (d.DayOfWeek == DayOfWeek.Saturday)
                    )
                {
                    days.Add(d);
                }
            }
            return days.ToArray();
        }     
    }

    public struct 出勤記録新規追加STRUCT
    {
        public string comboBoxText; //发送给目录窗口所在进程的数据
    }


}
