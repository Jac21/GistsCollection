namespace IteratorDemo
{
    class PaintingTask<TPainter> where TPainter : IPainter
    {
        public TPainter Painter { get; set; }
        public double SquareMeters { get; set; }

        public PaintingTask(TPainter painter, double sqMeters)
        {
            this.Painter = painter;
            this.SquareMeters = sqMeters;
        }
    }
}
