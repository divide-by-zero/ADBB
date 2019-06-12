namespace ADBB
{
    public class Device
    {
        public string Name { get; }
        public string Type { get; }

        public string DispName => Name + ":" + Type;
        public static Device None => new Device("None","None");

        public Device(string name,string type)
        {
            Name = name;
            Type = type;
        }
    }
}