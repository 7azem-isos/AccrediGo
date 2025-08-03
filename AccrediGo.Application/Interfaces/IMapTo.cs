using AutoMapper;

namespace AccrediGo.Application.Interfaces
{
    public interface IMapTo<TEntity> where TEntity : class
    {
        void Mapping(Profile profile);
    }
} 