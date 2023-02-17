using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiSfCuim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DirectoryArmController : ControllerBase
    {
        private readonly IConfiguration _config;
        public DirectoryArmController (IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{reference}")]
        public object Get(string reference)
        {

            // Variables 
            string path = _config.GetValue<string>("serverSettings:PathDirectory") + reference;
            dynamic msg;
            List<string> images = new();
            try
            {
                // Comprobar si existe el directorio.
                if (Directory.Exists(path))
                {
                    //Se listan las fotos y el archivo .txt
                    string[] patterns = { "jpg", "png", "JPEG", "txt" };
                    IEnumerable<string> image = Directory.GetFiles(path, "*.*").Where(file => patterns.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)));

                    foreach (var item in image)
                    {
                        string extension = Path.GetExtension(item)[1..];
                        if (extension == "txt")
                        {
                            string fecha = Path.GetFileNameWithoutExtension(item);
                            string anio = fecha.Substring(0, 4);
                            string mes = fecha.Substring(4, 2);
                            string dia = fecha.Substring(6, 2);


                            images.Add("Las fotos se subieron/actualizaron el día : " + dia + "/" + mes + "/" + anio);
                        }
                        else
                        {
                            byte[] text = System.IO.File.ReadAllBytes(item);
                            string text2 = Convert.ToBase64String(text);
                            images.Add(text2);
                        }

                    }

                    return images;

                }
               // msg = $"Aún no hay fotos para la referencia {Arg}";

                return JsonConvert.SerializeObject($"Aún no hay fotos para la referencia {reference}");
            }
            catch (Exception e)
            {
                msg = $"The process failed: {0}" + e.ToString();

                return (msg);
            }
            finally { }


        }


        [HttpPost("{reference}")]

        public object Post([FromBody] dynamic images, string reference)
        {
            try
            {

                string path = _config.GetValue<string>("serverSettings:PathDirectory");
                string imageB64;
                string referencePath = reference;
                int i = 1;

                //Deserializar el Array Json
                string imageString = images.ToString();
                var dates = JsonConvert.DeserializeObject<dynamic>(imageString);

                //Path con la referencia
                var folderPath = Path.Combine(path, referencePath);

                // Si el directorio no existe se crea y se guardan las fotos 
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    foreach (string date in dates)
                    {
                        //Substring para obtener el path de la imagen
                        imageB64 = date[(date.IndexOf(",") + 1)..];
                        System.IO.File.WriteAllBytes(Path.Combine(folderPath, $"{referencePath}({i}).jpg"), Convert.FromBase64String(imageB64));
                        i++;
                    }
                    //Se agrega el TXT
                    string fecha = DateTime.Now.ToString("yyyyMMdd");
                    System.IO.File.Create(folderPath + @"\" + fecha + ".txt").Close();

                    return Ok(JsonConvert.SerializeObject("Las fotos fueron guardadas"));
                }

                // si el directorio existe, se listan las fotos para agregar en el orden que corresponde, y agregar el nuevo txt de fecha.   
                else
                {
                    //se lista los archivos del directorio. 
                    string[] patterns = { "jpg", "png", "JPEG", "txt" };
                    IEnumerable<string> image = Directory.GetFiles(folderPath, "*.*").Where(file => patterns.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)));

                    //Se elimina el TXT fecha y se actualiza el index con la cantidad de imagenes en el directorio
                    foreach (var item in image)
                    {
                        if (Path.GetExtension(item)[1..] == "txt")
                        {
                            System.IO.File.Delete(item);
                        }
                        else
                        {
                            i++;
                        }

                    }
                    // Se agregan las nuevas fotos. 
                    foreach (string date in dates)
                    {
                        //Substring para obtener el path de la imagen
                        imageB64 = date[(date.IndexOf(",") + 1)..];
                        System.IO.File.WriteAllBytes(Path.Combine(folderPath, $"{referencePath}({i}).jpg"), Convert.FromBase64String(imageB64));
                        i++;
                    }
                    //Se agrega el nuevo TXT
                    string fecha = DateTime.Now.ToString("yyyyMMdd");
                    System.IO.File.Create (folderPath+@"\"+fecha+".txt").Close();
                    
                }

                return Ok(JsonConvert.SerializeObject("Las fotos fueron guardadas"));

            }
            catch (Exception e)
            {
                var msg = $"The process failed: {0}" + e.ToString();

                return (msg);
            }
            finally 
            {
                
            }
        }

    }
}
