using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;  


namespace CRUDJSON
{
    
    public class Program
    {
        public string jsonFile = @"C:\Users\pjuli\source\repos\CRUDJSON\CRUDJSON\user.json";
        public void Agregarperson()  
        {  
            Console.WriteLine("Digita el ID: ");  
            var studentid = Console.ReadLine();  
            Console.WriteLine("\nDigita su nombre: ");
            var studentname = Console.ReadLine();  
  
            var newStudent = "{ 'studentid': " + studentid + ", 'studentname': '" + studentname + "'}";  
            try  
            {  
                var json = File.ReadAllText(jsonFile);  
                var jsonObj = JObject.Parse(json);  
                var studentsArrary = jsonObj.GetValue("experiences") as JArray;  
                var newStud = JObject.Parse(newStudent);  
                studentsArrary.Add(newStud);  

                 jsonObj["students"] = studentsArrary;  
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);  
                File.WriteAllText(jsonFile, newJsonResult);
                Console.WriteLine("Informacion guardada");
            }  
            catch (Exception ex)  
            {  
                Console.WriteLine("Add Error : " + ex.Message.ToString());  
            }  
        }  
        public void ActualizarPerson()  
        {  
            string json = File.ReadAllText(jsonFile);  
  
            try  
            {  
                var jObject = JObject.Parse(json);  
                JArray studentsArrary = (JArray)jObject["students"];  
                Console.Write("Digita el id para actualizar/modificar : ");  
                var studentId = Convert.ToInt32(Console.ReadLine());  
  
                if (studentId > 0)  
                {  
                    Console.Write("Digita su nombre : ");  
                    var studentName = Convert.ToString(Console.ReadLine());  
  
                    foreach (var student in studentsArrary.Where(obj => obj["studentid"].Value<int>() == studentId))  
                    {  
                        student["studentname"] = !string.IsNullOrEmpty(studentName) ? studentName : "";  
                    }  
  
                    jObject["students"] = studentsArrary;  
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);  
                    File.WriteAllText(jsonFile, output);
                    Console.WriteLine("Datos actualizados");
                }  
                else  
                {  
                    Console.Write("Id invalido");  
                    ActualizarPerson();  
                }  
            }  
            catch (Exception ex)  
            {  
  
                Console.WriteLine("Update Error : " + ex.Message.ToString());  
            }  
        }  

        public void EliminarPerson()  
        {  
            var json = File.ReadAllText(jsonFile);  
            try  
            {  
                var jObject = JObject.Parse(json);  
                JArray studentsArrary = (JArray)jObject["students"];  
                Console.Write("Digita el id para eliminar : ");  
                var studentId = Convert.ToInt32(Console.ReadLine());  
  
                if (studentId > 0)  
                {  
                    var studentName = string.Empty;  
                    var studentToDeleted = studentsArrary.FirstOrDefault(obj => obj["studentid"].Value<int>() == studentId);  
  
                    studentsArrary.Remove(studentToDeleted);  
  
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);  
                    File.WriteAllText(jsonFile, output);  
                    Console.WriteLine("Dato eliminado");
                }  
                else  
                {  
                    Console.Write("Id invalido");  
                    ActualizarPerson();  
                }  
            }  
            catch (Exception)  
            {  
  
                throw;  
            }  
        }  

        public void MostrarPerson()  
        {  
            var json = File.ReadAllText(jsonFile);  
            try  
            {  
                var jObject = JObject.Parse(json);  
  
                if (jObject != null)  
                {  
                    Console.WriteLine("ID :" + jObject["id"].ToString());  
                    Console.WriteLine("Name :" + jObject["name"].ToString());  
  
                    var address = jObject["address"];  
                    Console.WriteLine("Street :" + address["street"].ToString());  
                    Console.WriteLine("City :" + address["city"].ToString());  
                    Console.WriteLine("Zipcode :" + address["zipcode"]);  
                    JArray studentsArrary = (JArray)jObject["students"];  
                    if (studentsArrary != null)  
                    {  
                        foreach (var item in studentsArrary)  
                        {  
                            Console.WriteLine("Id :" + item["studentid"]);  
                            Console.WriteLine("Name :" + item["studentname"].ToString());  
                        }  
  
                    }  
                    Console.WriteLine("Phone Number :" + jObject["phoneNumber"].ToString());  
                    Console.WriteLine("Role :" + jObject["role"].ToString());  
  
                }  
            }  
            catch (Exception)  
            {  
  
                throw;  
            }  
        }  
  
        static void Main(string[] args)
        {
            Program objProgram = new CRUDJSON.Program();

            Menu start = new Menu();
            start.iniciar();

        }  
       
    }
}
