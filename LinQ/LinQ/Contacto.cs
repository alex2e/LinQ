using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ
{
    public class Contacto
    {
        #region ATRIBUTOS
        /// <summary>
        /// Atributos de tipo String: Nombre y DNI.
        /// </summary>
        private string nombre, dni;
        /// <summary>
        /// Atributo de tipo int: edad
        /// </summary>
        private int edad;
        #endregion

        #region PROPIEDADES
        /// <summary>
        /// Propiedad Nombre
        /// </summary>
        public string Nombre
        {
            get
            {
                return nombre;
            }
        }
        /// <summary>
        /// Propiedad Edad
        /// </summary>
        public String Edad
        {
            get
            {
                return edad.ToString();
            }
        }
        /// <summary>
        /// Propiedad DNI
        /// </summary>
        public string Dni
        {
            get
            {
                return dni;
            }
        }
        #endregion

        /// <summary>
        /// Constructor de Contacto
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="edad"></param>
        /// <param name="dni"></param>
        public Contacto(string nombre, int edad, string dni)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.dni = dni;
        }
        
        /// <summary>
        /// Método utilizado para mostrar los detalles del contacto con formato
        /// </summary>
        /// <returns>Cadena formateada con la información de contacto</returns>
        override
        public string ToString()
        {
            return string.Format("{0}     {1}     {2}", nombre, edad, dni);
        }

    }
}
