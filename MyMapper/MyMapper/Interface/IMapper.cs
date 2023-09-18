using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapper.Interface
{
    internal interface IMapper<FromModel, ToModel> where ToModel : class, new()
    {
        ToModel MappingModels(FromModel entity);
    }
}
