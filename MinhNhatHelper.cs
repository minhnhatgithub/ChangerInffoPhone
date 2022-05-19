using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace RegisterFaceBook
{
    public class MinhNhatHelper
    {
        internal static void DeleteForderSaveFile(string SerialNo)
        {
            if (File.Exists(Application.StartupPath + "\\InfoDevices\\cckinfo_" + SerialNo + ".txt"))
            {
                File.Delete(Application.StartupPath + "\\InfoDevices\\cckinfo_" + SerialNo + ".txt");
            }
        }
        public static bool PushInfoFile(string SerialNo, string Destination)
        {
            string cmd = " push " + Destination;
            Form1.ExecuteCMD(SerialNo, cmd);
            return true;
        }
        public static bool StartPushFileChangerInfo(string device)
        {
            try
            {
                PushInfoFile(device, string.Concat(new string[]
          {
                "\"",
                Application.StartupPath,
                "\\InfoDevices\\cckinfo_",
                device,
                ".txt\" /sdcard/ntcinfo_",
                device,
                ".txt"
          }));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void StartServiceFake(string SerialNo)
        {
            string cmd = "shell su -c am startservice com.ntc.just4fone/.NTCController";
            Form1.ExecuteCMD(SerialNo, cmd);
        }
        public static void StopServiceFake(string SerialNo)
        {
            string cmd = "shell su -c am stopservice com.ntc.just4fone/.NTCController";
            Form1.ExecuteCMD(SerialNo, cmd);
        }
        public static int GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            double value = new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
            return Convert.ToInt32(value);
        }
        public static Dictionary<string, string> GenernateDeviceInfo()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                if (!File.Exists(Application.StartupPath + "/InfoDevices/DataDevice/deviceData.txt"))
                {
                    new WebClient().DownloadFile("https://cck.vn//Download/Utils/deviceData.txt", Application.StartupPath + "/InfoDevices/DataDevice/deviceData.txt");
                }
                string[] array = File.ReadAllLines(Application.StartupPath + "/InfoDevices/DataDevice/deviceData.txt");
                dictionary.Add("Manufacture", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[0]);
                dictionary.Add("Brand", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[1]);
                dictionary.Add("Model", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[2]);
                dictionary.Add("Board", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[3]);
                dictionary.Add("Device", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[4]);
                dictionary.Add("long", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[5]);
                dictionary.Add("lat", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[6]);
                dictionary.Add("alt", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[7]);
                dictionary.Add("speed", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[8]);
                List<string> list = new List<string>
                {
                    "Vinaphone",
                    "Mobifone",
                    "Viettel"
                };
                dictionary.Add("Carrier", list[new Random().Next(0, list.Count)]);
                dictionary.Add("CarrierCode", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[10]);
                dictionary.Add("ContryCode", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[11]);
                dictionary.Add("OSName", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[12]);
                dictionary.Add("OSArch", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[13]);
                dictionary.Add("OSVersion", array[GetRandomNumberInRange(0.0, (double)(array.Length - 1))].Split(new char[]
                {
                    '|'
                })[14]);
            }
            catch
            {
            }
            return dictionary;
        }
        public static string GenerateAndroidSerial()
        {
            string source = "0123456789abcdef";
            string text = "";
            Random random = new Random();
            while (text.Length < 12)
            {
                text += source.ElementAt(random.Next(16));
            }
            return text;
        }
        public static string GenerateAndroidID()
        {
            string source = "0123456789abcdef";
            string text = "";
            Random random = new Random();
            while (text.Length < 16)
            {
                text += source.ElementAt(random.Next(16));
            }
            return text;
        }
        private static Func<byte, string> func_0;
        private static string e(byte x)
        {
            return string.Format("{0}:", x.ToString("X2"));
        }
        private static string b(string digitString)
        {
            int i = 0;
            int num = 0;
            int num2 = digitString.Length - 1;
            while (i < digitString.Length)
            {
                int num3 = Convert.ToInt32(digitString.ElementAt(num2 - i));
                if (i % 2 == 0)
                {
                    num3 *= 2;
                    if (num3 > 9)
                    {
                        num3 -= 9;
                    }
                }
                num += num3;
                i++;
            }
            return Convert.ToString(num * 9 % 10);
        }
        public static string GetRandomMacAddress()
        {
            Random random = new Random();
            byte[] array = new byte[6];
            random.NextBytes(array);
            IEnumerable<byte> source = array;
            if (func_0 == null)
            {
                func_0 = new Func<byte, string>(e);
            }
            string text = string.Concat(source.Select(func_0).ToArray<string>());
            return text.TrimEnd(new char[]
            {
                ':'
            });
        }
        public static string randomIMEI(string originIMEI = "150520063080968624")
        {
            string text = "";
            if (originIMEI.Length > 8)
            {
                text = originIMEI.Substring(0, 8);
            }
            else if (originIMEI.Length == 8)
            {
                text = originIMEI;
            }
            string text2 = text;
            Random random = new Random();
            while (text2.Length < 14)
            {
                text2 += random.Next(10).ToString();
            }
            return text2 + b(text2);
        }
        public static string randomPhoneNumber()
        {
            string source = "0123456789";
            string text = "09";
            Random random = new Random();
            while (text.Length < 9)
            {
                text += source.ElementAt(random.Next(10));
            }
            return text;
        }
        public static string randomSimSerial(string originSimSerial = "150520063080968624")
        {
            string text = "";
            if (originSimSerial.Length > 8)
            {
                text = originSimSerial.Substring(0, 8);
            }
            else if (originSimSerial.Length == 8)
            {
                text = originSimSerial;
            }
            string text2 = text;
            Random random = new Random();
            while (text2.Length < 14)
            {
                text2 += random.Next(10).ToString();
            }
            return text2 + b(text2);
        }
        public static Dictionary<string, string> ProduceTxtAddNew(string deviceId, string uid = "")
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string text = string.Empty;
            string filedata = string.Empty;
            try
            {
                Dictionary<string, string> dictionary2 = MinhNhatHelper.GenernateDeviceInfo();
                string text2 = MinhNhatHelper.GenerateAndroidSerial();
                string text3 = MinhNhatHelper.GenerateAndroidID();
                string randomMacAddress = MinhNhatHelper.GetRandomMacAddress();
                string text4 = MinhNhatHelper.randomIMEI("150520063080968624");
                string text5 = MinhNhatHelper.randomIMEI("150520063080968624");
                string text6 = MinhNhatHelper.randomPhoneNumber();
                string text7 = MinhNhatHelper.randomSimSerial("150520063080968624");
                text = string.Concat(new string[]
                {
                    text,
                    dictionary2["Manufacture"],
                    "\n",
                    dictionary2["Brand"],
                    "\n",
                    dictionary2["Model"],
                    "\n",
                    dictionary2["Board"],
                    "\n",
                    dictionary2["Device"],
                    "\n",
                    text3,
                    "\n",
                    text2,
                    "\n",
                    dictionary2["long"],
                    "\n",
                    dictionary2["lat"],
                    "\n",
                    dictionary2["alt"],
                    "\n",
                    dictionary2["speed"],
                    "\n",
                    randomMacAddress,
                    "\n",
                    Guid.NewGuid().ToString("N").Substring(0, new Random().Next(5, 20)),
                    "\n",
                    randomMacAddress,
                    "\n",
                    text4,
                    "\n",
                    text5,
                    "\n",
                    text6,
                    "\n",
                    text7,
                    "\n",
                    dictionary2["Carrier"],
                    "\n",
                    dictionary2["CarrierCode"],
                    "\n",
                    dictionary2["ContryCode"],
                    "\n",
                    dictionary2["OSName"],
                    "\n",
                    dictionary2["OSArch"],
                    "\n",
                    dictionary2["OSVersion"]
                });
                filedata = string.Concat(new string[]
              {
                    filedata,
                    deviceId,
                    "|",
                    dictionary2["Manufacture"],
                    "|",
                    dictionary2["Brand"],
                    "|",
                    dictionary2["Model"],
                    "|",
                    dictionary2["Board"],
                    "|",
                    text3,
                    "|",
                    text2,
                    "|",
                    randomMacAddress,
                    "|",
                    text4,
                    "|",
                    text5,
                    "|",
                    text6,
                    "|",
                    text7,
                    "|",
                    dictionary2["Carrier"],
                    "|",
                    dictionary2["CarrierCode"],
                    "|",
                    dictionary2["ContryCode"],
                    "|",
                    dictionary2["OSName"],
                    "|",
                    dictionary2["OSArch"],
                    "|",
                    dictionary2["OSVersion"],
                    "|",
                    DateTime.Now.ToString("dddd,MM/dd/yyyy HH:mm:ss")
              });
                dictionary.Add("Manufacture", dictionary2["Manufacture"]);
                dictionary.Add("Brand", dictionary2["Brand"]);
                dictionary.Add("Model", dictionary2["Model"]);
                dictionary.Add("Board", dictionary2["Board"]);
                dictionary.Add("Device", dictionary2["Device"]);
                dictionary.Add("AndroidID", text3);
                dictionary.Add("AndroidSerial", text2);
                dictionary.Add("long", dictionary2["long"]);
                dictionary.Add("lat", dictionary2["lat"]);
                dictionary.Add("alt", dictionary2["alt"]);
                dictionary.Add("speed", dictionary2["speed"]);
                dictionary.Add("WifiMac", randomMacAddress);
                dictionary.Add("WifiName", "MyFiod");
                dictionary.Add("BSSID", randomMacAddress);
                dictionary.Add("IMEI", text4);
                dictionary.Add("IMSI", text5);
                dictionary.Add("PhoneNumber", text6);
                dictionary.Add("SimSerial", text7);
                dictionary.Add("Carrier", dictionary2["Carrier"]);
                dictionary.Add("CarrierCode", dictionary2["CarrierCode"]);
                dictionary.Add("ContryCode", dictionary2["ContryCode"]);
                dictionary.Add("OSName", dictionary2["OSName"]);
                dictionary.Add("OSArch", dictionary2["OSArch"]);
                dictionary.Add("OSVersion", dictionary2["OSVersion"]);
                SQLiteUtils sqliteUtils = new SQLiteUtils();
                DataRow dataRow = sqliteUtils.dataRow(uid);
                if (dataRow != null && uid != "" && dataRow["brand"] != "" && dataRow["brand"].ToString().Length > 50)
                {
                    text = dataRow["brand"].ToString();
                }
                else
                {
                    sqliteUtils.ExecuteQuery(string.Format("Update Account Set Device='{2}', Brand='{0}' where id='{1}'", text, uid, dictionary2["Manufacture"]));
                }
                using (FileStream fileStream = new FileStream(Application.StartupPath + "\\InfoDevices\\cckinfo_" + deviceId + ".txt", FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(text);
                    }
                }
                using (var streamWriter = new StreamWriter("InfoDevices\\DataDevice\\InfoDevice.txt",true))
                {
                    streamWriter.WriteLine(filedata);
                }
            }
            catch
            {
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return dictionary;
        }
        public static string ExecuteCMD(string index, string cmd)
        {
            string result = "";
            try
            {
                string startupPath = Application.StartupPath;
                string text = "";
                try
                {
                    Process process = new Process();
                    cmd = string.Concat(new string[]
                    {
                        " -s ",
                        index,
                        " ",
                        cmd
                    });
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = startupPath + "\\adb.exe",
                        Arguments = cmd,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    };
                    process.Start();
                    StreamReader standardOutput = process.StandardOutput;
                    text = standardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception)
                {
                }
                result = text;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static void SetStoragePermission(string SerialNo)
        {
            ExecuteCMD(SerialNo, "shell su -c pm grant com.facebook.katana android.permission.READ_EXTERNAL_STORAGE");
            ExecuteCMD(SerialNo, "shell su -c pm grant com.facebook.katana android.permission.WRITE_EXTERNAL_STORAGE");
        }
        public static string validChar = "_abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string UnicodeToKoDauAndGach(string s)
        {
            string result;
            if (s != null)
            {
                s = HttpUtility.HtmlDecode(s).ToLower();
                string text = string.Empty;
                for (int i = 0; i < s.Length; i++)
                {
                    int num = "_0987654321abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZàáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ-Ð ".IndexOf(s[i].ToString());
                    if (num >= 0)
                    {
                        text += "_0987654321abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZaaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU-D-"[num];
                    }
                    else
                    {
                        text = (text ?? "");
                    }
                }
                s = "";
                for (int i = 0; i < text.Length; i++)
                {
                    int num = validChar.IndexOf(text[i].ToString());
                    if (num >= 0)
                    {
                        s += validChar[num];
                    }
                }
                s = s.Trim();
                result = s;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        public static void StartPushFile(string deviceID, string path)
        {
            try
            {
                SetStoragePermission(deviceID);
                Thread.Sleep(500);
                List<string> list = new List<string>();
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    if (files.Length <= 0)
                    {

                    }
                    list = (from arg in files orderby Guid.NewGuid() select arg).Take(1).ToList<string>();
                }
                string text = ExecuteCMD(deviceID, " shell ls sdcard/Pictures");
                List<string> list2 = (from x in text.Split(new string[]
     {
                Environment.NewLine
     }, StringSplitOptions.None)
                                      where !string.IsNullOrEmpty(x)
                                      select x).ToList<string>();
                foreach (string str in list2)
                {
                    ExecuteCMD(deviceID, " shell rm -rf /sdcard/Pictures/" + str);
                    ExecuteCMD(deviceID, " shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///mnt/sdcard/Pictures/" + str);
                }
                foreach (string text2 in list)
                {
                    string str2 = UnicodeToKoDauAndGach(Path.GetFileName(text2)) + Path.GetExtension(text2);
                    ExecuteCMD(deviceID, string.Format(" push \"{0}\" \"{1}\"", text2, "/sdcard/Pictures/0" + str2));
                    ExecuteCMD(deviceID, " shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///mnt/sdcard/Pictures/0" + str2);
                    Thread.Sleep(1000);
                }
            }
            catch
            {

            }
        }
    }
}
