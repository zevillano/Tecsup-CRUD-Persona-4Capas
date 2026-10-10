using System;

namespace CapaEntidades
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Estado { get; set; }
    }
}