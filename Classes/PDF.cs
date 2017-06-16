using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using MigraDoc.DocumentObjectModel;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.IO;
using System.Windows.Media;

namespace FolkBok
{
    class InvoicePDF
    {
        private string name;
        private Invoice invoice;

        public InvoicePDF(string name, Invoice invoice)
        {
            this.name = name;
            this.invoice = invoice;
            createDocument();
        }

        private void createDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            double pointsPermm = page.Height / page.Height.Millimeter;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont Times11 = new XFont("Times New Roman", 11);
            XFont Times11Bold = new XFont("Times New Roman", 11, XFontStyle.Bold);
            XFont Times9 = new XFont("Times New Roman", 9);
            XStringFormat left = new XStringFormat();
            left.Alignment = XStringAlignment.Near;
            XStringFormat right = new XStringFormat();
            right.Alignment = XStringAlignment.Far;
            XStringFormat center = new XStringFormat();
            center.Alignment = XStringAlignment.Center;

            byte[] data;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FolkBok.Images.FolkLogo.png"))
            {
                int count = (int)stream.Length;
                data = new byte[count];
                stream.Read(data, 0, count);
            }
            MemoryStream mstream = new MemoryStream(data);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = mstream;
            image.EndInit();
            double topLeftX = 14.5 * pointsPermm;
            double topLeftY = 20.5 * pointsPermm;
            double height = 29.55 * pointsPermm;
            double width = 71.67 * pointsPermm;
            gfx.DrawImage(XImage.FromBitmapSource(image), topLeftX, topLeftY, width, height);

            width = 181.74 * pointsPermm;
            gfx.DrawString("Faktura", new XFont("Times New Roman", 24), XBrushes.Black, topLeftX + width, 27.63 * pointsPermm, right);

            topLeftY = 54.84 * pointsPermm - 1;
            height = 5.67 * pointsPermm + 1;
            width = 88.16 * pointsPermm;
            gfx.DrawRectangle(XBrushes.Black, topLeftX, topLeftY, width, height);
            System.Windows.Point lp = new System.Windows.Point(16.5 * pointsPermm, topLeftY);
            System.Windows.Point rp = new System.Windows.Point(16.5 * pointsPermm + width / 2, topLeftY);
            gfx.DrawString("Belopp", new XFont("Times New Roman", 14, XFontStyle.Bold), XBrushes.White, lp, left);
            gfx.DrawString(getAmountString(invoice.Sum), new XFont("Times New Roman", 14, XFontStyle.Bold), XBrushes.White, rp, left);

