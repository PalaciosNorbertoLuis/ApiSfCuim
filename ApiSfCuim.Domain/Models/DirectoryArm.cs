using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public class DirectoryArm
    {
        public DirectoryArm() { }


        public static object Search (string Arg)
        {
            // Specify the directory you want to manipulate.
            string path = @"c:\Test\" + Arg;

            string[] msg;
            List<string> images = new();
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    string[] patterns = {"jpg","png","JPEG", "txt" }; //,

                    IEnumerable<string> image = Directory.GetFiles(path, "*.*").Where(file => patterns.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)));

                    foreach (var item in image)
                    {
                        string extension = Path.GetExtension(item)[1..];
                        if (extension == "txt")
                        {
                            string fecha = Path.GetFileNameWithoutExtension(item);
                            string anio = fecha.Substring(0,4);
                            string mes = fecha.Substring(4,2);
                            string dia = fecha.Substring(6,2);


                            images.Add("Las fotos se subieron/actualizaron el día : " +dia+"/"+mes+"/"+anio);
                        }
                        else
                        {
                            byte[] text = File.ReadAllBytes(item);
                            string text2 = Convert.ToBase64String(text);
                            images.Add(text2);
                        }

                    }

                    return images;

                }
                msg = new[] { $"Aun no se subieron fotos para la referencia {Arg}." };
                return msg;
            }
            catch (Exception e)
            {
                msg = new[] { "The process failed: {0}" + e.ToString() };

                return (msg);
            }
            finally { }


        }
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