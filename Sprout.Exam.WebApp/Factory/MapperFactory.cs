using AutoMapper;
using System;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp.Factory
{
    public class MapperFactory : IMapperFactory
    {
        public Dictionary<string, IMapper> Mappers { get; set; } = new Dictionary<string, IMapper>();
        public IMapper GetMapper(string mapperName = "")
        {
            return Mappers[mapperName];
        }
    }

    public interface IMapperFactory
    {
        IMapper GetMapper(string mapperName = "");
    }

    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
