namespace Game.Core.Gameplay.Dogs
{
    public class DogModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DogInfoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Hypoallergenic { get; set; }
    }
}