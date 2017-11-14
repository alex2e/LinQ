using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace LinQ
{
    public partial class MainPage : ContentPage
    {
        #region Atributos
        /// <summary>
        /// Lista de contactos
        /// </summary>
        ObservableCollection<Contacto> contactos = new ObservableCollection<Contacto>();

        /// <summary>
        /// Lista de contactos modificada según clasusla LinQ utilizada
        /// </summary>
        ObservableCollection<Contacto> contactosMostrar = new ObservableCollection<Contacto>();
        #endregion

        #region Métodos
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            
            //Leemos los contactos del XML y lo introducimos en nuestra list
            LeerContactos();

            //Enviamos nuestra List contactos a el ListView
            lvwListaUsuarios.ItemsSource = contactos;


            #region Clicked buttons
            //Reset button
            btnReset.Clicked += BtnReset_Clicked;
            //Where button
            btnWhere.Clicked += (sender, args) =>
            {
                BtnWhereClicked();
            };
            //First Or Default button
            btnFirstOrDefault.Clicked += (sender, args) =>
            {
                BtnFirstOrDefaultClicked();

            };
            //Single Or Default button
            btnSingleOrDefault.Clicked += BtnSingleOrDefault_Clicked;
            //Last Or Default button
            btnLastOrDefault.Clicked += BtnLastOrDefault_Clicked;
            //OrderBy button
            btnOrderBy.Clicked += BtnOrderBy_Clicked;
            //OrderByDescending
            btnOrderByDescending.Clicked += BtnOrderByDescending_Clicked;
            //SkipWhile button
            btnSkipWhile.Clicked += BtnSkipWhile_Clicked;
            //TakeWhile button
            btnTakeWhile.Clicked += BtnTakeWhile_Clicked;
            #endregion

        }
        /// <summary>
        /// Este método realiza la clausula SkipWhile de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakeWhile_Clicked(object sender, EventArgs e)
        {
            List<Contacto> contactosAux = contactos.TakeWhile(t => int.Parse(t.Edad) >= 18).ToList();
            contactosMostrar.Clear();
            if (contactosAux.Count() != 0)
            {
                foreach (Contacto c in contactosAux)
                {
                    contactosMostrar.Add(c);
                }
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }
        }
        /// <summary>
        /// Este método realiza la clausula SkipWhile de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSkipWhile_Clicked(object sender, EventArgs e)
        {
            List<Contacto> contactosAux = contactos.SkipWhile(t => int.Parse(t.Edad) >= 18).ToList();
            contactosMostrar.Clear();
            if (contactosAux.Count() != 0)
            {
                foreach (Contacto c in contactosAux)
                {
                    contactosMostrar.Add(c);
                }
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }

        }
        /// <summary>
        /// Este método realiza la clausula OrderByDescending de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderByDescending_Clicked(object sender, EventArgs e)
        {
            List<Contacto> contactosAux = contactos.OrderByDescending(t => t.Nombre).ToList();
            contactosMostrar.Clear();
            if (contactosAux.Count() != 0)
            {
                foreach (Contacto c in contactosAux)
                {
                    contactosMostrar.Add(c);
                }
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }
        }
        /// <summary>
        /// Este método realiza la clausula OrderBy de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderBy_Clicked(object sender, EventArgs e)
        {
            List<Contacto> contactosAux = contactos.OrderBy(t => t.Nombre).ToList();
            contactosMostrar.Clear();
            if (contactosAux.Count() != 0)
            {
                foreach (Contacto c in contactosAux)
                {
                    contactosMostrar.Add(c);
                }
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }

        }
        /// <summary>
        /// Este método realiza la clausula LastOrDefault de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLastOrDefault_Clicked(object sender, EventArgs e)
        {
            Contacto personaLOD = contactos.LastOrDefault(p => p.Edad == EntryEdad.Text);
            contactosMostrar.Clear();
            if (personaLOD != null)
            {
                contactosMostrar.Add(personaLOD);
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }
        }
        /// <summary>
        /// Este método realiza la clausula SingleOrDefault de Linq cuando el botón es pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSingleOrDefault_Clicked(object sender, EventArgs e)
        {
            try
            {
                Contacto personaSOD = contactos.Where(p => p.Nombre.Contains(entryName.Text)).SingleOrDefault(p => p.Edad == EntryEdad.Text);
                contactosMostrar.Clear();
                if (personaSOD != null)
                {
                    contactosMostrar.Add(personaSOD);
                    CargarListView();
                }
                else
                {
                    LanzarAdvertencia(null, "No se han encontrado datos.");
                }
            }
            catch (Exception)
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");

            }
        }
        /// <summary>
        /// Este método resetea la lista de contactos mostrados, dejandola como al iniciar la App.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Clicked(object sender, EventArgs e)
        {
            contactosMostrar.Clear();
            foreach (Contacto c in contactos)
            {
                contactosMostrar.Add(c);
            }
            CargarListView();
        }
        /// <summary>
        /// Este método realiza la clausula FirstOrDefault de Linq cuando el botón es pulsado.
        /// </summary>
        private void BtnFirstOrDefaultClicked()
        {
            // Se recupera la primera instancia que cumple con el criterio de búsqueda
            Contacto personaFirstExiste = contactos.FirstOrDefault(t => t.Nombre == entryName.Text);
            contactosMostrar.Clear();
            if (personaFirstExiste != null)
            {
                contactosMostrar.Add(personaFirstExiste);
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null, "No se han encontrado datos.");
            }
        }
        /// <summary>
        /// Este método realiza la clausula Where de Linq cuando el botón es pulsado.
        /// </summary>
        private void BtnWhereClicked()
        {
            List<Contacto> contactosAux = contactos.Where(t => t.Nombre == entryName.Text).ToList();
            contactosMostrar.Clear();
            if (contactosAux.Count() != 0)
            {
                foreach (Contacto c in contactosAux)
                {
                    contactosMostrar.Add(c);
                }
                CargarListView();
            }
            else
            {
                LanzarAdvertencia(null,"No se han encontrado datos.");
            }
        }

        /// <summary>
        /// Este metodo inserta en una lista de contactos los que saca del archivo info.xml
        /// </summary>
        public void LeerContactos()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("LinQ.Assets.Info.xml");
            StreamReader objReader = new StreamReader(stream);
            var doc = XDocument.Load(stream);

            foreach (XElement element in doc.Root.Elements())
            {
                contactos.Add(new Contacto(element.Element("NOMBRE").Value, int.Parse (element.Element("EDAD").Value), element.Element("DNI").Value));
            }


        }

        /// <summary>
        ///  Muestra al usuario información sobre un error que él esta cometiendo
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="mensaje"></param>
        private void LanzarAdvertencia(String titulo, String mensaje)
        {
            DisplayAlert(titulo, mensaje, "ACEPTAR");
        }

        /// <summary>
        /// Vacia el listVew con los contactos buscados
        /// </summary>
        private void CargarListView()
        {
            lvwListaUsuarios.ItemsSource = contactosMostrar;
        }

        #endregion    
    }
}
