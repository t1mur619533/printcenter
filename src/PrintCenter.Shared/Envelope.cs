namespace PrintCenter.Shared
{
    public abstract class Envelope<TModel>
    {
        public TModel Model { get; set; }

        public int TotalCount { get; set; }

        private Envelope()
        {
        }

        protected Envelope(TModel model, int totalCount)
        {
            Model = model;
            TotalCount = totalCount;
        }
    }
}