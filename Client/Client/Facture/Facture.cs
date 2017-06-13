using Client.Model;
using MigraDoc.DocumentObjectModel;
using System;
using System.Diagnostics;

namespace Client.Facture
{
    public class Facture : IFacture
    {
        private static Facture _instance;

        protected Facture()
        {
        }

        public static Facture GetInstace()
        {
            if (_instance == null)
            {
                _instance = new Facture();
            }
            return _instance;
        }

        public void NewFacture(Transakcja transakcja)
        {
            try
            {
                System.Collections.Generic.IEnumerable<Artykul_w_transakcji> art = transakcja.Artykuly_w_transakcji;

                MigraDoc.DocumentObjectModel.Document doc = new MigraDoc.DocumentObjectModel.Document();
                MigraDoc.DocumentObjectModel.Section sec = doc.AddSection();

                Paragraph paragraph = sec.Headers.Primary.AddParagraph();
                paragraph.AddText("Szczegółowe dane transakcji");
                paragraph.Format.Font.Size = 18;
                paragraph.Format.Font.Bold = true;
                paragraph.Format.Alignment = ParagraphAlignment.Center;

                MigraDoc.DocumentObjectModel.Tables.Table table = new MigraDoc.DocumentObjectModel.Tables.Table();
                table.Borders.Width = 0;

                MigraDoc.DocumentObjectModel.Tables.Column column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(5));
                column.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;

                column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(7));
                column.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;

                MigraDoc.DocumentObjectModel.Tables.Row row = table.AddRow();
                MigraDoc.DocumentObjectModel.Tables.Cell cell = row.Cells[0];

                cell = row.Cells[0];
                cell.AddParagraph("Nr transakcji");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.idTransakcji.ToString());

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Data");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.Data.ToString());

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Imię");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.Klienci.Imie);

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Nazwisko");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.Klienci.Nazwisko);

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Nazwa firmy");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.Klienci.Nazwa_firmy);

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Nazwa dostawcy");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];
                cell.AddParagraph(transakcja.Dostawcy.Nazwa);

                row = table.AddRow();

                cell = row.Cells[0];
                cell.AddParagraph("Przedmioty");
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 12;
                cell = row.Cells[1];

                double suma = 0;
                foreach (Artykul_w_transakcji awt in art)
                {
                    cell = row.Cells[1];
                    cell.AddParagraph(awt.Artykuly.Nazwa + ", " + awt.Artykuly.Cena + "zl" + "\n");
                    suma = suma + (double)awt.Artykuly.Cena;
                }

                doc.LastSection.Add(table);

                sec.AddParagraph();
                sec.AddParagraph();
                doc.AddSection();
                sec.AddParagraph("Suma: " + suma + "zł");
                sec.AddParagraph();

                MigraDoc.Rendering.PdfDocumentRenderer docRend = new MigraDoc.Rendering.PdfDocumentRenderer(false);
                docRend.Document = doc;
                docRend.RenderDocument();

                string name = "TransInfo.pdf";

                docRend.PdfDocument.Save(name);
                Process.Start(name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Facture NewFacture: {ex} " + nameof(NewFacture));
            }
        }
    }
}