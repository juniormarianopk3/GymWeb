using AutoMapper;
using GymWeb.Models;
using GymWeb.Models.ViewModels;

namespace GymWeb.Profiles {
    public class ExercicioProfile  : Profile{

        public ExercicioProfile() {
            CreateMap<ExercicioViewModel, Exercicio>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.Id}")
                ).ForMember(
                dest => dest.Nome,
                opt => opt.MapFrom(src => $"{src.Nome}")
                ).ForMember(
                dest => dest.AreaCorporal,
                opt => opt.MapFrom(src => $"{src.AreaCorporal}")
                ).ForMember(
                dest => dest.AreaCorporal,
                opt => opt.MapFrom(src => $"{src.AreaCorporal}")
                ).ForMember(
                dest => dest.QntSerie,
                opt => opt.MapFrom(src => $"{src.QntSerie}")
                ).ForMember(
                dest => dest.Repeticao,
                opt => opt.MapFrom(src => $"{src.Repeticao}")
                ).ForMember(
                dest => dest.Image,
                opt => opt.MapFrom(src => $"{src.Image}")
                ).ForMember(
                dest => dest.ImageMusculoAlvo,
                opt => opt.MapFrom(src => $"{src.ImageMusculoAlvo}")
                ).ForMember(
                dest => dest.Preparacao,
                opt => opt.MapFrom(src => $"{src.Preparacao}")
                ).ForMember(
                dest => dest.Execucao,
                opt => opt.MapFrom(src => $"{src.Execucao}")
                ).ForMember(
                dest => dest.Dicas,
                opt => opt.MapFrom(src => $"{src.Dicas}")
                ).ForMember(
                dest => dest.MusculoPrimario,
                opt => opt.MapFrom(src => $"{src.MusculoPrimario}")
                )
                .ForMember(
                dest => dest.MusculoSecundario,
                opt => opt.MapFrom(src => $"{src.MusculoSecundario}")
                );
        }
    }
}
