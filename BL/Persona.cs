using CURP.Enums;
using CURP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Persona
    {
        public static ML.Result Add(ML.Persona persona)
        {

            ML.Result result = new ML.Result();

            try
            {
                persona.CURP = GenerarCurp(persona);

                using (DL.JSanchezDrSecurityEntities contex = new DL.JSanchezDrSecurityEntities())
                {
                    var query = contex.PersonaAdd(persona.Nombre, persona.ApellidoPaterno, persona.ApellidoMaterno, DateTime.Parse(persona.FechaNacimiento), persona.Sexo, persona.EstadoNacimiento, persona.Telefono, persona.CURP, persona.Direccion.IdDireccion);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;

        }

        public static ML.Result Update(ML.Persona persona)
        {
            ML.Result result = new ML.Result();

            try
            {
                persona.CURP = GenerarCurp(persona);

                using (DL.JSanchezDrSecurityEntities contex = new DL.JSanchezDrSecurityEntities())
                {
                    var query = contex.PersonaUpdate(persona.IdPersona, persona.Nombre, persona.ApellidoPaterno, persona.ApellidoMaterno, DateTime.Parse(persona.FechaNacimiento), persona.Sexo, persona.EstadoNacimiento, persona.Telefono, persona.CURP, persona.Direccion.IdDireccion);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar los datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;

        }

        public static ML.Result Delete(int IdPersona)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JSanchezDrSecurityEntities contex = new DL.JSanchezDrSecurityEntities())
                {
                    var query = contex.PersonaDelete(IdPersona);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar los datos";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JSanchezDrSecurityEntities contex = new DL.JSanchezDrSecurityEntities())
                {
                    var query = contex.PersonaGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Persona persona = new ML.Persona();

                            persona.IdPersona = obj.IdPersona;
                            persona.Nombre = obj.Nombre;
                            persona.ApellidoPaterno = obj.ApellidoPaterno;
                            persona.ApellidoMaterno = obj.ApellidoMaterno;
                            persona.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            persona.Sexo = obj.Sexo;
                            persona.EstadoNacimiento = obj.EstadoNacimiento;
                            persona.Telefono = obj.Telefono;
                            persona.CURP = obj.CURP;

                            persona.Direccion = new ML.Direccion();
                            persona.Direccion.IdDireccion = obj.IdDireccion;
                            persona.Direccion.Calle = obj.Calle;
                            persona.Direccion.NumeroInterior = obj.NumeroInterior;
                            persona.Direccion.NumeroExterior = obj.NumeroExterior;
                            persona.Direccion.Colonia = obj.Colonia;
                            persona.Direccion.Municipio = obj.Municipio;
                            persona.Direccion.Estado = obj.Estado;

                            result.Objects.Add(persona);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(int IdPersona)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JSanchezDrSecurityEntities contex = new DL.JSanchezDrSecurityEntities())
                {
                    var query = contex.PersonaGetById(IdPersona).FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();

                        var obj = query;

                        ML.Persona persona = new ML.Persona();

                        persona.IdPersona = obj.IdPersona;
                        persona.Nombre = obj.Nombre;
                        persona.ApellidoPaterno = obj.ApellidoPaterno;
                        persona.ApellidoMaterno = obj.ApellidoMaterno;
                        persona.FechaNacimiento = obj.FechaNacimiento.Value.ToString("yyyy-MM-dd");
                        persona.Sexo = obj.Sexo;
                        persona.EstadoNacimiento = obj.EstadoNacimiento;
                        persona.Telefono = obj.Telefono;
                        persona.CURP = obj.CURP;

                        persona.Direccion = new ML.Direccion();
                        persona.Direccion.IdDireccion = obj.IdDireccion;
                        persona.Direccion.Calle = obj.Calle;
                        persona.Direccion.NumeroInterior = obj.NumeroInterior;
                        persona.Direccion.NumeroExterior = obj.NumeroExterior;
                        persona.Direccion.Colonia = obj.Colonia;
                        persona.Direccion.Municipio = obj.Municipio;
                        persona.Direccion.Estado = obj.Estado;

                        result.Object = persona;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static string GenerarCurp(ML.Persona persona)
        {
            try
            {
                Estado result;
                Enum.TryParse(persona.EstadoNacimiento, out result); //Obtenemos las coicidencias de los estdoas con el que fue ingresado
                //Obtenememos los sexos y si existen las cadenas
                var Sex = Sexo.Mujer;
                bool H = persona.Sexo.Contains("Hombre");
                //Valida si es un sexo o el otro
                if (H == true)
                {
                    Sex = Sexo.Hombre;
                }
                //Se genera el curp de acuerdo a los datos
                string CURP = Curp.Generar(persona.Nombre,
                                           persona.ApellidoPaterno,
                                           persona.ApellidoMaterno,
                                           Sex,
                                           DateTime.Parse(persona.FechaNacimiento),
                                           result);
                //Si se genero el curp lo guardamos en el curp si no no se guarda nada y mandamos la excepcion
                if (CURP != null)
                {
                    persona.CURP = CURP;
                }
            }
            catch (Exception ex)
            {
                //Error
            }
            return persona.CURP;//Devolvemos el curp
        }
    }
}
