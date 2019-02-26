namespace TaskParallel.ForEach.ScannerExample
{
    public interface ICodedImage
    {
    }

    public class CodedImage : ICodedImage
    {
        private byte[] imageData;
        private Image image;

        public CodedImage(byte[] imageData, Image image)
        {
            this.imageData = imageData;
            this.image = image;
        }
    }
}