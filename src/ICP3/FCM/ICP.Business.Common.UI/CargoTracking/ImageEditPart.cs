using System.Drawing;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Crawler.CommonLibrary;

namespace ICP.Business.Common.UI
{
    public partial class ImageEditPart : BaseEditPart
    {
        public ImageEditPart()
        {
            InitializeComponent();
        }

        public override object DataSource
        {
            set
            {
                if (value !=null)
                {
                    if (value is byte[])
                        pictureBox1.Image = ImageHelper.BytesToImage((byte[])value);
                    else if (value is Image)
                        pictureBox1.Image = (Image)value;
                    else
                        pictureBox1.Image = null;
                }
            }
        }
    }
}
