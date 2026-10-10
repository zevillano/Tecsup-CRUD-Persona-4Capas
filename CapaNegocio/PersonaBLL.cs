using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class PersonaBLL
    {
        private PersonaDAO objDAO = new PersonaDAO();

        // 1. Obtener la lista de personas
        public List<Persona> Listar()
        {
            return objDAO.Listar();
        }

        // 2. Buscar personas por filtro
        public List<Persona> Buscar(string filtro)
        {
            return objDAO.Buscar(filtro);
        }

        // 3. Registrar con validaciones
        public bool Registrar(Persona obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                mensaje = "El documento no puede estar vacío.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre no puede estar vacío.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido no puede estar vacío.";
                return false;
            }

            return objDAO.Registrar(obj);
        }

        // 4. Modificar con validaciones
        public bool Modificar(Persona obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.IdPersona <= 0)
            {
                mensaje = "Debe seleccionar una persona válida para modificar.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                mensaje = "El documento no puede estar vacío.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre no puede estar vacío.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido no puede estar vacío.";
                return false;
            }

            return objDAO.Modificar(obj);
        }

        // 5. Eliminar lógicamente
        public bool Eliminar(int idPersona, out string mensaje)
        {
            mensaje = string.Empty;

            if (idPersona <= 0)
            {
                mensaje = "Debe seleccionar una persona válida para eliminar.";
                return false;
            }

            return objDAO.Eliminar(idPersona);
        }
    }
}