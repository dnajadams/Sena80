using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace SeNA80
{
    /// <summary>
    /// DataBaseIO
    /// two methods included
    /// Method 1: ReadUsers - will read the User DataBase File and store each of the strings in 
    /// globals.Users.  The maximum number of Users that can be in the database is 500.
    /// Method 2: WriteUsers - Used ONLY by the Configuration Program to add/delete users from
    /// the database
    /// NOTES: 1) The data is stored in an encrypted binary, Base-64, format...
    /// </summary>
    public class DataBaseIO
    {
        static BinaryReader binRead;
        static BinaryWriter binWrite;

        public static bool ReadUsers()
        {
            //string AppDataPath = Application.CommonAppDataPath;
            //int index = AppDataPath.IndexOf("Ltd.");
            //string newApp = AppDataPath.Substring(0, index + 4);
            string passpath = "c:\\SeNA\\SeNA80sec.bin";
            
            FileStream fs;
            //now write the file
            try
            {
                //open the file for writing
                fs = new FileStream(passpath, FileMode.Open);
            }
            catch (IOException x)
            {
                MessageBox.Show("The following error occured opening the security file\n\n" + x.ToString(),
                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Cannot open the file. - " + passpath + "  reason - " + x.ToString());
                return false;
            }
            try
            {
                int i = 0;
                //read all of the the encrypted strings, one per line
                binRead = new BinaryReader(fs);
                while (fs.Length != fs.Position)
                {
                    string read = binRead.ReadString();
                    if (!DES.isValidBase64(read))
                    {
                        MessageBox.Show(read, "Not Valid Base-64");
                        read = DES.Decript(read.TrimStart());
                    }
                    else
                        read = DES.Decript(read.Trim());  //decrypt each string

                    globals.Users[i] = read.TrimStart('+');
                    i++;
                    if(i == 500)
                    {
                        MessageBox.Show("Maximum Number of Users Reached!\n\nThe maximum allowed is 500", "Exceeded Maximum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        binRead.Close(); //close the reader and stream and return
                        fs.Close();
                        return false;
                    }
                }

            }
            catch (IOException x)
            {
                MessageBox.Show("The following error occured writing to the security file\n\n" + x.ToString(),
                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Cannot write to file. Error -" + x.ToString());
                return false;
            }
            //close binary reader and filestream
            binRead.Close();
            fs.Close();
            return true;
        }

        public static bool WriteFinalUsers(string[] LocUsers)
        {
            //string AppDataPath = Application.CommonAppDataPath;
            //int index = AppDataPath.IndexOf("Ltd.");
            //string newApp = AppDataPath.Substring(0, index + 4);
            string passpath = "c:\\SeNA\\SeNA80sec.bin";

            int len = LocUsers.Length;
            //first open the file for writing
            //now write the file
            try
            {
                //open the file for writing
                binWrite = new BinaryWriter(new FileStream(passpath, FileMode.Create)); //kill the old file, creat a new one
            }
            catch (IOException x)
            {
                MessageBox.Show("The following error occured creating the security file\n\n" + x.ToString(),
                    "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Cannot create the file. - " + passpath + "  reason - " + x.ToString());
                return false;
            }
            //it will try to write all 500 lines
            for (int t = 0; t <= len; t++)
            {
                if (!string.IsNullOrEmpty(LocUsers[t]) && LocUsers[t].Length > 16)
                {
                    //first encrypt the line
                    string towrite = DES.Encript(LocUsers[t].Trim());

                    //make sure it is a valid Base-64 string going in
                    if (!DES.isValidBase64(towrite))
                        MessageBox.Show(LocUsers[t] + "- Does not convert to secure form");

                    //now write the line
                    try
                    {
                        //write the encrypted strings
                        binWrite.Write(towrite);
                    }
                    catch (IOException x)
                    {
                        MessageBox.Show("The following error occured writing to the security file\n\n" + x.ToString(),
                           "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine("Cannot write to file. Error -" + x.ToString());
                        return false;
                    }
                }
                else //assume we've reached the end of the user list
                    break;
            }
            binWrite.Close();

            return true;
        }
        public static bool WriteInitUsers()
        {
            BinaryWriter binWrite;

            //string AppDataPath = Application.CommonAppDataPath;
            //int index = AppDataPath.IndexOf("Ltd.");
            //string newApp = AppDataPath.Substring(0, index + 4);
            string passpath = "c:\\SeNA\\SeNA80sec.bin";

            //four basic users, encrypt the strings
            string l1 = DES.Encript("Administrator,Administrator,Administrator,false");
            string l2 = DES.Encript("Service,Sena2018,Administrator,false");
            string l4 = DES.Encript("Advanced User,password, User 1,true, 511");
            string l3 = DES.Encript("Basic User,password,User 2,true, 492");

            //now write the file
            try
            {
                //open the file for writing
                    binWrite = new BinaryWriter(new FileStream(passpath, FileMode.Create));
            }
            catch (IOException x)
            {
                MessageBox.Show("The following error occured creating the security file\n\n" + x.ToString(),
                    "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Cannot create the file. - " + passpath + "  reason - " + x.ToString());
                return false;
            }
            try
            {
                //write the encrypted strings
                binWrite.Write(l1);
                binWrite.Write(l2);
                binWrite.Write(l3);
                binWrite.Write(l4);
            }
            catch (IOException x)
            {
                MessageBox.Show("The following error occured writing to the security file\n\n" + x.ToString(),
                    "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Cannot write to file. Error -" + x.ToString());
                return false;
            }
            binWrite.Close();

            //store the data in the user array
            globals.Users[0] = "Administrator, Administrator,Administrator, false";
            globals.Users[1] = "Service, Sena2018,Administrator,false";
            globals.Users[2] = "Advanced User,password,User 1,true, 511";
            globals.Users[3] = "Basic User,password,User 2,true, 511";

            return true;
        }
        public static bool IsBase64(string base64String)
        {

            if (base64String == null || base64String.Length == 0)
                return false;
            if (base64String.Length % 4 != 0)
                return false;
            if (base64String.Contains(" ") || base64String.Contains("\t"))
                return false;
            if (base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception x)
            {
                Debug.WriteLine("Not Valid Base-64 String" + x.ToString());
                // Handle the exception
            }
            return false;
        }

    }

    public class UserManagement
    {
        public class BLL
        {
            private static string DB_Path = "DB/xmlFile1.xml";

            public static string[]   Query_User()
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(DB_Path);

                XmlElement root = doc.DocumentElement;   //获取根节点  
                XmlNodeList personNodes = root.GetElementsByTagName("User"); //获取Person子节点集合  

                string[] UserList = new string[personNodes.Count];

                int i = 0;
                foreach (XmlNode node in personNodes)
                {
                    string id = ((XmlElement)node).GetAttribute("ID");
                    UserList[i] = id;
                        i++;
                }
                return UserList;
            }

            public static Model.UserInfo Query_UserInfo(string UID)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(DB_Path);

                XmlElement root = doc.DocumentElement;   //获取根节点  
                XmlNodeList personNodes = root.GetElementsByTagName("User"); //获取Person子节点集合  

                Model.UserInfo Info = new Model.UserInfo();

                foreach (XmlNode node in personNodes)
                {
                    string id = ((XmlElement)node).GetAttribute("ID");

                    if (id == UID)
                    {
                        string PWD =DES.Decript(  ((XmlElement)node).GetElementsByTagName("Password")[0].InnerText);
                        string ActiveStatus = ((XmlElement)node).GetElementsByTagName("ActiveStatus")[0].InnerText;


                        Info.UserID = id;
                        Info.UserPWD = PWD;
                        Info.UserType = id;
                        Info.UserStatus = bool.Parse(ActiveStatus);
                        break;
                    }
                }
                return Info;       
            }

            public static bool UserLoginCheck(string UID, string PWD)
            {
                Model.UserInfo Info= Query_UserInfo(UID);

                if(Info.UserID==null)
                { return false; }

                if (Info.UserPWD == PWD)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static string UserTypeCheck(string UID)
            {
                Model.UserInfo Info = Query_UserInfo(UID);

                if (Info.UserID == null)
                { return null; }

                return Info.UserType;
            }
            public static bool UserStatusCheck(string UID)
            {

                Model.UserInfo Info = Query_UserInfo(UID);

                if (Info.UserID == null)
                { return false; }

                return Info.UserStatus;
            }

            public static void Modify_UserInfo(string UID, string PWD, bool ActiveStatus)
            {

                XmlDocument doc = new XmlDocument();

                doc.Load(DB_Path);    

                XmlElement root = doc.DocumentElement;   

                XmlElement selectEle = (XmlElement)root.SelectSingleNode("/UserLogin/User[@ID='"+UID+"']");

                ((XmlElement)selectEle.GetElementsByTagName("Password")[0]).InnerText = DES.Encript(PWD);
                ((XmlElement)selectEle.GetElementsByTagName("ActiveStatus")[0]).InnerText = ActiveStatus.ToString();

                doc.Save(DB_Path);
            }



        }

        public class Model
        {
            public  class UserInfo
            {
                public  string UserID = null;
                public  string UserPWD = null;
                public  string UserType = null;
                public  bool UserStatus = false;
            }
        }
    }

    public class DES
    {
       
        public static byte[] key = new byte[] { 8, 12, 50, 15, 2, 4, 83, 92 };
        public static byte[] iv = new byte[] { 25, 37, 16, 40, 77, 37, 77, 27 };
        public static byte[] key1 = new byte[] { 84, 62, 48, 61, 92, 7, 9, 20 };
        public static byte[] iv1 = new byte[] { 21, 29, 23, 31, 24, 14, 27, 87 };
        public static byte[] key2 = new byte[] { 45, 58, 88, 37, 50, 22, 92, 27 };
        public static byte[] iv2 = new byte[] { 79, 78, 91, 36, 79, 80, 29, 47 };

        public static byte[][] akey = new byte[][] { key, key1, key2 };
        public static byte[][] aiv = new byte[][] { iv, iv1, iv2 };
        public static string Encript(string cText)
        {
            bool bGoodString = false;


            string NewCText = String.Empty;
            try
            {
                byte[] bText = Encoding.ASCII.GetBytes(cText.Trim());
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream MS = new MemoryStream();

                CryptoStream CS = new CryptoStream(MS, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                CS.Write(bText, 0, bText.Length);
                CS.FlushFinalBlock();
                NewCText = ((Convert.ToBase64String(MS.ToArray())).Replace("+", "%2B"));

                bGoodString = isValidBase64(NewCText);
                //most of the time it is good
                if (bGoodString)
                {
                    return NewCText;
                }
                else
                {
                    cText = '+' + cText;//pad in the front
                    while(!bGoodString)
                    { 
                        bText = Encoding.ASCII.GetBytes(cText.Trim());
                        provider = new DESCryptoServiceProvider();
                        MS = new MemoryStream();

                        CS = new CryptoStream(MS, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                        CS.Write(bText, 0, bText.Length);
                        CS.FlushFinalBlock();
                        NewCText = ((Convert.ToBase64String(MS.ToArray())).Replace("+", "%2B"));

                        bGoodString = isValidBase64(NewCText);
                        if(bGoodString)
                        {
                            //MessageBox.Show("Good Conversion", cText);
                            return NewCText;
                        }

                        cText = '+' + cText;//pad in the front
                    }
                }
                return NewCText;
            }
            catch
            {
                return "";
            }
        }
        // Valid Base 64 characters
        private static Char[] Base64Chars = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };
        public static Boolean isValidBase64(String value)
        {

            // The quickest test. If the value is null or is equal to 0 it is not base64
            // Base64 string's length is always divisible by four, i.e. 8, 16, 20 etc. 
            // If it is not you can return false. Quite effective
            // Further, if it meets the above criterias, then test for spaces.
            // If it contains spaces, it is not base64
            if (value == null || value.Length == 0)
                return false;
            if (value.Length % 4 != 0)
                return false;
            if (value.Contains(" ") || value.Contains("\t"))
                return false;
            if (value.Contains("\r") || value.Contains("\n"))
                return false;

            // 98% of all non base64 values are invalidated by this time.
            var index = value.Length - 1;

            // if there is padding step back
            if (value[index] == '=')
                index--;

            // if there are two padding chars step back a second time
            if (value[index] == '=')
                index--;

            // Now traverse over characters
            // You should note that I'm not creating any copy of the existing strings, 
            // assuming that they may be quite large
            for (var i = 0; i <= index; i++)
                // If any of the character is not from the allowed list
                if (!Base64Chars.Contains(value[i]))
                    // return false
                    return false;

            // If we got here, then the value is a valid base64 string
            return true;
        }
       
        public static string Decript(string cText)
        {
            try
            {
                byte[] bText = Convert.FromBase64String(cText.Replace(" ", "+"));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream MS = new MemoryStream();
                CryptoStream CS = new CryptoStream(MS, provider.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                CS.Write(bText, 0, bText.Length);
                CS.FlushFinalBlock();
                return Encoding.ASCII.GetString(MS.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }
}
