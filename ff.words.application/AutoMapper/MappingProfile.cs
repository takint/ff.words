namespace ff.words.application.AutoMapper
{
    using ff.words.application.Common;
    using ff.words.data.Common;
    using global::AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var types = typeof(ICreateMapping).GetTypeInfo().Assembly.GetTypes();

            LoadEntityMappings(types);
        }

        private void LoadEntityMappings(IEnumerable<Type> types)
        {
            CreateMap<BaseViewModel, BaseEntity>().ForMember(m => m.RowVersion, x => x.MapFrom(vm => ByteArrayConverter.FromString(vm.RowVersion)));
            CreateMap<BaseEntity, BaseViewModel>().ForMember(m => m.RowVersion, x => x.MapFrom(ent => ByteArrayConverter.ToString(ent.RowVersion)));

            var maps = (from t in types
                       where typeof(ICreateMapping).IsAssignableFrom(t)
                                && !t.GetTypeInfo().IsAbstract
                                && !t.GetTypeInfo().IsInterface
                       select (ICreateMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMapping(this);
            }
        }
    }
}