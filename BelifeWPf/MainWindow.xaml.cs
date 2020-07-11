using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using BeLife.Negocio;

namespace BelifeWPf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        bool AltoContraste = false;

        public BeLife.Negocio.Contrato _contrato;
        public BeLife.Negocio.Cliente _cliente;

        //public int Id { get; } = 10; 

        public MainWindow()
        {
            InitializeComponent();
            Cargar();
            CargarSexo();
            CargarEstado();

            CargarSexoF();
            CargarEstadoF();
            //CargarContrato(CbTipoPlan.SelectedValue.ToString());
            //regristro Cliente
            //Limpiar();
            CargarCliente();

            //BtFiltroListadoCliente 
            //LimpiarF();
            CargarEstadoF();
            CargarSexoF();

            //registro contrato
        
       

            //Listado de contratos
            CargarContratos();
            //CargarPoliza();

        }

        private void Btn_despliegaFly_Click(object sender, RoutedEventArgs e)
        {
            fly.IsOpen = true;
        }

        private async void Cerrar(object sender, RoutedEventArgs e)
        {
            MessageDialogResult Result = await this.ShowMessageAsync("Confirmación", "¿Está Seguro que Desea Cerrar la Aplicación?", MessageDialogStyle.AffirmativeAndNegative);

            if (Result == MessageDialogResult.Affirmative)
            {
                this.Close();
            }
        }



        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtCliente_Click(object sender, RoutedEventArgs e)
        {
            TCPrincipal.SelectedIndex = 0;
            fly.IsOpen = false;

        }

        private void BtBusCliente_click(object sender, RoutedEventArgs e)
        {
            TCPrincipal.SelectedIndex = 1;

            CargarCliente();

            fly.IsOpen = false;
        }

        private void BtContrato_Click(object sender, RoutedEventArgs e)
        {
            TCPrincipal.SelectedIndex = 2;
            fly.IsOpen = false;
        }

        private void BtbusContrato_Click(object sender, RoutedEventArgs e)
        {
            TCPrincipal.SelectedIndex = 3;

            CargarContratos();

            fly.IsOpen = false;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }



        private void Cbsexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        /*
        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 

                                        TabItem 1 Registrar cliente

        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 
        */
        public void Limpiar()
        {
                
            TxRut.Text = string.Empty;
            TxNombres.Text = string.Empty;
            TxApellidos.Text = string.Empty;
            DpFechaNacimiento.SelectedDate = DateTime.Today;
            CbSexo.SelectedIndex = 0;
            CbEstadoCivil.SelectedIndex = 0;
            LbNombreCliente.Content = "";

            CargarSexo();
            CargarEstado();
         
            //CargarID();



        }







        public void CargarSexo()
        {
            
            /* Carga todas los cliente */
            Sexo sexo = new Sexo();
            CbSexo.ItemsSource = sexo.ReadAllSexo();

            /* Configura los datos en el ComboBOx */
            CbSexo.DisplayMemberPath = "Descripcion"; //Propiedad para mostrar
            CbSexo.SelectedValuePath = "Descripcion"; //Propiedad con el valor a rescatar

            CbSexo.SelectedIndex = 0; //Posiciona en el primer registro

        }

        public void CargarEstado()
        {
            /* Carga todas los cliente */
            Estado estado = new Estado();
            CbEstadoCivil.ItemsSource = estado.ReadAllEstado();

            /* Configura los datos en el ComboBOx */
            CbEstadoCivil.DisplayMemberPath = "Descripcion"; //Propiedad para mostrar
            CbEstadoCivil.SelectedValuePath = "Descripcion"; //Propiedad con el valor a rescatar

            CbEstadoCivil.SelectedIndex = 0; //Posiciona en el primer registro

        }
                      


        //basura
        private void CbEstadoCivil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //

        //BOTON REGISTRAR cliente listo no tocar 
        private async void BtRegistrarCliente_Click_1(object sender, RoutedEventArgs e)
        {

            Cliente cli = new Cliente()
            {
                RutCliente = TxRut.Text,
                Nombres = TxNombres.Text,
                Apellidos = TxApellidos.Text,
                FechaNacimiento = DateTime.Today,
                IdSexo = CbSexo.SelectedValue.ToString(),
                IdEstadoCivil = CbEstadoCivil.SelectedValue.ToString()
            };


            if (cli.Create())
            {
                await this.ShowMessageAsync("Exito", "Cliente Registrado");
                Limpiar();
            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamnete", "Cliente No Pudo Ser Registrado");
            }
        }


        //BOTON BUSCAR
        private async void BtBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            //BOTON BUSCAR SI el cliente esta registrado
            Cliente cli = new Cliente()
            {
                RutCliente = TxRut.Text
            };

            if (cli.Read())
            {
                TxNombres.Text = cli.Nombres;
                TxApellidos.Text = cli.Apellidos;
                DpFechaNacimiento.SelectedDate = cli.FechaNacimiento;
                CbSexo.SelectedValue = cli.IdSexo;
                CbEstadoCivil.SelectedValue = cli.IdEstadoCivil;


                await this.ShowMessageAsync("Exito", "Cliente Leído");
            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Cliente No ha sido Leído");
            }
        }



        //BOTON ACTUALIZAR
        private async void BtActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            //BOTON ACTUALIZAR datos del cliente
            Cliente cli = new Cliente()
            {
                RutCliente = TxRut.Text,
                Nombres = TxNombres.Text,
                Apellidos = TxApellidos.Text,
                FechaNacimiento = DpFechaNacimiento.SelectedDate.Value,
                IdSexo = CbSexo.SelectedValue.ToString(),
                IdEstadoCivil = CbEstadoCivil.SelectedValue.ToString()

            };

            if (cli.Update())
            {
                await this.ShowMessageAsync("Exito", "Cliente Actualizado");
                Limpiar();
            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Cliente No Pudo Ser Actualizado");
            }
        }


        //BOTON ELIMINAR
        private async void BtEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            //BOTON ELIMINAR al cliente 
            Cliente cli = new Cliente()
            {
                RutCliente = TxRut.Text
            };
            if (cli.Delete())
            {
                await this.ShowMessageAsync("Exito", "Cliente Eliminado");
                Limpiar();

            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Cliente No Pudo Ser Eliminado");
            }
        }

        private void BtLimpiarCliente_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }







        /*
        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 

                                        TabItem 2 Lista clientes

        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 
        */

        private void LimpiarF()
        {

            TxFiltrarRut.Text = string.Empty;
            CbFiltrarSexo.SelectedIndex = 0;
            CbFiltrarEstadoCivil.SelectedIndex = 0;
           
            CargarSexoF();
            CargarEstadoF();
            

        }

        private void CargarSexoF()
        {
            /* Carga todas los cliente */
            Sexo sexo = new Sexo();
            CbFiltrarSexo.ItemsSource = sexo.ReadAllSexo();

            /* Configura los datos en el ComboBOx */
            CbFiltrarSexo.DisplayMemberPath = "Descripcion"; //Propiedad para mostrar
            CbFiltrarSexo.SelectedValuePath = "IdSexo"; //Propiedad con el valor a rescatar
            CbFiltrarSexo.SelectedIndex = 0; //Posiciona en el primer registro

        }
        private void CargarEstadoF()
        {
            /* Carga todas los cliente */
            Estado estado = new Estado();
            CbFiltrarEstadoCivil.ItemsSource = estado.ReadAllEstado();

            /* Configura los datos en el ComboBOx */
            CbFiltrarEstadoCivil.DisplayMemberPath = "Descripcion"; //Propiedad para mostrar
            CbFiltrarEstadoCivil.SelectedValuePath = "IdEstadoCivi"; //Propiedad con el valor a rescatar
            CbFiltrarEstadoCivil.SelectedIndex = 0; //Posiciona en el primer registro

        }


        public void CargarCliente()
        {

            //cargar los empleados en la data grid 
            Cliente clientes = new Cliente();
            DGlistadoClientes.ItemsSource = clientes.ReadAll();


        }

        public void filtro()
        {
            Cliente cli = new Cliente();
            DGlistadoClientes.ItemsSource = cli.ReadSE(TxFiltrarRut.Text, CbFiltrarSexo.SelectedIndex, CbFiltrarEstadoCivil.SelectedIndex);
        }

        private void BtFiltroListadoCliente_Click(object sender, RoutedEventArgs e)
        {
            filtro();
        }
        private void Btnrefresliscli(object sender, MouseButtonEventArgs e)
        {
            CargarCliente();
        }

        //BOTON REFRESCAR CLIENTE
        private void BtrefreshListadoCliente_Click(object sender, RoutedEventArgs e)
        {
            CargarCliente();
            Limpiar();
        }

        /*
        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 

                                        TabItem 3 Registrar contrato

        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 
        */

        //cargar el combobox 
        private void CargarContrato()
        {
            //Carga todas los contratos */
            Plan plan = new Plan();
            CbCodigoPlan.ItemsSource = plan.ReadAllPlan(CbTipoPlan.SelectedValue.ToString());

            /* Configura los datos en el ComboBOx */
            CbCodigoPlan.DisplayMemberPath = "Nombre"; //Propiedad para mostrar
            CbCodigoPlan.SelectedValuePath = "IdPlan"; //Propiedad con el valor a rescatar

            CbCodigoPlan.SelectedIndex = 0; //Posiciona en el primer registro

        }

        private void Cargar()
        {
            TipoContrato tipoContrato = new TipoContrato();
            CbTipoPlan.ItemsSource = tipoContrato.ReadAll();
            CbTipoPlan.DisplayMemberPath = "Descripcion";
            CbTipoPlan.SelectedValuePath = "IdTipoContrato";
            CbTipoPlan.SelectedIndex = 0; //Posiciona en el primer registro

     


        }

        //private void CargarID()
        //{
        //    Plan plan = new Plan();
        //    CbCodigo .ItemsSource = plan.ReadAll3();
        //    CbCodigo.DisplayMemberPath = "Nombre";
        //    CbCodigo.SelectedValuePath = "IdPlan";


        //}



        //BOTON CREAR CONTRATO
        private async void BtCrearContrato_Click(object sender, RoutedEventArgs e)
        {


            //boton crear contrato en UI
            Contrato contrato = new Contrato();

            Tarificador pr = new Tarificador();

            Double Prianual = pr.Prima_anual(TxRutCliente.Text);

            if (CbCodigoPlan.SelectedIndex == 0)
            {
                Prianual = Prianual + 0.5;
            }
            else if (CbCodigoPlan.SelectedIndex == 1)
            {
                Prianual = Prianual + 3.5;
            }
            else if (CbCodigoPlan.SelectedIndex == 2)
            {
                Prianual = Prianual + 1.2;
            }
            else if (CbCodigoPlan.SelectedIndex == 3)
            {
                Prianual = Prianual + 2;
            }
            else if (CbCodigoPlan.SelectedIndex == 4)
            {
                Prianual = Prianual + 3.5;
            }


            Double Primensual = pr.Prima_anual(TxRutCliente.Text) / 12;



            contrato.RutCliente = TxRutCliente.Text;

            contrato.FechaCreacion = DateTime.Today;
            contrato.FechaTermino = (DateTime)DpFechaTermino.SelectedDate;
            contrato.FechaInicioVigencia = (DateTime)DpFechaInicioVig.SelectedDate;
            //el fin de la vigencia se calcula cin el inicio mas 1 año
            contrato.FechaFinVigencia = ((DateTime)DpFechaInicioVig.SelectedDate).AddYears(1);
            //registro automatico de la prima mensual
            contrato.PrimaMensual = Primensual;
            //registro automatico de la prima anual 
            contrato.PrimaAnual = Prianual;
            contrato.CodigoPlan = CbCodigoPlan.SelectedValue.ToString();
            contrato.IdTipoContrato = int.Parse(CbTipoPlan.SelectedValue.ToString());
            contrato.Observaciones = TxObservaciones.Text;

            if (ChBDeclaracionSalud.IsChecked == true)
            {
                contrato.Vigente = true;
            }

            else
            {
                contrato.Vigente = false;
            }

            if (ChBDeclaracionSalud.IsChecked == true)
            {
                contrato.DeclaracionSalud = true;
            }
            else
            {
                contrato.DeclaracionSalud = false;
            }

            contrato.Numero = Convert.ToDateTime((DateTime.Now)).ToString("yyyyMMddhhmmss");

            if (contrato.CreateContrato())
            {
                await this.ShowMessageAsync("Exito", "Contrato Registrado");
                LimpiarControles();
            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Contrato No Pudo Ser Registrado");
                
            }
        }


        //BOTON ACTUALIZAR 
        private async void BtActualizarContrato_Click(object sender, RoutedEventArgs e)
        {
               
             
            Contrato contrato = new Contrato();
            contrato.Numero = TxNContrato.Text.ToString();
            contrato.RutCliente = TxRutCliente.Text;
            contrato.FechaCreacion = (DateTime)DpFechaCreacion.SelectedDate;
            contrato.FechaTermino = (DateTime)DpFechaTermino.SelectedDate;
            contrato.FechaInicioVigencia = (DateTime)DpFechaInicioVig.SelectedDate;
            contrato.FechaFinVigencia = (DateTime)DpFechaFInVig.SelectedDate;
            contrato.PrimaMensual = Convert.ToDouble(TxPrimaMensual.Text);
            contrato.PrimaAnual = Convert.ToDouble(TxPrimaAnual.Text);
            contrato.CodigoPlan = CbCodigoPlan.SelectedValue.ToString();
            contrato.IdTipoContrato = int.Parse(CbTipoPlan.SelectedValue.ToString());
            contrato.Observaciones = TxObservaciones.Text;
            ChBVigencia.IsEnabled = true;

            if (ChBVigencia.IsChecked == true)
            {
                contrato.Vigente = true;
            }
            else
            {
                contrato.Vigente = false;
            }

            if (ChBDeclaracionSalud.IsChecked == true)
            {
                contrato.DeclaracionSalud = true;
            }
            else
            {
                contrato.DeclaracionSalud = false;
            }



            if (contrato.UpdateContrato())
            {
                await this.ShowMessageAsync("Exito", "Contrato Actualizado");
                LimpiarControles();
            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Contrato No Pudo Ser Actualizado");

            }
        }


        //BOTON BUSCAR 
        private async void BtBuscarContrato_Click(object sender, RoutedEventArgs e)
        {


            Contrato con = new Contrato();


            if (!TxRutCliente.Text.Equals(""))
            {
                con.RutCliente = TxRutCliente.Text;
            }
            else
            {
                con.Numero = TxNContrato.Text;
            }

            //Para buscar el nombre del cliente y ponerlo en contrato
            if (con.ReadContrato())
            {
                Cliente cli = new Cliente();


                if (!TxRutCliente.Text.Equals(""))
                {
                    cli.RutCliente = TxRutCliente.Text;
                }
                else
                {
                    cli.RutCliente = con.RutCliente;
                }


                if (cli.Read())
                {
                    LbNombreCliente.Content = "Cliente:" + " " + cli.Nombres + " " + cli.Apellidos;
                }


                TxNContrato.Text = con.Numero;
                DpFechaCreacion.SelectedDate = con.FechaCreacion;
                DpFechaTermino.SelectedDate = con.FechaTermino;
                DpFechaInicioVig.SelectedDate = con.FechaInicioVigencia;
                DpFechaFInVig.SelectedDate = con.FechaFinVigencia;
                TxPrimaMensual.Text = con.PrimaMensual.ToString();
                TxPrimaAnual.Text = con.PrimaAnual.ToString();
                CbCodigoPlan.SelectedValue = con.CodigoPlan;
                CbTipoPlan.SelectedValue = con.IdTipoContrato;
                TxObservaciones.Text = con.Observaciones;
                TxRutCliente.Text = con.RutCliente;

                //Checkbox de vigencia
                if (con.Vigente == true)
                {
                    ChBVigencia.IsChecked = true;
                }
                else
                {
                    ChBVigencia.IsChecked = false;
                }

                //Checkbox de decaracion salud
                if (con.DeclaracionSalud == true)
                {
                    ChBDeclaracionSalud.IsChecked = true;
                }
                else
                {
                    ChBDeclaracionSalud.IsChecked = false;
                }

                //bloquear los datos
                TxRutCliente.IsEnabled = false;
                DpFechaCreacion.IsEnabled = false;
                DpFechaTermino.IsEnabled = false;
                DpFechaFInVig.IsEnabled = false;
                TxPrimaAnual.IsEnabled = false;
                TxPrimaMensual.IsEnabled = false;
                TxNContrato.IsEnabled = false;

                ChBDeclaracionSalud.IsEnabled = false;
                CbCodigoPlan.IsEnabled = false;
                DpFechaInicioVig.IsEnabled = false;

                await this.ShowMessageAsync("Exito", "Contrato Leído");

            }
            else
            {
                await this.ShowMessageAsync("Intentalo Nuevamente", "Contrato No Pudo Ser Leído");

            }
        }


        //BOTON TERMINAR
        private async void BtTerminarContrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            contrato.Numero = TxNContrato.Text;
            contrato.RutCliente = TxRutCliente.Text;
            contrato.FechaCreacion = (DateTime)DpFechaCreacion.SelectedDate;
            contrato.FechaInicioVigencia = (DateTime)DpFechaInicioVig.SelectedDate;
            contrato.FechaFinVigencia = (DateTime)DpFechaFInVig.SelectedDate;
            contrato.PrimaMensual = Convert.ToDouble(TxPrimaMensual.Text);
            contrato.PrimaAnual = Convert.ToDouble(TxPrimaAnual.Text);
            contrato.CodigoPlan = CbCodigoPlan.SelectedValue.ToString();
            contrato.Observaciones = TxObservaciones.Text;
            ChBVigencia.IsEnabled = false;

            if (ChBVigencia.IsChecked == true)
            {
                contrato.Vigente = false;
            }
            else
            {
                contrato.Vigente = false;
            }
            if (ChBDeclaracionSalud.IsChecked == true)
            {
                contrato.DeclaracionSalud = true;
            }
            else
            {
                contrato.DeclaracionSalud = false;
            }

            if (contrato.DeleteContrato())
            {
                await this.ShowMessageAsync("Información", "Contrato Terminado");
                LimpiarControles();
                ChBVigencia.IsEnabled = true;
            }
            else
            {

                await this.ShowMessageAsync("Intentelo Nuevamente", "Contrato No Pudo Ser Terminado");
            }
        }




        private void BtLimpiarContrato_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            //limpiar controles de UI
            TxRutCliente.Text = string.Empty;
            DpFechaCreacion.SelectedDate = DateTime.Today;
            DpFechaTermino.SelectedDate = DateTime.Today;
            DpFechaInicioVig.SelectedDate = DateTime.Today;
            DpFechaFInVig.SelectedDate = DateTime.Today;
            ChBVigencia.IsChecked = false;
            ChBDeclaracionSalud.IsChecked = false;
            TxPrimaAnual.Text = string.Empty;
            TxPrimaMensual.Text = string.Empty;
            TxObservaciones.Text = string.Empty;
            CbCodigoPlan.SelectedIndex = 0;
            TxNContrato.Text = string.Empty;
            LbNombreCliente.Content = "";

          

            //bloquear los datos
            TxRutCliente.IsEnabled = true;
            DpFechaCreacion.IsEnabled = true;
            DpFechaTermino.IsEnabled = true;
            DpFechaFInVig.IsEnabled = true;
            DpFechaInicioVig.IsEnabled = false;
            TxPrimaAnual.IsEnabled = false;
            TxPrimaMensual.IsEnabled = false;
            ChBDeclaracionSalud.IsEnabled = true;
            CbCodigoPlan.IsEnabled = true;
            TxNContrato.IsEnabled = true;
        }

        // Filtro al listado de contrato
        public void FiltroCli()
        {
            Cliente cli = new Cliente();
            DGlistadoClientes.ItemsSource = cli.FilCli(TxRutCliente.Text);
        }

        private void BtnBuscListadoCli(object sender, RoutedEventArgs e)
        {
            FiltroCli();

            TCPrincipal.SelectedIndex = 1;

            LimpiarControles();

            fly.IsOpen = false;
        }
        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        // Filtro al listado de cliente
        public void FiltroCon()
        {
            Contrato con = new Contrato();
            DGListadoContrato.ItemsSource = con.FilCon(TxNContrato.Text);
        }

        private void BtnBuscListadoCon(object sender, RoutedEventArgs e)
        {
            FiltroCon();

            TCPrincipal.SelectedIndex = 3;

            LimpiarControles();

            fly.IsOpen = false;
        }
        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||





        /*
        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 

                                        TabItem 4 Lista contratos

        0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000 
        */



        private void CargarContratos()
        {
            /* Carga todos los Empleados */
            Contrato contrato = new Contrato();
            DGListadoContrato.ItemsSource = contrato.ReadAll();
        }

        //private void CargarPoliza()
        //{
        //    /* Carga todas los cliente */
        //    Plan pl = new Plan();
        //    CBFiltroNumPoliza.ItemsSource = pl.ReadAllPlan();

        //    /* Configura los datos en el ComboBOx */
        //    CBFiltroNumPoliza.DisplayMemberPath = "PolizaActual"; //Propiedad para mostrar
        //    CBFiltroNumPoliza.SelectedValuePath = "PolizaActual"; //Propiedad con el valor a rescatar
        //    CBFiltroNumPoliza.SelectedIndex = 0; //Posiciona en el primer registro

        //}

        public void BFiltro()
        {
            Contrato con = new Contrato();
            DGListadoContrato.ItemsSource = con.ReadS(TxNumFiltroContrato.Text, TxRutFiltroContrato.Text, CBFiltroNumPoliza.SelectedValue.ToString());
        }//filtro

        private void Filtrarcont(object sender, RoutedEventArgs e)
        {
            BFiltro();
        }

        private void BtRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarContratos();
            LimpiarDG();
        }


        private void LimpiarDG()
        {

            TxRutFiltroContrato.Text = string.Empty;
            TxNumFiltroContrato.Text = string.Empty;
            CBFiltroNumPoliza.SelectedIndex = 0;
            

        }


        //terminar la seleccion de datos de la lista
        private void DGlistadoClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Limpiar();
            if (DGlistadoClientes.SelectedItem == null) return;
            var selected = DGlistadoClientes.SelectedItem as Cliente;
            
        }
        



        private void Btnrefresliscon(object sender, MouseButtonEventArgs e)
        {
            CargarContratos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //prueba alto contraste 
            
            if (AltoContraste == false)
            {
                Application.Current.Resources.MergedDictionaries.Clear();

                
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Green.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedTabControl.xaml", UriKind.RelativeOrAbsolute)
                });



                //Llamada a tema altocontraste
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("DiccionarioRecursos/BotonDiccionarioAltoContraste.xaml", UriKind.RelativeOrAbsolute)
                });

                AltoContraste = true;

            }
            else if (AltoContraste == true)
            {

                Application.Current.Resources.MergedDictionaries.Clear();


                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Steel.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Baselight.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedTabControl.xaml", UriKind.RelativeOrAbsolute)
                });



                //Llamada a tema altocontraste
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("DiccionarioRecursos/BotonDiccionario.xaml", UriKind.RelativeOrAbsolute)
                });

                AltoContraste = false;
            }
        }

        private void MetroTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Guarda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CbTipoPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarContrato();
        }
    }
    
}
