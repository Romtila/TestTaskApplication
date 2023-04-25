using Mapster;
using TestTaskApplication.Application.Models;
using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Application.Mappers;

public class NodeMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Node, NodeReadModel>()
            .Map(dest => dest.Children, src => src.Children);
    }
}