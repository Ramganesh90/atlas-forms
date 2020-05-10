using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.DataAccess.Entity
{
    public class BorderEvent : IPdfPTableEvent
    {
        public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases)
        {
            float[] width = widths[0];
            float x1 = width[0];
            float x2 = width[width.Length - 1];
            float y1 = heights[0];
            float y2 = heights[heights.Length - 1];
            PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
            cb.RoundRectangle(x1, y1, x2 - x1, y2 - y1, 3);
            cb.SetRGBColorStroke(66, 139, 202);
            cb.SetLineWidth(0.5f);
            cb.Stroke();
        }
    }
}