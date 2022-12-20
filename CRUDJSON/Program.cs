using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
                var studentsArrary = jsonObj.GetValue("students") as JArray;  
                var newStud = JObject.Parse(newStudent);  
                studentsArrary.Add(newStud);  

                 jsonObj["students"] = studentsArrary;  
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);  
                File.WriteAllText(jsonFile, newJsonResult);
                Console.Write("\n");
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
                    Console.Write("\n");
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
            var jObject = JObject.Parse(json);
            JArray studentsArrary = (JArray)jObject["students"];
            if (lista_vacia() == true)
            {
                Console.WriteLine("La lista esta vacia");
            }
            else
            {
                Console.Write("Digita el id para eliminar : ");
                var studentId = Convert.ToInt32(Console.ReadLine());

                var studentName = string.Empty;
                var studentToDeleted = studentsArrary.FirstOrDefault(obj => obj["studentid"].Value<int>() == studentId);

                studentsArrary.Remove(studentToDeleted);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, output);
                Console.WriteLine("Dato eliminado");
            }
 
        }
        public bool lista_vacia()
        {
            var json = File.ReadAllText(jsonFile);
            var jObject = JObject.Parse(json);
            if (jObject.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MostrarPerson()  
        {
            var json = File.ReadAllText(jsonFile);
            var jObject = JObject.Parse(json);
            if (lista_vacia()==true)
            {
                Console.WriteLine("La lista esta vacia");
            }
            else
            {
                Console.WriteLine("|----Lista---------------------|");
                Console.WriteLine("| ID :" + jObject["id"].ToString());
                Console.WriteLine("| Universidad :" + jObject["Universidad"].ToString());

                var Dirreccion = jObject["Dirreccion"];
                Console.WriteLine("| Calle :" + Dirreccion["Calle"].ToString());
                Console.WriteLine("| Ciudad :" + Dirreccion["Ciudad"].ToString());
                Console.WriteLine("| codigoPostal :" + Dirreccion["codigoPostal"]);
                Console.WriteLine("|------Estudiantes: -----------");
                JArray studentsArrary = (JArray)jObject["students"];
                if (studentsArrary != null)
                {
                    foreach (var item in studentsArrary)
                    {
                        Console.WriteLine("| Id : {0} | nombre : {1} ", item["studentid"], item["studentname"]);
                        //Console.WriteLine("Id :" + item["studentid"]);
                        //Console.WriteLine("Name :" + item["studentname"].ToString());
                    }

                }
            }
            
        }
        public class Dirreccion
        {
            public string Calle { get; set; }
            public string Ciudad { get; set; }
            public int codigoPostal { get; set; }
        }

        public class Root
        {
            public int id { get; set; }
            public string Universidad { get; set; }
            public Dirreccion Dirreccion { get; set; }
            public List<Student> students { get; set; }
        }

        public class Student
        {
            public int studentid { get; set; }
            public string studentname { get; set; }
        }
        public void buscar()
        {
            if (lista_vacia() == true)
            {
                Console.WriteLine("La lista esta vacia");
            }
            else
            {
                int buscar;
                string link = @"C:\Users\pjuli\source\repos\CRUDJSON\CRUDJSON\user.json";
                WebRequest request = WebRequest.Create(link);
                WebResponse response = request.GetResponse();
                Console.WriteLine("Digite su id para buscarlo: ");
                buscar=Convert.ToInt32(Console.ReadLine());
               

                using (Stream datastream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(datastream);
                    string responsefromserver = reader.ReadToEnd();

                    Root root = JsonConvert.DeserializeObject<Root>(responsefromserver);

                    foreach (Student s in root.students)
                    {
                        if (buscar ==s.studentid)
                        {
                            Console.WriteLine("-----Dato encontrado: ---");
                            Console.Write("\n");
                            Console.WriteLine("| Id : {0} | nombre : {1} ", s.studentid, s.studentname);
                            //Console.WriteLine("id= " + s.studentid + " Name: " + s.studentname);
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                        
                    }
                }
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
