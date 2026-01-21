using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration cfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User,UserDTO>().ReverseMap();
            cfg.CreateMap<Note, NoteDTO>().ReverseMap();
            cfg.CreateMap<NoteCreateDTO, Note>();

        });
        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