            topLeftY += height;
            height = 31.25 * pointsPermm;
            gfx.DrawRectangle(XBrushes.Gainsboro, topLeftX, topLeftY, width, height);
            lp.Y = topLeftY;
            rp.Y = topLeftY;
            gfx.DrawString("Fakturanummer", Times11, XBrushes.Black, lp, left);
            gfx.DrawString(invoice.Number.ToString(), Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + height / 7;
            rp.Y = topLeftY + height / 7;
            gfx.DrawString("Fakturadatum", Times11, XBrushes.Black, lp, left);
            gfx.DrawString(invoice.Date.ToShortDateString(), Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + 2 * height / 7;
            rp.Y = topLeftY + 2 * height / 7;
            gfx.DrawString("Betalningsvillkor", Times11, XBrushes.Black, lp, left);
            gfx.DrawString("30 dagar", Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + 3 * height / 7;
            rp.Y = topLeftY + 3 * height / 7;
            gfx.DrawString("Förfallodag", Times11, XBrushes.Black, lp, left);
            gfx.DrawString(invoice.Date.AddDays(30).ToShortDateString(), Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + 4 * height / 7;
            rp.Y = topLeftY + 4 * height / 7;
            gfx.DrawString("Dröjsmålsränta", Times11, XBrushes.Black, lp, left);
            gfx.DrawString("8%", Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + 5 * height / 7;
            rp.Y = topLeftY + 5 * height / 7;
            gfx.DrawString("Er referens", Times11, XBrushes.Black, lp, left);
            gfx.DrawString(invoice.YourReference, Times11, XBrushes.Black, rp, left);
            lp.Y = topLeftY + 6 * height / 7;
            rp.Y = topLeftY + 6 * height / 7;
            gfx.DrawString("Vår referens", Times11, XBrushes.Black, lp, left);
            gfx.DrawString(invoice.OurReference, Times11, XBrushes.Black, rp, left);

            topLeftY = 114.34 * pointsPermm;
            width = 181.74 * pointsPermm;
            height = 4.49 * pointsPermm + 1;
            gfx.DrawRectangle(XBrushes.Black, topLeftX, topLeftY, width, height);
            gfx.DrawString("Beskrivning", Times11, XBrushes.White, new System.Windows.Point(topLeftX + pointsPermm, topLeftY), left);
            gfx.DrawString("Datum", Times11, XBrushes.White, new System.Windows.Point(topLeftX + 5 * width / 8, topLeftY), left);
            gfx.DrawString("Belopp", Times11, XBrushes.White, new System.Windows.Point(topLeftX + 13 * width / 16, topLeftY), left);

            topLeftY += height;
            foreach (InvoiceLine line in invoice.getLines())
            {
                gfx.DrawString(line.Description, Times11, XBrushes.Black, new System.Windows.Point(topLeftX + pointsPermm, topLeftY),left);
                gfx.DrawString(line.Date.ToShortDateString(), Times11, XBrushes.Black, new System.Windows.Point(topLeftX + 5 * width / 8, topLeftY), left);
                gfx.DrawString(line.Cost.ToString() + " kr", Times11, XBrushes.Black, new System.Windows.Point(topLeftX + width, topLeftY), right);
                topLeftY = topLeftY + 5 * pointsPermm;
            }

            gfx.DrawLine(XPens.Black, topLeftX + 5 * width / 8, topLeftY + pointsPermm, topLeftX + width, topLeftY + pointsPermm);
            topLeftY = topLeftY + 1 * pointsPermm;
            gfx.DrawString("Summa att betala", Times11Bold, XBrushes.Black, new System.Windows.Point(topLeftX + 13 * width / 16, topLeftY), right);
            gfx.DrawString(getAmountString(invoice.Sum), Times11Bold, XBrushes.Black, new System.Windows.Point(topLeftX + width, topLeftY), right);

            height = 5;
            System.Windows.Point p1 = new System.Windows.Point(lp.X, 153.56 * pointsPermm);
            System.Windows.Point p2 = new System.Windows.Point(p1.X + 23.55 * pointsPermm, 153.56 * pointsPermm);
            System.Windows.Point p3 = new System.Windows.Point(p2.X + 75.58 * pointsPermm, 153.56 * pointsPermm);
            System.Windows.Point p4 = new System.Windows.Point(p3.X + 20.78 * pointsPermm, 153.56 * pointsPermm);
            gfx.DrawString("Organisation", Times9, XBrushes.Black, p1, left);
            gfx.DrawString("Folktetten", Times9, XBrushes.Black, p2, left);
            gfx.DrawString("Tel.", Times9, XBrushes.Black, p3, left);
            gfx.DrawString("0706-141866", Times9, XBrushes.Black, p4, left);
            p1.Y += height * pointsPermm;
            p2.Y += height * pointsPermm;
            p3.Y += height * pointsPermm;
            p4.Y += height * pointsPermm;
            gfx.DrawString("Adress", Times9, XBrushes.Black, p1, left);
            gfx.DrawString("Skarpskyttevägen 22G Lgh 1201 226 42 Lund", Times9, XBrushes.Black, p2,left);
            gfx.DrawString("E-post", Times9, XBrushes.Black, p3, left);
            gfx.DrawString("info@folktetten.se", Times9, XBrushes.Black, p4, left);
            p1.Y += height * pointsPermm;
            p2.Y += height * pointsPermm;
            p3.Y += height * pointsPermm;
            p4.Y += height * pointsPermm;
            gfx.DrawString("Org. Nr.", Times9, XBrushes.Black, p1, left);
            gfx.DrawString("802495-4656", Times9, XBrushes.Black, p2, left);
            gfx.DrawString("Hemsida", Times9, XBrushes.Black, p3, left);
            gfx.DrawString("www.folktetten.se", Times9, XBrushes.Black, p4, left);
            p2.Y += height * pointsPermm;
            gfx.DrawString("Godkänd för F-skatt", Times9, XBrushes.Black, p2, left);

            p2.X += 43.57 * pointsPermm;
            p2.Y = 172 * pointsPermm;
            gfx.DrawString("INBETALNING/GIRERING AVI Nr 1", Times11, XBrushes.Black, p2, left);
            topLeftY = 176.55 * pointsPermm;
            topLeftX = 13 * pointsPermm;
            width = 186.27 * pointsPermm;
            height = 72.7 * pointsPermm;
            gfx.DrawLine(XPens.Black, topLeftX, topLeftY, topLeftX + width, topLeftY);
            gfx.DrawLine(XPens.Black, topLeftX, topLeftY + height, topLeftX + width, topLeftY + height);
            gfx.DrawLine(XPens.Black, topLeftX, topLeftY, topLeftX, topLeftY + height);
            gfx.DrawLine(XPens.Black, topLeftX + width, topLeftY, topLeftX + width, topLeftY + height);
            gfx.DrawLine(XPens.Black, topLeftX, topLeftY + height / 2, topLeftX + width, topLeftY + height / 2);
            gfx.DrawLine(XPens.Black, topLeftX + width / 2, topLeftY + height / 2, topLeftX + width / 2, topLeftY + height);

            p1.Y = 221.47 * pointsPermm;
            p1.X = 111.81 * pointsPermm;
            gfx.DrawString("Folktetten", Times11Bold, XBrushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("Philip Jönsson", Times11, XBrushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("Skarpskyttevägen 22G Lgh 1201", Times11, XBrushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("226 42 Lund", Times11, XBrushes.Black, p1, left);

            System.Windows.Point top = new System.Windows.Point(120.76 * pointsPermm, 60.51 * pointsPermm);
            System.Windows.Point bottom = new System.Windows.Point(20.44 * pointsPermm, 221.47 * pointsPermm);
            string[] lines = invoice.Address.Split('\n');
            XFont font = Times11Bold;
            foreach (string line in lines)
            {
                gfx.DrawString(line, font, XBrushes.Black, top, left);
                gfx.DrawString(line, font, XBrushes.Black, bottom, left);
                bottom.Y += 4 * pointsPermm;
                top.Y += 4 * pointsPermm;
                if (font.Bold)
                {
                    font = Times11;
                }
            }

            XBrush brush = new XSolidBrush(XColor.FromArgb(208,206,206));
            topLeftX = 14.5 * pointsPermm;
            topLeftY = 257.92 * pointsPermm;
            width = 181.74 * pointsPermm;
            height = 14.44 * pointsPermm;
            gfx.DrawRectangle(brush, topLeftX, topLeftY, width, height);
            topLeftX += 35.15 * pointsPermm;
            topLeftY += height / 3;
            width = 52 * pointsPermm;
            height /= 3;
            gfx.DrawRectangle(XBrushes.White, topLeftX, topLeftY, width, height);
            p1.X = topLeftX + width / 2;
            p1.Y = topLeftY;
            gfx.DrawString(getAmountString(invoice.Sum), Times11Bold, XBrushes.Black, p1, center);
            topLeftX += width + 4.78 * pointsPermm;
            gfx.DrawRectangle(XBrushes.White, topLeftX, topLeftY, width, height);
            p1.X = topLeftX + width / 2;
            gfx.DrawString("666-4791", Times11Bold, XBrushes.Black, p1, center);

            string filename = name + ".pdf";
            document.Save(filename);
            Process.Start(filename);
        }

        private string getAmountString(double amount)
        {
            string result = "";
            result += (int)(amount);
            int dec = (int)Math.Round((Math.Round(amount, 2) - (int)(amount)) * 100); if(dec < 10)
            {
                result += ",0" + dec;
            }
            else
            {
                result += "," + dec;
            }
            return result + " kr";
        }
    }

    class VoucherPDF
    {
        private string name;
        private Voucher voucher;

        public VoucherPDF(string name, Voucher voucher)
        {
            this.name = name;
            this.voucher = voucher;
            createDocument();
        }

        private void createDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            double pointsPermm = page.Height / page.Height.Millimeter;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont Times11 = new XFont("Times New Roman", 11);
            XFont Times11Bold = new XFont("Times New Roman", 11, XFontStyle.Bold);
            XFont Times9 = new XFont("Times New Roman", 9);
            XStringFormat left = new XStringFormat();
            left.Alignment = XStringAlignment.Near;
            XStringFormat right = new XStringFormat();
            right.Alignment = XStringAlignment.Far;
            XStringFormat center = new XStringFormat();
            center.Alignment = XStringAlignment.Center;

            byte[] data;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FolkBok.Images.FolkLogo.png"))
            {
                int count = (int)stream.Length;
                data = new byte[count];
                stream.Read(data, 0, count);
            }
            MemoryStream mstream = new MemoryStream(data);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = mstream;
            image.EndInit();
            double topLeftX = 14.5 * pointsPermm;
            double topLeftY = 20.5 * pointsPermm;
            double height = 29.55 * pointsPermm;
            double width = 71.67 * pointsPermm;
            gfx.DrawImage(XImage.FromBitmapSource(image), topLeftX, topLeftY, width, height);

            string filename = name + ".pdf";
            document.Save(filename);
            Process.Start(filename);
        }
    }
}
