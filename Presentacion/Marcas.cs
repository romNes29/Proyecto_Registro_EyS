using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Presentacion
{
    public partial class Marcas : Form
    {
        private string Path = @"C:\Users\donacion\source\repos\PruebaWebCam\PruebaWebCam";
        private bool HayDispositivos;
        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice MiWebCam;
        public Marcas()
        {
            InitializeComponent();
        }

        private void fechaHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        public void CargaDispositivos()
        {
            //ELijo el tipo de dispositivos que deseo
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivos.Count > 0)
            {
                //Carga todos los dispositivos que se encuentren en un combo box
                HayDispositivos = true;
                for (int i = 0; i < MisDispositivos.Count; i++)
                {
                    cbxDispositivos.Items.Add(MisDispositivos[i].Name.ToString());
                }
                cbxDispositivos.Text = MisDispositivos[0].ToString();
            }
            else
            {
                HayDispositivos = false;
                cbxDispositivos.Text = "NO EXISTEN DISPOSITIVOS";
            }
        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone(); //Bitmap formato de imagen que se captura
            pbx1.Image = Imagen;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            int i = cbxDispositivos.SelectedIndex;
            string NombreVideo = MisDispositivos[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();
        }
        private void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }

        private void Marcas_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }
        private void capturar()
        {
            pbx2.Image = pbx1.Image;
            pbx2.Image.Save(Path + "HDNESTOR.jpg", ImageFormat.Jpeg);
        }
    }
}
