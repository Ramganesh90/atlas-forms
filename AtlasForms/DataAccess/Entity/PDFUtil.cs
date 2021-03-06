﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.DataAccess.Entity
{
    public static class PDFUtil
    {
        public static BaseColor LightBlue = new BaseColor(14, 45, 76);
        public static BaseColor grey = new BaseColor(119, 119, 119);
        public static iTextSharp.text.Font font_heading_1 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, new BaseColor(14, 45, 76));
        public static iTextSharp.text.Font font_heading_2 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, new BaseColor(119, 119, 119));
        public static iTextSharp.text.Font font_body = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(14, 45, 76));
        public static iTextSharp.text.Font font_body_bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, new BaseColor(14, 45, 76));
        public static iTextSharp.text.Font spanBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, new BaseColor(100, 100, 100));
        public static iTextSharp.text.Font spanNormal = FontFactory.GetFont(FontFactory.HELVETICA, 12, new BaseColor(119, 119, 119));
        public static iTextSharp.text.Font spanNormalBlack = FontFactory.GetFont(FontFactory.HELVETICA, 11, new BaseColor(43, 43, 43));


        public static PdfPCell HeaderCell(string text, Font font, int align = 0, float BordersWidth = 3f)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 4f;
            cell.PaddingTop = 4f;
            cell.HorizontalAlignment = align;
            return cell;
        }

        public static Paragraph GetLineSeparator()
        {
            Paragraph LineSeparator = new Paragraph("   ");
            iTextSharp.text.pdf.draw.VerticalPositionMark seperator = new iTextSharp.text.pdf.draw.LineSeparator();
            seperator.Offset = -4f;
            LineSeparator.Add(seperator);
            return LineSeparator;
        }

        public static PdfPCell CreateCell(object text, Font fontStyle, int align = 0, bool hasBorder = false, BaseColor baseColor = null, bool padding = false, int colSpan = 0)
        {
            var cell = new PdfPCell(new Phrase(AppUtil.formatText(text), fontStyle));
            if (hasBorder)
            {
                cell.Border = iTextSharp.text.Rectangle.BOX;
                if (baseColor == null)
                {
                    cell.BorderColor = new BaseColor(119, 119, 119);
                }
            }
            else { cell.Border = iTextSharp.text.Rectangle.NO_BORDER; }
            cell.HorizontalAlignment = align;
            if (!padding)
            {
                cell.Padding = 5;
            }
            if (colSpan > 0)
            {
                cell.Colspan = colSpan;
            }
            return cell;
        }

        /// <summary>
        /// For Job Activation Headers
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PdfPTable HeaderSection(string text)
        {
            var HeaderTable = new PdfPTable(1);
            HeaderTable.HorizontalAlignment = 0;
            HeaderTable.WidthPercentage = 100;
            HeaderTable.SpacingBefore = 5;
            HeaderTable.SpacingAfter = 5;
            HeaderTable.DefaultCell.Border = 0;
            HeaderTable.SetWidths(new float[] { 1 });

            PdfPCell cell = new PdfPCell(new Phrase(text, font_heading_1));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 4f;
            cell.PaddingTop = 1f;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

            HeaderTable.AddCell(cell);
            return HeaderTable;

        }

        public static PdfPTable createTableWithHeader(string title, float[] columns, bool setBorder=false)
        {
            var table = new PdfPTable(columns.Length);
            table.TableEvent = (new BorderEvent());
            table.HorizontalAlignment = 0;
            table.WidthPercentage = 100;
            table.SpacingBefore = 5;
            table.SpacingAfter = 5;
            table.DefaultCell.Border = 0;
            table.DefaultCell.Padding = 10f;
            table.SetWidths(columns);

            var tableTitle = new PdfPCell(new Phrase(title, PDFUtil.spanNormal));
            tableTitle.Border = Rectangle.NO_BORDER;
            tableTitle.PaddingBottom = 8f;
            tableTitle.PaddingTop = 4f;
            tableTitle.PaddingLeft = 8f;
            tableTitle.HorizontalAlignment = 0;
            tableTitle.Colspan = columns.Length;
            table.AddCell(tableTitle);
            if (!setBorder)
            {
                table.TableEvent = (new BorderEvent());

            }
            return table;
        }

    }
}