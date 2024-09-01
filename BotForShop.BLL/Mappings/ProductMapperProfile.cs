using AutoMapper;
using BotForShop.Core.Dtos;
using BotForShop.Core.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.BLL.Mappings
{
    public class ProductMapperProfile:Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductDto, ProductOutputModel>();
        }
    }
}
