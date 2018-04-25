namespace CMSCore.Shared.Configuration
{
    public class AzureStorageConst
    {
        public const string DefaultEndpointsProtocol = "https";

        public const string AccountName = "cmscore";

        public const string AccountKey =
            "Abc9f2usjENqWPSDSxZt3A97NqRInznfjfhFN07ZgTvBOy+EzPga3XvHii8tFLXjPmuS0QtX09Hv3qkU43xX+g==";

        public static readonly string CMSCoreConnectionString =
            $"DefaultEndpointsProtocol={DefaultEndpointsProtocol};AccountName={AccountName};AccountKey={AccountKey}";
    }
}