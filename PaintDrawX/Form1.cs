using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDrawX
{
    public partial class PaintDrawX : Form
    {
        public PaintDrawX()
        {
            InitializeComponent();
            // Mengatur ukuran formulir menjadi 600x600 piksel
            this.ClientSize = new System.Drawing.Size(600, 600);
            // Menambahkan event handler untuk melukis formulir
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintDrawX_Paint);
        }

        private void PaintDrawX_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Pusat kanvas
            int pusatX = this.ClientSize.Width / 2;
            int pusatY = this.ClientSize.Height / 2;

            // Membuat pena dan kuas
            Pen pena = new Pen(Color.Black, 2);
            Brush kuasKepala = new SolidBrush(Color.FromArgb(108, 204, 218)); // Biru muda
            Brush kuasMata = new SolidBrush(Color.FromArgb(246, 200, 103)); // Kuning
            Brush kuasAntena = new SolidBrush(Color.FromArgb(108, 204, 218));
            Brush kuasMulut = new SolidBrush(Color.Black); // Hitam
            Brush kuasLidah = new SolidBrush(Color.FromArgb(235, 64, 89)); // Merah muda
            Brush kuasKacamata = new SolidBrush(Color.FromArgb(50, 50, 50)); // Abu-abu gelap untuk kacamata

            // Menggambar kepala
            // Dimensi elemen gambar
            int lebarKepala = 300;
            int tinggiKepala = 200;
            int kepalaX = pusatX - lebarKepala / 2; // Posisi horizontal kepala
            int kepalaY = pusatY - tinggiKepala / 2; // Posisi vertikal kepala

            g.FillRectangle(kuasKepala, kepalaX, kepalaY, lebarKepala, tinggiKepala); // Menggambar dan mengisi persegi panjang kepala
            g.DrawRectangle(pena, kepalaX, kepalaY, lebarKepala, tinggiKepala); // Menggambar batas persegi panjang kepala

            // Menggambar bentuk kubah untuk dasar antena

            int lebarKubah = 80;
            int tinggiKubah = 40;
            int kubahX = pusatX - lebarKubah / 2; // Posisi horizontal kubah
            int kubahY = kepalaY - tinggiKubah + 20; // Posisi vertikal kubah

            g.FillPie(kuasAntena, kubahX, kubahY, lebarKubah, tinggiKubah, 180, 180); // Menggambar dan mengisi kubah
            g.DrawArc(pena, kubahX, kubahY, lebarKubah, tinggiKubah, 180, 180); // Menggambar batas kubah

            // Menggambar lingkaran dasar antena
            int diameterLingkaranDasar = 10;
            int lingkaranDasarX = pusatX - diameterLingkaranDasar / 2; // Posisi horizontal lingkaran dasar
            int lingkaranDasarY = kubahY - diameterLingkaranDasar / 2; // Posisi vertikal

            g.FillEllipse(kuasAntena, lingkaranDasarX, lingkaranDasarY, diameterLingkaranDasar, diameterLingkaranDasar); // Menggambar dan mengisi lingkaran dasar
            g.DrawEllipse(pena, lingkaranDasarX, lingkaranDasarY, diameterLingkaranDasar, diameterLingkaranDasar); // Menggambar batas lingkaran dasar

            // Menggambar garis antena
            int tinggiAntena = 100; // Menambah tinggi antena
            g.DrawLine(pena, pusatX, lingkaranDasarY, pusatX, lingkaranDasarY - tinggiAntena); // Menggambar garis lurus sebagai antena

            // Menggambar lingkaran di sekitar garis antena (dua lingkaran, mulai dari kecil hingga besar)
            for (int i = 1; i <= 2; i++)
            {
                int radius = 5 * i; // Radius lingkaran
                int lingkaranX = pusatX - radius; // Posisi horizontal lingkaran
                int lingkaranY = lingkaranDasarY - radius - 20 * i; // Posisi vertikal lingkaran

                g.DrawEllipse(pena, lingkaranX, lingkaranY, radius * 2, radius); // Menggambar lingkaran
            }

            // Menggambar ujung antena
            int diameterUjung = 10;
            int ujungX = pusatX - diameterUjung / 2; // Posisi horizontal ujung antena
            int ujungY = lingkaranDasarY - tinggiAntena - diameterUjung; // Posisi vertikal ujung antena

            g.FillEllipse(kuasAntena, ujungX, ujungY, diameterUjung, diameterUjung); // Menggambar dan mengisi ujung antena
            g.DrawEllipse(pena, ujungX, ujungY, diameterUjung, diameterUjung); // Menggambar batas ujung antena

            // Menggambar kacamata berbentuk pil yang terhubung di antara mata
            int kacamataX = kepalaX + 40; // Posisi horizontal kacamata
            int kacamataY = kepalaY + 50; // Posisi vertikal kacamata
            int lebarKacamata = 230; // Lebar kacamata
            int tinggiKacamata = 50; // Tinggi kacamata
            int radiusKacamata = 25; // Radius kacamata

            // Membuat jalur grafis baru untuk kacamata
            GraphicsPath jalur = new GraphicsPath(); 

            // Menambahkan lengkungan atas kiri
            jalur.AddArc(kacamataX, kacamataY, radiusKacamata * 2, tinggiKacamata, 180, 90);
            // Menambahkan lengkungan atas kanan
            jalur.AddArc(kacamataX + lebarKacamata - radiusKacamata * 2, kacamataY, radiusKacamata * 2, tinggiKacamata, 270, 90);
            // Menambahkan lengkungan bawah kanan
            jalur.AddArc(kacamataX + lebarKacamata - radiusKacamata * 2, kacamataY + tinggiKacamata - radiusKacamata * 2, radiusKacamata * 2, radiusKacamata * 2, 0, 90);
            // Menambahkan lengkungan bawah kiri
            jalur.AddArc(kacamataX, kacamataY + tinggiKacamata - radiusKacamata * 2, radiusKacamata * 2, radiusKacamata * 2, 90, 90);
            // Menutup jalur grafis
            jalur.CloseFigure();

            // Mengisi jalur kacamata dengan kuas
            g.FillPath(kuasKacamata, jalur);
            // Menggambar batas jalur kacamata
            g.DrawPath(pena, jalur); 

            // Menggambar mata

            int lebarMata = 30;
            int tinggiMata = 30;
            int mata1X = kacamataX + 30; // Posisi horizontal mata pertama
            int mata1Y = kacamataY + 10; // Posisi vertikal mata pertama
            int mata2X = kacamataX + lebarKacamata - 60; // Posisi horizontal mata kedua
            int mata2Y = mata1Y; // Posisi vertikal mata kedua

            g.FillEllipse(kuasMata, mata1X, mata1Y, lebarMata, tinggiMata); // Menggambar dan mengisi mata pertama
            g.DrawEllipse(pena, mata1X, mata1Y, lebarMata, tinggiMata); // Menggambar batas mata pertama

            g.FillEllipse(kuasMata, mata2X, mata2Y, lebarMata, tinggiMata); // Menggambar dan mengisi mata kedua
            g.DrawEllipse(pena, mata2X, mata2Y, lebarMata, tinggiMata); // Menggambar batas mata kedua

            // Menggambar mulut yang tersenyum dan mengisi dengan warna hitam

            int lebarMulut = 100;
            int tinggiMulut = 50;
            int mulutX = pusatX - lebarMulut / 2; // Posisi horizontal mulut
            int mulutY = kepalaY + 100; // Posisi vertikal mulut

            g.FillPie(kuasMulut, mulutX, mulutY, lebarMulut, tinggiMulut, 0, 180); // Menggambar dan mengisi mulut
            g.DrawArc(pena, mulutX, mulutY, lebarMulut, tinggiMulut, 0, 180); // Menggambar batas mulut

            // Menggambar lidah di bagian bawah tengah

            int lebarLidah = 50;
            int tinggiLidah = 20;
            int lidahX = pusatX - lebarLidah / 2; // Posisi horizontal lidah
            int lidahY = mulutY + 30; // Posisi vertikal lidah

            g.FillPie(kuasLidah, lidahX, lidahY, lebarLidah, tinggiLidah, 0, 180); // Menggambar dan mengisi lidah
        }

        // Event handler untuk memuat formulir
        private void PaintDrawX_Load(object sender, EventArgs e) 
        {

        }
    }
}
