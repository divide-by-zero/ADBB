namespace ADBB
{
    public class PackageData
    {
        public string Name { get; }
        public string ApkPath { get; }

        public PackageData(string name,string apkPath)
        {
            Name = name;
            ApkPath = apkPath;
        }
    }
}