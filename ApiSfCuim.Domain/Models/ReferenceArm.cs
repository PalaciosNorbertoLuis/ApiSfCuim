using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public class ReferenceArm
    {
        public ReferenceArm() { }

        public static string Directorios(string Arg)
        {


            // Specify the directory you want to manipulate.
            string path = @"c:\Test\" + Arg;
            string msg;
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    msg = ($"El directorio {Arg} ya existe");
                    //Console.Beep();
                    return (msg);
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                msg = ($"El directorio se creo correctamente el {Directory.GetCreationTime(path)}.");
                return (msg);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                //  Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                msg = "The process failed: {0}" + e.ToString();

                return (msg);
            }
            finally { }

        }
    }
}