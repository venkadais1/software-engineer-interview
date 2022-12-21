using AutoMapper;
using Microsoft.Extensions.Configuration;
using NLog.Config;

namespace Zip.Installments.DAL.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<IConfiguration, LoggingConfiguration>()
            //    .ForMember(l=>l.Variables.Add("","")
        }
    }
}
