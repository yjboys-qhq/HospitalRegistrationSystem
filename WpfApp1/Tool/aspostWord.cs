using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Drawing;

namespace Tool
{
    class AspostWord
    {
        private Aspose.Words.Document doc;
        private Aspose.Words.DocumentBuilder builder;
        public AspostWord()
        {
        }

        public AspostWord(Document doc)
        {
            this.doc = doc;
        }

        public void CreateNewDocument(string filePath)
        {
            this.doc = new Aspose.Words.Document(filePath);
            this.builder = new Aspose.Words.DocumentBuilder(doc);
        }

        public void SaveDocument(string filePath)
        {
            this.doc.Save(filePath, Aspose.Words.SaveFormat.Xps);
        }

        public void InsertValue(string bookmark, string value)
        {
            if (this.doc.Range.Bookmarks[bookmark] != null)
            {
                this.doc.Range.Bookmarks[bookmark].Text = value;
            }
        }

        public void InsertPicture(string bookmark, string picturePath, float width, float height)
        {
            if (this.doc.Range.Bookmarks[bookmark] != null)
            {
                builder.MoveToBookmark(bookmark);
                /*var img = */
                builder.InsertImage(picturePath, RelativeHorizontalPosition.Margin, 0, RelativeVerticalPosition.Margin, 0, 155, 155, WrapType.Square);
                //img.Width = width;
                //img.Height = height;
                //img.HorizontalAlignment = Aspose.Words.Drawing.HorizontalAlignment.Center;
            }
        }

        public void InsertPicture(string bookmark, string picturePath)
        {
            this.InsertPicture(bookmark, picturePath, 150, 150);
        }
        public Document Doc { get { return doc; } set { doc = value; } }
        public DocumentBuilder Builder { get { return builder; } set { builder = value; } }
    }
}
