using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;

namespace Usefull_Tools
{
    /// <summary>
    /// Files
    /// </summary>
    public class Files
    {
        static string setFDir = null;

        /// <summary>
        /// fileDirectory used to save the files
        /// </summary>
        public static string fileDir
        {
            get
            {
                if (setFDir == null)
                {
                    string dir = Environment.CurrentDirectory;
                    string appData = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "HenriCorp", appName);


                    if (Files.HasFolderWritePermission(dir))
                    {
                        setFDir = dir;
                    }
                    else if (Files.HasFolderWritePermission(appData))
                    {
                        setFDir = dir;

                    }
                    else setFDir = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "HenriCorp", appName);

                    Directory.CreateDirectory(Files.fileDir);


                    Files.tempDir = Path.Combine(Files.fileDir, "temp");
                    System.Console.WriteLine("Loggin destination dir : " + setFDir);
                    return setFDir;

                }
                else { return setFDir; }
            }
        }

        /// <summary>
        /// App name
        /// </summary>
        public static string appName
        {
            get { return AppName; }
            set { AppName = value; setFDir = null; }
        }
        static string AppName = "app";



        /// <summary>
        /// temporary file
        /// </summary>
        public static string tempDir { get; set; }

        /// <summary>
        /// Save the specified content in a file
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void save(string[] dir, string fileName, string content)
        {
            saveFile(Path.Combine(dir), fileName, content);
        }


        /// <summary>
        /// Save the specified content in a file
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void saveFile(string dir, string fileName, string content)
        {
            if (Directory.Exists(Path.Combine(fileDir, dir)) == false) { Directory.CreateDirectory(Path.Combine(fileDir, dir)); }

            File.WriteAllText(Path.Combine(fileDir, dir, fileName), content);
        }

        /// <summary>
        /// Determine if the user has write access to the specified folder : not implemented for the moment
        /// </summary>
        /// <param name="destDir"></param>
        /// <returns></returns>
        static bool HasFolderWritePermission(string destDir)
        {
            try { Directory.CreateDirectory(destDir); }
            catch { System.Diagnostics.Debug.WriteLine("Could not create the dir : " + destDir); }

            if (string.IsNullOrEmpty(destDir) || !Directory.Exists(destDir)) return false;
            try
            {
                DirectorySecurity security = Directory.GetAccessControl(destDir);
                SecurityIdentifier users = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                foreach (AuthorizationRule rule in security.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                {
                    if (rule.IdentityReference == users)
                    {
                        FileSystemAccessRule rights = ((FileSystemAccessRule)rule);
                        if (rights.AccessControlType == AccessControlType.Allow)
                        {
                            if (rights.FileSystemRights == (rights.FileSystemRights | FileSystemRights.Modify)) return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // temp files and dir
        /// <summary>
        /// Reyturn a tempr file
        /// </summary>
        /// <param name="extention"></param>
        /// <returns></returns>
        public static string getTempFile(string extention)
        {
            string directory = Path.Combine(tempDir);
            if (Directory.Exists(directory) == false) { Directory.CreateDirectory(directory); }

            string name = "current";
            int version = 0;
            while (File.Exists(Path.Combine(directory, name + version + extention)))
            {
                version++;
            }
            return Path.Combine(directory, name + version + extention);
        }

        /// <summary>
        /// Get a unique path
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileName"></param>
        /// <param name="extention"></param>
        /// <returns></returns>
        public static string getUniquePath(string dir, string fileName, string extention)
        {
            string directory = Path.Combine(fileDir, dir);
            if (Directory.Exists(directory) == false) { Directory.CreateDirectory(directory); }

            int version = 0;
            while (File.Exists(Path.Combine(directory, fileName + version + extention)))
            {
                version++;

            }
            return Path.Combine(directory, fileName + version + extention);
        }


    }
}
