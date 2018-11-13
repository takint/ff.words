namespace ff.words.application.AutoMapper
{
    using global::AutoMapper;

    public class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}